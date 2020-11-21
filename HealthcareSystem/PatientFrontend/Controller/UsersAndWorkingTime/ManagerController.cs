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
        public ManagerService managerService = new ManagerService();

        public bool DeleteProfile(string jmbg)
        {
            return managerService.DeleteProfile(jmbg);
        }

        public User EditProfile(User user)
        {
            return managerService.EditProfile((Manager)user);
        }

        public User Register(User user)
        {
            return null;
        }

        public User SignIn(string username, string password)
        {
            return managerService.SignIn(username,password);
        }

        public List<User> ViewAllUsers()
        {
            return null;
        }

        public User ViewProfile(string jmbg)
        {
            return managerService.ViewProfile(jmbg);
        }
    }
}