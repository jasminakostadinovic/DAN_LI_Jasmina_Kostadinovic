using HealthcareSoftware.ViewModel.Registration;
using System.Windows;

namespace HealthcareSoftware.View.Registration
{
    /// <summary>
    /// Interaction logic for AddNewPatientView.xaml
    /// </summary>
    public partial class AddNewPatientView : Window
    {
        public AddNewPatientView()
        {
            InitializeComponent();
            this.DataContext = new AddNewPatientViewModel(this);
        }
    }
}
