using DataValidations;
using HealthcareSoftware.Command;
using HealthcareSoftware.View.Registration;
using System.Windows.Controls;
using System.Windows.Input;

namespace HealthcareSoftware.ViewModel
{
	class MainWindowViewModel : ViewModelBase
	{
		#region Fields
		private string userName;
		readonly MainWindow loginView;
		#endregion

		#region Constructor
		internal MainWindowViewModel(MainWindow view)
		{
			this.loginView = view;
		}
		#endregion

		#region Properties
		public string UserName
		{
			get
			{
				return userName;
			}
			set
			{
				userName = value;
				OnPropertyChanged(nameof(UserName));
			}
		}
		#endregion
		//login
		private ICommand submitCommand;
		public ICommand SubmitCommand
		{
			get
			{
				if (submitCommand == null)
				{
					submitCommand = new RelayCommand(Submit);
					return submitCommand;
				}
				return submitCommand;
			}
		}

		void Submit(object obj)
		{
			string password = (obj as PasswordBox).Password;
			var validate = new Validations();
			//if (UserName == Constants.usernamedMaster && SecurePasswordHasher.Verify(password, constants.passwordEmployeeHashed))
			//{
			//	MasterView masterView = new MasterView();
			//	loginView.Close();
			//	masterView.Show();
			//	return;
			//}
		}	

		//registrate
		private ICommand registrateCommand;
		public ICommand RegistrateCommand
		{
			get
			{
				if (registrateCommand == null)
				{
					registrateCommand = new RelayCommand(Registrate);
					return registrateCommand;
				}
				return registrateCommand;
			}
		}

		private void Registrate(object obj)
		{
			RegistrationView registrateView = new RegistrationView();
			loginView.Close();
			registrateView.Show();
			return;
		}
	}
}
