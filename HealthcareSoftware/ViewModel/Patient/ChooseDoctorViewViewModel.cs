using HealthcareData.Models;
using HealthcareData.Repositories;
using HealthcareSoftware.Command;
using HealthcareSoftware.View.Patient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace HealthcareSoftware.ViewModel.Patient
{
    class ChooseDoctorViewViewModel : ViewModelBase
    {
        #region Fields
        private readonly ChooseDoctorView chooseDoctorView;
        private tblDoctor doctor;
        private List<tblDoctor> doctors;
        private HealtcareDBRepository db = new HealtcareDBRepository();
        #endregion
        #region Properties
        public bool CanSave { get; set; }
        public bool IsDoctorChosen { get; set; }
        public tblDoctor Doctor
        {
            get
            {
                return doctor;
            }
            set
            {
                if (doctor == value) return;
                doctor = value;
                OnPropertyChanged(nameof(Doctor));
            }
        }
        public List<tblDoctor> Doctors
        {
            get
            {
                return doctors;
            }
            set
            {
                if (doctors == value) return;
                doctors = value;
                OnPropertyChanged(nameof(Doctors));
            }
        }
        private tblPatient patient;
        #endregion

        #region Constructors
        public ChooseDoctorViewViewModel(ChooseDoctorView chooseDoctorView, tblPatient patient)
        {
            this.chooseDoctorView = chooseDoctorView;
            this.patient = patient;
            doctors = db.LoadDoctors();
            if (doctors.Any())
                doctor = doctors[0];
            else
                doctor = new tblDoctor();
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
            return true;
        }
        private void SaveExecute()
        {
            try
            {
                MainWindow loginWindow = new MainWindow();
                var db = new HealtcareDBRepository();

                if (!doctors.Any())
                {
                    MessageBox.Show("There is no created doctor to be chosen.");
                 
                    chooseDoctorView.Close();
                    loginWindow.Show();
                    return;
                }

                patient.DoctorID = doctor.DoctorID;              
                bool isAdded = db.TryUpdatePatient(patient.PatientID, patient);

                if (isAdded == true)
                    MessageBox.Show("New Sick Leave Requirement is successfully sent.");
                else
                    MessageBox.Show("Something went wrong.");

                chooseDoctorView.Close();
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
            chooseDoctorView.Close();
            loginWindow.Show();

        }

        #endregion
    }
}
