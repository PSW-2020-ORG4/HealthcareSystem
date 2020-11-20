/***********************************************************************
 * Module:  IngridientRepository.cs
 * Author:  Dragana Carapic
 * Purpose: Definition of the Class Repository.IngridientRepository
 ***********************************************************************/

using Backend.Repository.IngridientRepository;
using Model.Manager;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;

namespace Repository
{
    public class IngridientRepository : IIngridientRepository
    {
        private string path;

        public IngridientRepository()
        {

            string fileName = "ingridient.json";
            path = Path.GetFullPath(fileName);

        }
        public List<Ingredient> GetAllIngridients()
        {
            // TODO: implement
            List<Ingredient> ingredientList = ReadFromFile();
            return ingredientList;
        }

        private List<Ingredient> ReadFromFile()
        {
            List<Ingredient> ingredientList;
            if (File.Exists(path))
            {
                string json = File.ReadAllText(path);
                ingredientList = JsonConvert.DeserializeObject<List<Ingredient>>(json);
            }
            else
            {
                ingredientList = new List<Ingredient>();
                WriteInFile(ingredientList);
            }
            return ingredientList;
        }

        private void WriteInFile(List<Ingredient> ingredientList)
        {
            string json = JsonConvert.SerializeObject(ingredientList);
            File.WriteAllText(path, json);
        }


    }
}