using DataValidations;
using HealthcareData.Models;
using HealthcareData.Repositories;
using HealthcareData.Validations;
using HealthcareSoftware.Command;
using HealthcareSoftware.View.Registration;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace HealthcareSoftware.ViewModel.Registration
{
    class AddNewPatientViewModel : ViewModelBase, IDataErrorInfo
    {
        #region Fields
        private readonly AddNewPatientView addNewPatientView;
        private tblPatient patient;
        private tblHealthcareUserData userData;
        private string surname;
        private string givenName;
        private string personalNo;
        private string username;
        private string password;
        private string healthInsuranceCardNo;
        #endregion
        #region Properties
        public bool IsAddedNewPatient { get; internal set; }
        public bool CanSave { get; set; }

        public string HealthInsuranceCardNo
        {
            get
            {
                return healthInsuranceCardNo;
            }
            set
            {
                if (healthInsuranceCardNo == value) return;
                healthInsuranceCardNo = value;
                OnPropertyChanged(nameof(HealthInsuranceCardNo));
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
        public AddNewPatientViewModel(AddNewPatientView addNewPatientView)
        {
            this.addNewPatientView = addNewPatientView;
            PersonalNo = string.Empty;
            Username = string.Empty;
            Password = string.Empty;
            GivenName = string.Empty;
            Surname = string.Empty;
            HealthInsuranceCardNo = string.Empty;
            patient = new tblPatient();
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
               
                else if (name == "HealthInsuranceCardNo")
                {
                    if (!validateHealthcareData.IsUniqueHealthInsuranceCardNo(HealthInsuranceCardNo))
                    {
                        validationMessage = "Health Insurance Card No must be unique!";
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
                || string.IsNullOrWhiteSpace(HealthInsuranceCardNo)
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
                if(userId != 0)
                {
                    patient.UserDataID = userId;
                    patient.HealthInsuranceCardNo = HealthInsuranceCardNo;
                    //adding new patient to database 
                    IsAddedNewPatient = db.TryAddNewPatient(patient);
                    addNewPatientView.Close();
                    return;
                }
                IsAddedNewPatient = false;
                addNewPatientView.Close();
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
            IsAddedNewPatient = false;
            addNewPatientView.Close();
        }
        #endregion
    }
}
