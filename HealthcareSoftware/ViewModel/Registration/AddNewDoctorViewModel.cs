using DataValidations;
using HealthcareData.Models;
using HealthcareData.Repositories;
using HealthcareData.Validations;
using HealthcareSoftware.Command;
using HealthcareSoftware.View.Registration;
using System;
using System.ComponentModel;
using System.Windows;
using System.Windows.Input;

namespace HealthcareSoftware.ViewModel.Registration
{
    class AddNewDoctorViewModel : ViewModelBase, IDataErrorInfo
    {
        #region Fields
        private readonly AddNewDoctorView addNewDoctorView;
        private tblDoctor doctor;
        private tblHealthcareUserData userData;
        private string surname;
        private string givenName;
        private string personalNo;
        private string username;
        private string password;
        private string bankAccountNo;
        #endregion
        #region Properties
        public bool IsAddedNewDoctor { get; internal set; }
        public bool CanSave { get; set; }

        public string BankAccountNo
        {
            get
            {
                return bankAccountNo;
            }
            set
            {
                if (bankAccountNo == value) return;
                bankAccountNo = value;
                OnPropertyChanged(nameof(BankAccountNo));
            }
        }
        public string Password
        {
            get
            {
                return password;
            }
            set
            {
                if (password == value) return;
                password = value;
                OnPropertyChanged(nameof(Password));
            }
        }
        public string Username
        {
            get
            {
                return username;
            }
            set
            {
                if (username == value) return;
                username = value;
                OnPropertyChanged(nameof(Username));
            }
        }
        public string PersonalNo
        {
            get
            {
                return personalNo;
            }
            set
            {
                if (personalNo == value) return;
                personalNo = value;
                OnPropertyChanged(nameof(PersonalNo));
            }
        }
        public string Surname
        {
            get
            {
                return surname;
            }
            set
            {
                if (surname == value) return;
                surname = value;
                OnPropertyChanged(nameof(Surname));
            }
        }

        public string GivenName
        {
            get
            {
                return givenName;
            }
            set
            {
                if (givenName == value) return;
                givenName = value;
                OnPropertyChanged(nameof(GivenName));
            }
        }

        #endregion
        #region Constructors
        public AddNewDoctorViewModel(AddNewDoctorView addNewDoctorView)
        {
            this.addNewDoctorView = addNewDoctorView;
            PersonalNo = string.Empty;
            Username = string.Empty;
            Password = string.Empty;
            GivenName = string.Empty;
            Surname = string.Empty;
            BankAccountNo = string.Empty;
            doctor = new tblDoctor();
            userData = new tblHealthcareUserData();
            CanSave = true;
        }

        #endregion

        #region IDataErrorInfoImplementation
        //validations

        public string Error
        {
            get
            {
                return null;
            }
        }

        public string this[string name]
        {
            get
            {
                CanSave = true;
                var validate = new Validations();
                var validateHealthcareData = new HealthcareValidations();
                string validationMessage = string.Empty;

                if (name == "PersonalNo")
                {
                    if (!validate.IsValidPersonalNoFormat(PersonalNo))
                    {
                        validationMessage = "Invalid personal number format!";
                        CanSave = false;
                    }
                    if (!validateHealthcareData.IsUniquePersonalNo(PersonalNo))
                    {
                        validationMessage = "Personal number must be unique!";
                        CanSave = false;
                    }
                }
                else if (name == "Username")
                {
                    if (!validateHealthcareData.IsUniqueUsername(Username))
                    {
                        validationMessage = "Username number must be unique!";
                        CanSave = false;
                    }
                }

                else if (name == "BankAccountNo")
                {
                    if (!validate.IsDigitsOnly(BankAccountNo) || BankAccountNo.Length != 16)
                    {
                        validationMessage = "Invalid bank account number!";
                        CanSave = false;
                    }
                    if (!validateHealthcareData.IsUniqueBankAccountNo(BankAccountNo))
                    {
                        validationMessage = "Bank Account No must be unique!";
                        CanSave = false;
                    }
                }
                if (string.IsNullOrEmpty(validationMessage))
                    CanSave = true;

                return validationMessage;
            }
        }
        #endregion

        #region Commands
        private ICommand save;
        public ICommand Save
        {
            get
            {
                if (save == null)
                {
                    save = new RelayCommand(param => SaveExecute(), param => CanSaveExecute());
                }
                return save;
            }
        }

        private bool CanSaveExecute()
        {
            if (
                string.IsNullOrWhiteSpace(GivenName)
                || string.IsNullOrWhiteSpace(Surname)
                || string.IsNullOrWhiteSpace(BankAccountNo)
                || string.IsNullOrWhiteSpace(PersonalNo)
                || string.IsNullOrWhiteSpace(Username)
                || string.IsNullOrWhiteSpace(Password)
                || CanSave == false)
                return false;
            return true;
        }
        private void SaveExecute()
        {
            try
            {
                var db = new HealtcareDBRepository();
                userData.GivenName = GivenName;
                userData.Surname = Surname;
                userData.PersonalNo = PersonalNo;
                userData.Username = Username;
                userData.Password = SecurePasswordHasher.Hash(Password);

                db.TryAddNewUserData(userData);
                var userId = db.GetUserDataId(Username);
                if (userId != 0)
                {
                    doctor.UserDataID = userId;
                    doctor.BankAccountNo = BankAccountNo;
                    //adding new patient to database 
                    IsAddedNewDoctor = db.TryAddNewDoctor(doctor);
                    addNewDoctorView.Close();
                    return;
                }
                IsAddedNewDoctor = false;
                addNewDoctorView.Close();
                return;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }


        //Escaping action
        private ICommand exit;
        public ICommand Exit
        {
            get
            {
                if (exit == null)
                {
                    exit = new RelayCommand(param => ExitExecute(), param => CanExitExecute());
                }
                return exit;
            }
        }


        private bool CanExitExecute()
        {
            return true;
        }

        private void ExitExecute()
        {
            IsAddedNewDoctor = false;
            addNewDoctorView.Close();
        }
        #endregion
    }
}
