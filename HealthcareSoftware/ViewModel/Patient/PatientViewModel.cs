using HealthcareData.Models;
using HealthcareSoftware.View.Patient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

    }
}
