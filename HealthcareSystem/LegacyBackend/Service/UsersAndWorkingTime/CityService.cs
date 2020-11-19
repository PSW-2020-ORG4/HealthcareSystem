/***********************************************************************
 * Module:  CityService.cs
 * Author:  Sladjana Savkovic
 * Purpose: Definition of the Class Service.Users&WorkingTimeService.CityService
 ***********************************************************************/

using Model.Users;
using Repository;
using System;
using System.Collections.Generic;

namespace Service.UsersAndWorkingTime
{
   public class CityService
   {
        private CityRepository cityRepository = new CityRepository();
        public List<City> ViewCities()
        {
            return cityRepository.GetAllCities();
        }
        public City getCityByZipCode(int zipCode)
        {
            return cityRepository.getCityByZipCode(zipCode);
        }

    }
}