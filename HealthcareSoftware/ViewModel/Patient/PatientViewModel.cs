using HealthcareData.Models;
using HealthcareSoftware.Command;
using HealthcareSoftware.View.Patient;
using System;
using System.Windows;
using System.Windows.Input;

namespace HealthcareSoftware.ViewModel.Patient
{
    class PatientViewModel : ViewModelBase
    {
        #region Fields
        private readonly PatientView patientView;
        private tblPatient patient;
        #endregion
  

        #region Constructors
        public PatientViewModel(PatientView patientView, tblPatient patient)
        {
            this.patientView = patientView;
            this.patient = patient;
        }
        #endregion
        #region Commands

        //choose doctor

        private ICommand chooseDoctor;
        public ICommand ChooseDoctor
        {
            get
            {
                if (chooseDoctor == null)
                {
                    chooseDoctor = new RelayCommand(param => ChooseDoctorExecute(), param => CanChooseDoctor());
                }
                return chooseDoctor;
            }
        }

        private void ChooseDoctorExecute()
        {
            try
            {
                ChooseDoctorView chooseDoctorViewView = new ChooseDoctorView(patient);
                chooseDoctorViewView.ShowDialog();

                if ((chooseDoctorViewView.DataContext as ChooseDoctorViewViewModel).IsDoctorChosen == true)
                {
                    MessageBox.Show("Doctor is successfully chosen!");
                }
                else
                    MessageBox.Show("Something went wrong.");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }
        private bool CanChooseDoctor()
        {
            if (patient.DoctorID == 0)
                return true;
            return false;
        }


        //adding new requirement

        private ICommand addNewRequirement;
        public ICommand AddNewRequirement
        {
            get
            {
                if (addNewRequirement == null)
                {
                    addNewRequirement = new RelayCommand(param => AddNewRequirementExecute(), param => CanAddAddNewRequirement());
                }
                return addNewRequirement;
            }
        }

        private void AddNewRequirementExecute()
        {
            try
            {
                AddNewRequirementView addNewRequirementView = new AddNewRequirementView(patient);
                addNewRequirementView.ShowDialog();

                if ((addNewRequirementView.DataContext as AddNewRequirementViewModel).IsAddedNewRequirement == true)
                {
                    MessageBox.Show("New Sick Leave Requirement is successfully created!");
                }
                else
                    MessageBox.Show("Something went wrong.");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }
        private bool CanAddAddNewRequirement()
        {
            if (patient.DoctorID == 0)
                return false;
            else
                return true;
        }


        //closing the view

        private ICommand logout;
        public ICommand Logout
        {
            get
            {
                if (logout == null)
                {
                    logout = new RelayCommand(param => ExitExecute(), param => CanExitExecute());
                }
                return logout;
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
