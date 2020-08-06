using HealthcareData.Models;
using HealthcareData.Repositories;
using HealthcareSoftware.Command;
using HealthcareSoftware.View.Patient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace HealthcareSoftware.ViewModel.Patient
{
    class AddNewRequirementViewModel : ViewModelBase, IDataErrorInfo
    {
        #region Fields
        private readonly AddNewRequirementView patientView;
        private string requirementDate;
        private string requirementCause;
        private string companyName;
        private string isEmergencyStr;
        private DateTime dateDateValue;
        private tblPatient patient;
        private tblSickLeaveRequirement requirement;
        #endregion
        #region Properties
        public bool CanSave { get; set; }
        public bool IsAddedNewRequirement { get; set; }

        public string IsEmergencyStr
        {
            get
            {
                return isEmergencyStr;
            }
            set
            {
                if (isEmergencyStr == value) return;
                isEmergencyStr = value;
                OnPropertyChanged(nameof(IsEmergencyStr));
            }
        }
        public string RequirementCause
        {
            get
            {
                return requirementCause;
            }
            set
            {
                if (requirementCause == value) return;
                requirementCause = value;
                OnPropertyChanged(nameof(RequirementCause));
            }
        }
        public string RequirementDate
        {
            get
            {
                return requirementDate;
            }
            set
            {
                if (requirementDate == value) return;
                requirementDate = value;
                OnPropertyChanged(nameof(RequirementDate));
            }
        }

        public string CompanyName
        {
            get
            {
                return companyName;
            }
            set
            {
                companyName = value;
                OnPropertyChanged(nameof(CompanyName));
            }
        }
        #endregion

        #region Constructors
        public AddNewRequirementViewModel(AddNewRequirementView patientView, tblPatient patient)
        {
            this.patientView = patientView;
            this.patient = patient;
            requirement = new tblSickLeaveRequirement();
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

                string validationMessage = string.Empty;
                var culture = CultureInfo.InvariantCulture;
                var styles = DateTimeStyles.None;

                if (name == "Date")
                {
                    if (!DateTime.TryParse(RequirementDate, culture, styles, out dateDateValue))
                    {
                        validationMessage = "Invalid date format! use: [4/10/2008]";
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
                string.IsNullOrWhiteSpace(CompanyName)
                || string.IsNullOrWhiteSpace(RequirementCause)
                || string.IsNullOrWhiteSpace(RequirementDate)              
                || CanSave == false)
                return false;
            return true;
        }
        private void SaveExecute()
        {
            try
            {
                var db = new HealtcareDBRepository();

                requirement.RequirementDate = DateTime.Parse(RequirementDate);
                requirement.IsApproved = false;
                requirement.PatientID = patient.PatientID;
                if (IsEmergencyStr == "y")
                    requirement.IsEmergency = true;
                requirement.CompanyName = CompanyName;
                requirement.RequirementCause = RequirementCause;

                //adding new requrement record to database 
                bool isAdded = db.TryAddNewSickLeaveRequirement(requirement);

                if(isAdded == true)                
                    MessageBox.Show("New Sick Leave Requirement is successfully sent.");                
                else
                    MessageBox.Show("Something went wrong.");

                MainWindow loginWindow = new MainWindow();
                patientView.Close();
                loginWindow.Show();
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
            MainWindow loginWindow = new MainWindow();
            patientView.Close();
            loginWindow.Show();

        }

        #endregion
    }
}
