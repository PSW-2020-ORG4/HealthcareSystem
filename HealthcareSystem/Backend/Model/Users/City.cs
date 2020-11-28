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
        public City(int zipCode,string name,Country country)
        {
            ZipCode = zipCode;
            Name = name;
            InitCountry(country);

        }
        public City(City city)
        {
            ZipCode = city.ZipCode;
            Name = city.Name;
            InitCountry(city.Country);
        }

        private void InitCountry(Country country)
        {
            if (country != null)
            {
                Country = new Country(country);
                CountryId = country.Id;
            }
            else
            {
                Country = new Country();
                CountryId = 0;
            }
        }
    }
}