/***********************************************************************
 * Module:  CityController.cs
 * Author:  Sladjana Savkovic
 * Purpose: Definition of the Class Controller.Users&WorkingTimeController.CityController
 ***********************************************************************/

using Model.Users;
using Service.UsersAndWorkingTime;
using System;
using System.Collections.Generic;

namespace Controller.UsersAndWorkingTime
{
   public class CityController
   {

        private CityService cityService = new CityService();
        public List<City> ViewCities()
        {
            return cityService.ViewCities();
        }
        public City getCityByZipCode(int zipCode)
        {
            return cityService.getCityByZipCode(zipCode);
        }
   }
}