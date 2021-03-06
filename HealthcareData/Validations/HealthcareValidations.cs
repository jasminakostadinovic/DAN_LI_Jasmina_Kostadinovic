﻿using DataValidations;
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
                    var user = conn.tblHealthcareUserDatas.FirstOrDefault(x => x.Username == userName);
                  
                    if (user != null)
                    {
                        var passwordFromDb = conn.tblHealthcareUserDatas.First(x => x.Username == userName).Password;
                        return SecurePasswordHasher.Verify(password, passwordFromDb);
                    }
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

        public string GetUserType(int userDataId)
        {
            try
            {
                using (var conn = new HealthcareAppDataEntities())
                {
                    if (conn.tblPatients.Any(x => x.UserDataID == userDataId))
                        return "patient";
                    if (conn.tblDoctors.Any(x => x.UserDataID == userDataId))
                        return "doctor";
                    return null;
                }
            }
            catch (Exception)
            {
                return null;
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

        public bool IsUniqueBankAccountNo(string bankAccountNo)
        {
            try
            {
                using (var conn = new HealthcareAppDataEntities())
                {
                    return !conn.tblDoctors.Any(x => x.BankAccountNo == bankAccountNo);
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
            catch (Exception)
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

