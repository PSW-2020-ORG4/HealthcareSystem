/***********************************************************************
 * Module:  ManagerService.cs
 * Author:  Sladjana Savkovic
 * Purpose: Definition of the Class Service.ManagerService
 ***********************************************************************/

using Model.Users;
using Repository;
using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace Service.UsersAndWorkingTime
{
   public class ManagerService
   {

      public ManagerRepository managerRepository = new ManagerRepository();
      public Manager EditProfile(Manager manager)
      {
            if (!IsUsernameValid(manager.Username) || !IsPasswordValid(manager.Password))
            {
                return null;
            }
            return managerRepository.SetManager(manager);
      }
      
      public Manager ViewProfile(string jmbg)
      {
            return managerRepository.GetManager(jmbg);
      }

        public List<Manager> ViewManagers()
        {
            return managerRepository.GetAllManagers();
        }

        public bool DeleteProfile(string jmbg)
      {
            return false;
      }

      public Manager SignIn(string username, string password) 
      {
          return managerRepository.CheckUsernameAndPassword(username,password);
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