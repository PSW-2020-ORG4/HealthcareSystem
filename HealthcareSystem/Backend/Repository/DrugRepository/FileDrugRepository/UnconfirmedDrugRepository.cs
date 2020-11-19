/***********************************************************************
 * Module:  UnconfirmedDrugRepository.cs
 * Author:  Dragana Carapic
 * Purpose: Definition of the Class Repository.UnconfirmedDrugRepository
 ***********************************************************************/

using Backend.Repository.DrugRepository;
using Model.Manager;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;

namespace Repository
{
    public class UnconfirmedDrugRepository : IUnconfirmedDrugRepository
    {
        private string path;

        public UnconfirmedDrugRepository()
        {

            string fileName = "unconfirmedDrug.json";
            path = Path.GetFullPath(fileName);

        }

        public int getLastId()
        {
            List<Drug> drugs = ReadFromFile();
            if (drugs.Count == 0)
            {
                return 0;
            }
            return drugs[drugs.Count - 1].Id;
        }

        public Drug GetDrug(int id)
        {
            // TODO: implement
            List<Drug> drugList = ReadFromFile();
            foreach (Drug d in drugList)
            {
                if (d.Id == id)
                {
                    return d;
                }
            }
            return null;

        }

        public List<Drug> GetAllDrugs()
        {
            // TODO: implement
            List<Drug> drugList = ReadFromFile();
            return drugList;
        }

        public void UpdateDrug(Drug drug)
        {
            // TODO: implement
            List<Drug> drugList = ReadFromFile();

            foreach (Drug d in drugList)
            {
                if (d.Id == drug.Id)
                {
                    d.Ingredient = drug.Ingredient;
                    d.DrugType = drug.DrugType;
                    d.Name = drug.Name;
                    d.Id = drug.Id;
                    d.Quantity = drug.Quantity;
                    d.ExpirationDate = drug.ExpirationDate;
                    d.Producer = drug.Producer;
                    break;
                }
            }
            WriteInFile(drugList);
        }

        public void DeleteDrug(int id)
        {
            // TODO: implement
            List<Drug> drugList = ReadFromFile();
            Drug drugForDelete = null;
            foreach (Drug d in drugList)
            {
                if (d.Id == id)
                {
                    drugForDelete = d;
                    break;
                }
            }
            drugList.Remove(drugForDelete);
            WriteInFile(drugList);

        }

        public void AddDrug(Drug drug)
        {
            // TODO: implement
            List<Drug> drugList = ReadFromFile();
            Drug searchDrug = GetDrug(drug.Id);
            drugList.Add(drug);
            WriteInFile(drugList);
        }

        private List<Drug> ReadFromFile()
        {
            List<Drug> drugList;
            if (File.Exists(path))
            {
                string json = File.ReadAllText(path);
                drugList = JsonConvert.DeserializeObject<List<Drug>>(json);
            }
            else
            {
                drugList = new List<Drug>();
                WriteInFile(drugList);
            }
            return drugList;
        }

        private void WriteInFile(List<Drug> drugList)
        {
            string json = JsonConvert.SerializeObject(drugList);
            File.WriteAllText(path, json);
        }

    }
}