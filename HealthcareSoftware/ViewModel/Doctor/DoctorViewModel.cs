using HealthcareData.Models;
using HealthcareSoftware.View.Doctor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthcareSoftware.ViewModel.Doctor
{
    class DoctorViewModel : ViewModelBase
    {
        #region Fields
        private readonly DoctorView doctorView;
        private tblDoctor doctor;
        #endregion


        #region Constructors
        public DoctorViewModel(DoctorView doctorView, tblDoctor doctor)
        {
            this.doctorView = doctorView;
            this.doctor = doctor;
        }
        #endregion
    }
}
