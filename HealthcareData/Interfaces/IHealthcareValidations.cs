using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthcareData.Interfaces
{
    public interface IHealthcareValidations
    {
        bool IsCorrectUser(string userName, string password);
        bool IsUniqueUsername(string username);
        bool IsUniquePersonalNo(string personalNo);
        bool IsRequrementApproved(int requirementId);
        bool IsDoctorChosen(int patientId);

    }
}
