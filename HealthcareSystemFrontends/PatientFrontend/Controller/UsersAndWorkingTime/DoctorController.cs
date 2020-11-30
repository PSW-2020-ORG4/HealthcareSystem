/***********************************************************************
 * Module:  DoctorController.cs
 * Author:  Sladjana Savkovic
 * Purpose: Definition of the Class Controller.Users&WorkingTimeController.DoctorController
 ***********************************************************************/

using Model.Users;
using Service.UsersAndWorkingTime;
using Syncfusion.Windows.Shared;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Controller.UsersAndWorkingTime
{
   public class DoctorController : IUserStrategy
   {

        public DoctorService doctorSevice = new DoctorService();

        public bool DeleteProfile(string jmbg)
        {
            return doctorSevice.DeleteDoctor(jmbg);
        }

        public User EditProfile(User user)
        {
            return doctorSevice.EditDoctor((Doctor)user);
        }

        public User Register(User user)
        {
            return doctorSevice.RegisterDoctor((Doctor)user);
        }

        public User SignIn(string username, string password)
        {
            return doctorSevice.SignIn(username,password);
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