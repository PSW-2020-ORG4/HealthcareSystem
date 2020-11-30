/***********************************************************************
 * Module:  User.cs
 * Author:  Sladjana Savkovic
 * Purpose: Definition of the Class Korisnik.User
 ***********************************************************************/

using System;

namespace Model.Users
{
   public abstract class User
   {     
        public string Jmbg { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public GenderType Gender { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public City City { get; set; }
        public string HomeAddress { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }

    }
}