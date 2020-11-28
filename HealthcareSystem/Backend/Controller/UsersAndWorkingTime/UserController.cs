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
         private IUserStrategy Strategy { get; set; }

         public UserController(IUserStrategy strategy)
         {
            this.Strategy = strategy;
         }

         public User SignIn(string username, string password)
         {
            return this.Strategy.SignIn(username,password);
         }

        public void Register(User user)
        {
            Strategy.Register(user);
        }
        public void EditProfile(User user)
        {
            Strategy.EditProfile(user);
        }
        public User ViewProfile(string jmbg)
        {
            return this.Strategy.ViewProfile(jmbg);
        }
        public List<User> ViewAllUsers()
        {
            return this.Strategy.ViewAllUsers();
        }
    }
}