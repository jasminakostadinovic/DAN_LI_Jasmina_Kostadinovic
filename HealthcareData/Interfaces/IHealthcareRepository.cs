using HealthcareData.Models;
using System.Collections.Generic;

namespace HealthcareData.Interfaces
{
    public interface IHealthcareRepository
    {
        bool TryAddNewDoctor(tblDoctor doctor);     
        List<tblDoctor> LoadDoctors();
        tblDoctor LoadDoctor(int doctorId);
        bool TryAddNewPatient(tblPatient patient);
        bool TryUpdatePatient(int patientId, tblPatient patient);
        tblPatient LoadPatient(int patientId);
        bool TryAddNewUserData(tblHealthcareUserData userData);
        bool TryAddNewSickLeaveRequirement(tblSickLeaveRequirement requirement);
        bool TryUpdateSickLeaveRequirement(int requirementId, tblSickLeaveRequirement requirement);
        bool TryDeleteSickLeaveRequirement(int requirementId);
        List<vwSickLeaveRequirement> SickLeaveRequirements();
        vwSickLeaveRequirement SickLeaveRequirement(int requirementId);
        int GetUserDataId(string username);
    }
}
