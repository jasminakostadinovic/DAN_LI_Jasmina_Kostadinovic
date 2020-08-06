using HealthcareData.Interfaces;
using HealthcareData.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthcareData.Repositories
{
    public class HealtcareDBRepository : IHealthcareRepository
    {
        public int GetUserDataId(string username)
        {
            try
            {
                using (var conn = new HealthcareAppDataEntities())
                {
                    var user = conn.tblHealthcareUserDatas.FirstOrDefault(x => x.Username == username);
                    if (user != null)
                        return user.UserDataID;
                    return 0;
                }
            }
            catch (Exception)
            {
                return 0;
            }
        }

        public tblDoctor LoadDoctor(int doctorId)
        {
            try
            {
                using (var conn = new HealthcareAppDataEntities())
                {
                    return conn.tblDoctors.FirstOrDefault(x => x.DoctorID == doctorId);
                }
            }
            catch (Exception)
            {
                return null;
            }
        }

        public List<tblDoctor> LoadDoctors()
        {
            try
            {
                using (var conn = new HealthcareAppDataEntities())
                {
                    var doctors = new List<tblDoctor>();
                    if (conn.tblDoctors.Any())
                    {
                        foreach (var item in conn.tblDoctors)
                        {
                            doctors.Add(item);
                        }
                    }
                    return doctors;
                }
            }
            catch (Exception)
            {
                return new List<tblDoctor>();
            }
        }

        public tblPatient LoadPatient(int patientId)
        {
            try
            {
                using (var conn = new HealthcareAppDataEntities())
                {
                    return conn.tblPatients.FirstOrDefault(x => x.PatientID == patientId);
                }
            }
            catch (Exception)
            {
                return null;
            }
        }  

        public vwSickLeaveRequirement SickLeaveRequirement(int requirementId)
        {
            try
            {
                using (var conn = new HealthcareAppDataEntities())
                {
                    return conn.vwSickLeaveRequirements.FirstOrDefault(x => x.SickLeaveRequirementID == requirementId);
                }
            }
            catch (Exception)
            {
                return null;
            }
        }

        public List<vwSickLeaveRequirement> SickLeaveRequirements()
        {
            try
            {
                using (var conn = new HealthcareAppDataEntities())
                {
                    var requirements = new List<vwSickLeaveRequirement>();
                    if (conn.vwSickLeaveRequirements.Any())
                    {
                        foreach (var item in conn.vwSickLeaveRequirements)
                        {
                            requirements.Add(item);
                        }
                    }
                    return requirements;
                }
            }
            catch (Exception)
            {
                return new List<vwSickLeaveRequirement>();
            }
        }

        public bool TryAddNewDoctor(tblDoctor doctor)
        {
            try
            {
                using (var conn = new HealthcareAppDataEntities())
                {
                    conn.tblDoctors.Add(doctor);
                    conn.SaveChanges();
                    return true;
                }
            }
            catch (Exception)
            {
                return false;
            }
        }

        public bool TryAddNewPatient(tblPatient patient)
        {
            try
            {
                using (var conn = new HealthcareAppDataEntities())
                {
                    conn.tblPatients.Add(patient);
                    conn.SaveChanges();
                    return true;
                }
            }
            catch (Exception e)
            {
                return false;
            }
        }

        public bool TryAddNewSickLeaveRequirement(tblSickLeaveRequirement requirement)
        {
            try
            {
                using (var conn = new HealthcareAppDataEntities())
                {
                    conn.tblSickLeaveRequirements.Add(requirement);
                    conn.SaveChanges();
                    return true;
                }
            }
            catch (Exception)
            {
                return false;
            }
        }

        public bool TryAddNewUserData(tblHealthcareUserData userData)
        {
            try
            {
                using (var conn = new HealthcareAppDataEntities())
                {
                    conn.tblHealthcareUserDatas.Add(userData);
                    conn.SaveChanges();
                    return true;
                }
            }
            catch (Exception e)
            {
                return false;
            }
        }

   

        public bool TryDeleteSickLeaveRequirement(int requirementId)
        {
            try
            {
                using (var conn = new HealthcareAppDataEntities())
                {
                    var requirementToRemove = conn.tblSickLeaveRequirements.FirstOrDefault(x => x.SickLeaveRequirementID == requirementId);
                    if(requirementToRemove != null)
                    {
                        if(requirementToRemove.IsApproved == true)
                        {
                            conn.tblSickLeaveRequirements.Remove(requirementToRemove);
                            conn.SaveChanges();
                            return true;
                        }
                    }
                    return false;
                }
            }
            catch (Exception)
            {
                return false;
            }
        }

        public bool TryUpdatePatient(int patientId, tblPatient patient)
        {
            try
            {
                using (var conn = new HealthcareAppDataEntities())
                {
                    var patientToUpdate = conn.tblPatients.FirstOrDefault(x => x.PatientID == patientId);
                    if(patientToUpdate != null)
                    {
                        patientToUpdate.DoctorID = patient.DoctorID;
                        conn.SaveChanges();
                        return true;
                    }
                    return false;
                }
            }
            catch (Exception)
            {
                return false;
            }
        }

        public bool TryUpdateSickLeaveRequirement(int requirementId, tblSickLeaveRequirement requirement)
        {
            try
            {
                using (var conn = new HealthcareAppDataEntities())
                {
                    var requrementToUpdate = conn.tblSickLeaveRequirements.FirstOrDefault(x => x.SickLeaveRequirementID == requirementId);
                    if (requrementToUpdate != null)
                    {
                        requrementToUpdate.IsApproved = requirement.IsApproved;
                        conn.SaveChanges();
                        return true;
                    }
                    return false;
                }
            }
            catch (Exception)
            {
                return false;
            }
        }

    }
}
