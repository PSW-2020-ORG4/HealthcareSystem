﻿using Model.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Backend.Repository
{
    public interface ICityRepository
    {
	// trigger build
        List<City> GetCities();
        List<City> GetCitiesByCountryId(int countryId);
    }
}
