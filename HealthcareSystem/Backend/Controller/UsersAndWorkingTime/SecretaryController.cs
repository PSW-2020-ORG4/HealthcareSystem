/***********************************************************************
 * Module:  SecretaryController.cs
 * Author:  Sladjana Savkovic
 * Purpose: Definition of the Class Controller.Users&WorkingTimeController.SecretaryController
 ***********************************************************************/

using Model.Users;
using Service.UsersAndWorkingTime;
using System;
using System.Collections.Generic;

namespace Controller.UsersAndWorkingTime
{
   public class SecretaryController : IUserStrategy
   {

        private SecretaryService secretaryService = new SecretaryService();
        public User SignIn(string username, string password)
        {
            return secretaryService.SignIn(username,password);
        }
      
        public void Register(User user)
        {
            secretaryService.RegisterSecretary((Secretary)user);
        }
      
        public void EditProfile(User user)
        {
            secretaryService.EditProfile((Secretary)user);
        }
      
        public User ViewProfile(string jmbg)
        {
            return secretaryService.ViewProfile(jmbg);
        }
      
        public List<User> ViewAllUsers()
        {
            List<Secretary> secretaries = secretaryService.ViewSecretaries();
            List<User> result = new List<User>();
            result.AddRange(secretaries);
            return result;
        }
   
   
   }
}