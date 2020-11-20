/***********************************************************************
 * Module:  CityRepository.cs
 * Author:  Sladjana Savkovic
 * Purpose: Definition of the Class Repository.CityRepository
 ***********************************************************************/

using Model.Users;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;

namespace Repository
{
   public class CityRepository
   {

        private string path;

        public CityRepository()
        {
            string fileName = "cities.json";
            path = Path.GetFullPath(fileName);
        }
        public List<City> GetAllCities()
          {
             List<City> cityList = ReadFromFile();
             return cityList;
        }

        public City getCityByZipCode(int zipCode)
        {
            List<City> cities = ReadFromFile();
            foreach (City c in cities)
            {
                if (c.ZipCode == zipCode)
                {
                    return c;
                }
            }
            return null;
        }

        private List<City> ReadFromFile()
        {
            List<City> cityList;
            if (File.Exists(path))
            {
                string json = File.ReadAllText(path);
                cityList = JsonConvert.DeserializeObject<List<City>>(json);
            }
            else
            {
                cityList = new List<City>();
                WriteInFile(cityList);
            }
            return cityList;
        }

        private void WriteInFile(List<City> cityList)
        {
            string json = JsonConvert.SerializeObject(cityList);
            File.WriteAllText(path, json);
        }
    }
}