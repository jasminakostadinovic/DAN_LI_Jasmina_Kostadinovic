using HealthcareData.Interfaces;
using HealthcareData.Models;
using System;
using System.Linq;

namespace HealthcareData.Validations
{
    public class HealthcareValidations : IHealthcareValidations
    {
        public bool IsCorrectUser(string userName, string password)
        {
            try
            {
                using (var conn = new HealthcareAppDataEntities())
                {
                    var user = conn.tblHealthcareUserDatas.FirstOrDefault(x => x.Username == userName && x.Password == password);
                    if (user != null)
                        return true;
                    return false;
                }
            }
            catch (Exception)
            {
                return false;
            }

        }


        public bool IsDoctorChosen(int patientId)
        {
            try
            {
                using (var conn = new HealthcareAppDataEntities())
                {
                    var patient = conn.tblPatients.FirstOrDefault(x => x.PatientID == patientId);
                    if (patient != null)
                        return patient.DoctorID != 0;
                    return false;
                }
            }
            catch (Exception)
            {
                return false;
            }
        }

        public bool IsRequrementApproved(int requirementId)
        {
            try
            {
                using (var conn = new HealthcareAppDataEntities())
                {
                    var requirement = conn.tblSickLeaveRequirements.FirstOrDefault(x => x.SickLeaveRequirementID == requirementId);
                    if (requirement != null)
                        return requirement.IsApproved;
                    return false;
                }
            }
            catch (Exception)
            {
                return false;
            }
        }

        public bool IsUniqueHealthInsuranceCardNo(string insuranceNo)
        {
            try
            {
                using (var conn = new HealthcareAppDataEntities())
                {
                    return !conn.tblPatients.Any(x => x.HealthInsuranceCardNo == insuranceNo);
                }
            }
            catch (Exception)
            {
                return false;
            }
        }

        public bool IsUniquePersonalNo(string personalNo)
        {
            try
            {
                using (var conn = new HealthcareAppDataEntities())
                {
                    return !conn.tblHealthcareUserDatas.Any(x => x.PersonalNo == personalNo);
                }
            }
            catch (Exception e)
            {
                return false;
            }
        }

        public bool IsUniqueUsername(string username)
        {
            try
            {
                using (var conn = new HealthcareAppDataEntities())
                {
                    return !conn.tblHealthcareUserDatas.Any(x => x.Username == username);
                }
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}

