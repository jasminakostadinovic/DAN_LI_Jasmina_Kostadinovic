using HealthcareSoftware.View.Registration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
	}
}
