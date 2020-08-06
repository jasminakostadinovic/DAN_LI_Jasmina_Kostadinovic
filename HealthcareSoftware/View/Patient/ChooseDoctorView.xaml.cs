using System.Windows;
using HealthcareData.Models;
using HealthcareSoftware.ViewModel.Patient;

namespace HealthcareSoftware.View.Patient
{
    /// <summary>
    /// Interaction logic for ChooseDoctorView.xaml
    /// </summary>
    public partial class ChooseDoctorView : Window
    {
        public ChooseDoctorView(tblPatient patient)
        {
            InitializeComponent();
            this.DataContext = new ChooseDoctorViewViewModel(this, patient);
        }
    }
}
