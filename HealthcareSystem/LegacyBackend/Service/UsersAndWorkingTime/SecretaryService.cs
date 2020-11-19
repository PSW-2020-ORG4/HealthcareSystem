/***********************************************************************
 * Module:  SecretaryService.cs
 * Author:  Sladjana Savkovic
 * Purpose: Definition of the Class Service.SecretaryService
 ***********************************************************************/

using Model.Users;
using Repository;
using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace Service.UsersAndWorkingTime
{
   public class SecretaryService
   {

        private SecretaryRepository secretaryRepository = new SecretaryRepository();

        public Secretary RegisterSecretary(Secretary secretary)
        {
            if(!IsUsernameValid(secretary.Username) || !IsPasswordValid(secretary.Password))
            {
                return null;
            }
            return secretaryRepository.NewSecretary(secretary);
        }
      
        public Secretary EditProfile(Secretary secretary)
        {
            if (!IsUsernameValid(secretary.Username) || !IsPasswordValid(secretary.Password))
            {
                return null;
            }
            return secretaryRepository.SetSecretary(secretary);
        }
      
        public Secretary ViewProfile(string jmbg)
        {
            return secretaryRepository.GetSecretary(jmbg);
        }

        public List<Secretary> ViewSecretaries()
        {
            return secretaryRepository.GetAllSecretaries();
        }

        public bool DeleteProfile(string jmbg)
        {
            return secretaryRepository.DeleteSecretary(jmbg);
        }
      
        public  Secretary SignIn(string username, string password)
        {
            return secretaryRepository.CheckUsernameAndPassword(username,password);
        }
   
        private  bool IsUsernameValid(string username)
        {
            Regex regex = new Regex(@"^[a-zA-Z0-9\.\-_]{5,13}$");
            Match match = regex.Match(username);
            
            return match.Success;
        }
      
        private  bool IsPasswordValid(string password)
        {
            Regex regex = new Regex(@"^[a-zA-Z0-9\.\-_]{8,30}$");
            Match match = regex.Match(password);

            return match.Success;
        }
   
   
   }
}