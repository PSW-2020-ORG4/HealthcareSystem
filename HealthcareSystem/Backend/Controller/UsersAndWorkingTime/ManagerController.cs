/***********************************************************************
 * Module:  ManagerController.cs
 * Author:  Sladjana Savkovic
 * Purpose: Definition of the Class Controller.Users&WorkingTimeController.ManagerController
 ***********************************************************************/

using Model.Users;
using Service.UsersAndWorkingTime;
using System;
using System.Collections.Generic;

namespace Controller.UsersAndWorkingTime
{
   public class ManagerController : IUserStrategy
   {
        private ManagerService managerService = new ManagerService();

        public void EditProfile(User user)
        {
            managerService.EditProfile((Manager)user);
        }

        public void Register(User user)
        {
            throw new NotImplementedException();
        }

        public User SignIn(string username, string password)
        {
            return managerService.SignIn(username,password);
        }

        public List<User> ViewAllUsers()
        {
            List<Manager> managers = managerService.ViewManagers();
            List<User> result = new List<User>();
            result.AddRange(managers);
            return result;
        }

        public User ViewProfile(string jmbg)
        {
            return managerService.ViewProfile(jmbg);
        }
    }
}