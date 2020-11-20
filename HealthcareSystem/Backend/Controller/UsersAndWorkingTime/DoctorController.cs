/***********************************************************************
 * Module:  DoctorController.cs
 * Author:  Sladjana Savkovic
 * Purpose: Definition of the Class Controller.Users&WorkingTimeController.DoctorController
 ***********************************************************************/

using Backend.Repository;
using Model.Users;
using Service.UsersAndWorkingTime;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Controller.UsersAndWorkingTime
{
   public class DoctorController : IUserStrategy
   {
        private DoctorService doctorSevice = new DoctorService(new FileDoctorRepository());

        public void EditProfile(User user)
        {
            doctorSevice.EditDoctor((Doctor)user);
        }

        public void Register(User user)
        {
            doctorSevice.RegisterDoctor((Doctor)user);
        }

        public User SignIn(string username, string password)
        {
            return doctorSevice.SignIn(username, password);
        }

        public List<User> ViewAllUsers()
        {
            List<Doctor> doctors = doctorSevice.ViewDoctors();
            List<User> result = new List<User>();
            result.AddRange(doctors);
            return result;
        }

        public User ViewProfile(string jmbg)
        {
            return doctorSevice.ViewProfile(jmbg);
        }
    }
}