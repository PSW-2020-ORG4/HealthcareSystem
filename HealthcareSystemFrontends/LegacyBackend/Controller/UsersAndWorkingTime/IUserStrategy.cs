/***********************************************************************
 * Module:  UserStrategy.cs
 * Author:  Sladjana Savkovic
 * Purpose: Definition of the Interface Controller.Users&WorkingTimeController.UserStrategy
 ***********************************************************************/

using Model.Users;
using System;
using System.Collections.Generic;

namespace Controller.UsersAndWorkingTime
{
   public interface IUserStrategy
   {
      User SignIn(string username, string password);
      User Register(User user);
      User EditProfile(User user);
      User ViewProfile(string jmbg);
      bool DeleteProfile(string jmbg);
      List<User> ViewAllUsers();
   }
}