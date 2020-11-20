/***********************************************************************
 * Module:  DrugTypeRepository.cs
 * Author:  Dragana Carapic
 * Purpose: Definition of the Class Repository.DrugTypeRepository
 ***********************************************************************/

using Backend.Repository.DrugTypeRepository;
using Model.Manager;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;

namespace Repository
{
    public class DrugTypeRepository : IDrugTypeRepository
    {
        private string path;

        public DrugTypeRepository()
        {

            string fileName = "drugType.json";
            path = Path.GetFullPath(fileName);

        }
        public List<DrugType> GetAllDrugTypes()
        {
            // TODO: implement
            List<DrugType> drugTypeList = ReadFromFile();
            return drugTypeList;
        }

        private List<DrugType> ReadFromFile()
        {
            List<DrugType> drugTypeList;
            if (File.Exists(path))
            {
                string json = File.ReadAllText(path);
                drugTypeList = JsonConvert.DeserializeObject<List<DrugType>>(json);
            }
            else
            {
                drugTypeList = new List<DrugType>();
                WriteInFile(drugTypeList);
            }
            return drugTypeList;
        }

        private void WriteInFile(List<DrugType> drugTypeList)
        {
            string json = JsonConvert.SerializeObject(drugTypeList);
            File.WriteAllText(path, json);
        }

    }
}