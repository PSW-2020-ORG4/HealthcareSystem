/***********************************************************************
 * Module:  DoctorSevice.cs
 * Author:  Sladjana Savkovic
 * Purpose: Definition of the Class Service.DoctorSevice
 ***********************************************************************/

using Model.Users;
using Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace Service.UsersAndWorkingTime
{
   public class DoctorService
   {

        private DoctorRepository doctorRepository = new DoctorRepository();
        public Doctor RegisterDoctor(Doctor doctor)
        {
            if (!IsUsernameValid(doctor.Username) || !IsPasswordValid(doctor.Password))
            {
                return null;
            }
            return doctorRepository.NewDoctor(doctor);
        }
      
        public Doctor EditDoctor(Doctor doctor)
        {
            if (!IsUsernameValid(doctor.Username) || !IsPasswordValid(doctor.Password))
            {
                return null;
            }
            return doctorRepository.SetDoctor(doctor);
        }
      
        public bool DeleteDoctor(string jmbg)
        {
            return doctorRepository.DeleteDoctor(jmbg);
        }
      
        public List<Doctor> ViewDoctors()
        {
            return doctorRepository.GetAllDoctors();
        }
      
        public Doctor ViewProfile(string jmbg)
        {
            return doctorRepository.GetDoctor(jmbg);
        }

        public Doctor SignIn(string username, string password) 
        {
            return doctorRepository.CheckUsernameAndPassword(username,password);
        }

        private bool IsUsernameValid(string username) 
        {
            Regex regex = new Regex(@"^[a-zA-Z0-9\.\-_]{5,13}$");
            Match match = regex.Match(username);

            return match.Success;
        }

        private bool IsPasswordValid(string password) 
        {
            Regex regex = new Regex(@"^[a-zA-Z0-9\.\-_]{8,30}$");
            Match match = regex.Match(password);

            return match.Success;
        }
   
   
   }
}