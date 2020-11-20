/***********************************************************************
 * Module:  PatientController.cs
 * Author:  Sladjana Savkovic
 * Purpose: Definition of the Class Controller.Users&WorkingTimeController.PatientController
 ***********************************************************************/

using Model.Users;
using Service.UsersAndWorkingTime;
using System;
using System.Collections.Generic;

namespace Controller.UsersAndWorkingTime
{
   public class PatientController : IUserStrategy
   {

        public PatientService patientService = new PatientService();

        public bool DeleteProfile(string jmbg)
        {
            return patientService.DeletePatient(jmbg);
        }

        public User EditProfile(User user)
        {
            return patientService.EditPatient((Patient)user);
        }

        public User Register(User user)
        {
            return patientService.RegisterPatient((Patient)user);
        }

        public User SignIn(string username, string password)
        {
            return patientService.SignIn(username, password);
        }

        public List<User> ViewAllUsers()
        {
            List<Patient> patients =  patientService.ViewPatients();
            List<User> result = new List<User>();
            result.AddRange(patients);
            return result;
        }

        public User ViewProfile(string jmbg)
        {
            return patientService.ViewProfile(jmbg);
        }
    }
}