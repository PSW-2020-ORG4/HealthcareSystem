/***********************************************************************
 * Module:  City.cs
 * Author:  Sladjana Savkovic
 * Purpose: Definition of the Class Korisnik.City
 ***********************************************************************/

using Backend;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Model.Users
{
    public class City
    {
        [Key]
        public int ZipCode { get; set; }
        public string Name { get; set; }

        [ForeignKey("Country")]
        public int CountryId { get; set; }
        public virtual Country Country { get; set; }
        public virtual ICollection<User> Users { get; set; }

        public City() { }
        public City(string name,int zipCode)
        {
            Name = name;
            ZipCode = zipCode;
        }
        public City(City city)
        {
            Name = city.Name;
            ZipCode = city.ZipCode;
        }
    }
}