/***********************************************************************
 * Module:  UserController.cs
 * Author:  Sladjana Savkovic
 * Purpose: Definition of the Class Controller.Users&WorkingTimeController.UserController
 ***********************************************************************/

using Model.Users;
using System;
using System.Collections.Generic;

namespace Controller.UsersAndWorkingTime
{
    public class UserController
    {
         public IUserStrategy Strategy { get; set; }

         public UserController(IUserStrategy strategy)
         {
            this.Strategy = strategy;
         }

         public User SignIn(string username, string password)
         {
            return this.Strategy.SignIn(username,password);
         }

        public User Register(User user)
        {
            return this.Strategy.Register(user);
        }
        public User EditProfile(User user)
        {
            return this.Strategy.EditProfile(user);
        }
        public User ViewProfile(string jmbg)
        {
            return this.Strategy.ViewProfile(jmbg);
        }
        public bool DeleteProfile(string jmbg)
        {
            return this.Strategy.DeleteProfile(jmbg);
        }
        public List<User> ViewAllUsers()
        {
            return this.Strategy.ViewAllUsers();
        }
    }
}