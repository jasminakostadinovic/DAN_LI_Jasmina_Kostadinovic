using HealthcareSoftware.Command;
using HealthcareSoftware.View.Registration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace HealthcareSoftware.ViewModel.Registration
{
	class RegistrationViewModel : ViewModelBase
	{
		#region Fields
		readonly RegistrationView view;
		#endregion

		#region Constructor
		internal RegistrationViewModel(RegistrationView view)
		{
			this.view = view;
		}
        #endregion

        #region Commands

        //adding new patient

        private ICommand addNewPatient;
        public ICommand AddNewPatient
        {
            get
            {
                if (addNewPatient == null)
                {
                    addNewPatient = new RelayCommand(param => AddNewPatientExecute(), param => CanAddNewPatient());
                }
                return addNewPatient;
            }
        }

        private void AddNewPatientExecute()
        {
            try
            {
                AddNewPatientView addNewPatientView = new AddNewPatientView();
                addNewPatientView.ShowDialog();

                if ((addNewPatientView.DataContext as AddNewPatientViewModel).IsAddedNewPatient == true)
                {
                    MessageBox.Show("New patient is successfully created!");
                }
                else
                    MessageBox.Show("Something went wrong. New patient is not created.");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }
        private bool CanAddNewPatient()
        {
            return true;
        }


        //adding new patient

        private ICommand addNewDoctor;
        public ICommand AddNewDoctor
        {
            get
            {
                if (addNewDoctor == null)
                {
                    addNewDoctor = new RelayCommand(param => AddNewDoctorExecute(), param => CanAddNewDoctor());
                }
                return addNewDoctor;
            }
        }

        private void AddNewDoctorExecute()
        {
            try
            {
                AddNewDoctorView addNewDoctorView = new AddNewDoctorView();
                addNewDoctorView.ShowDialog();

                if ((addNewDoctorView.DataContext as AddNewDoctorViewModel).IsAddedNewDoctor == true)
                {
                    MessageBox.Show("New doctor is successfully created!");
                }
                else
                    MessageBox.Show("Something went wrong. New doctor is not created.");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }
        private bool CanAddNewDoctor()
        {
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
            view.Close();
            loginWindow.Show();
        }
        #endregion
    }
}
