using HealthcareSoftware.ViewModel.Registration;
using System.Windows;

namespace HealthcareSoftware.View.Registration
{
    /// <summary>
    /// Interaction logic for AddNewDoctorView.xaml
    /// </summary>
    public partial class AddNewDoctorView : Window
    {
        public AddNewDoctorView()
        {
            InitializeComponent();
            this.DataContext = new AddNewDoctorViewModel(this);
        }
    }
}
