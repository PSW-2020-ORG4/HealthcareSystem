/***********************************************************************
 * Module:  User.cs
 * Author:  Sladjana Savkovic
 * Purpose: Definition of the Class Korisnik.User
 ***********************************************************************/

using Model.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Model.Users
{
   public abstract class User
   {             
        [Key]
        public string Jmbg { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public GenderType Gender { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }

        [ForeignKey("City")]
        public int CityZipCode { get; set; }
        public virtual City City { get; set; }
        public string HomeAddress { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
    }
}