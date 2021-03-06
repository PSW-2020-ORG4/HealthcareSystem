/***********************************************************************
 * Module:  City.cs
 * Author:  Sladjana Savkovic
 * Purpose: Definition of the Class Korisnik.City
 ***********************************************************************/

using System;

namespace Model.Users
{
    public class City
    {
        public string Name { get; set; }
        public int ZipCode { get; set; }

        public City() { }
        public City(string name,int zipCode)
        {
            this.Name = name;
            this.ZipCode = zipCode;
        }
        public City(City city)
        {
            this.Name = city.Name;
            this.ZipCode = city.ZipCode;
        }
    }
}