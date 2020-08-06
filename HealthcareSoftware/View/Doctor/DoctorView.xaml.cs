using HealthcareData.Models;
using HealthcareSoftware.ViewModel.Doctor;
using System.Windows;

namespace HealthcareSoftware.View.Doctor
{
    /// <summary>
    /// Interaction logic for DoctorView.xaml
    /// </summary>
    public partial class DoctorView : Window
    {
        public DoctorView(tblDoctor doctor)
        {
            InitializeComponent();
            this.DataContext = new DoctorViewModel(this, doctor);
        }
    }
}
