using HealthcareData.Models;
using HealthcareSoftware.ViewModel.Patient;
using System.Windows;

namespace HealthcareSoftware.View.Patient
{
    /// <summary>
    /// Interaction logic for AddNewRequirementView.xaml
    /// </summary>
    public partial class AddNewRequirementView : Window
    {
        public AddNewRequirementView(tblPatient patient)
        {
            InitializeComponent();
            this.DataContext = new AddNewRequirementViewModel(this, patient);
        }
    }
}
