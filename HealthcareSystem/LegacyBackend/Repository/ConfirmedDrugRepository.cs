/***********************************************************************
 * Module:  ConfirmedDrugRepository.cs
 * Author:  Dragana Carapic
 * Purpose: Definition of the Class Repository.ConfirmedDrugRepository
 ***********************************************************************/

using Model.Manager;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;

namespace Repository
{
   public class ConfirmedDrugRepository
   {
        private string path;

        public ConfirmedDrugRepository() {

            string fileName = "confirmedDrug.json";
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
      
      public Drug SetDrug(Drug drug)
      {
            // TODO: implement
            List<Drug> drugList = ReadFromFile();

            foreach (Drug d in drugList)
            {
                if (d.Id == drug.Id)
                {
                    d.ingredient = drug.ingredient;
                    d.drugType = drug.drugType;
                    d.Name = drug.Name;
                    d.Id = drug.Id;
                    d.Quantity = drug.Quantity;
                    d.ExpirationDate = drug.ExpirationDate;
                    d.Producer = drug.Producer;
                    break;
                }
            }
            WriteInFile(drugList);
            return drug;
        }
      
      public bool DeleteDrug(int id)
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
            if (drugForDelete == null)
            {
                return false;
            }

            drugList.Remove(drugForDelete);
            WriteInFile(drugList);
            return true;
        }
      
      public Drug NewDrug(Drug drug)
      {
            // TODO: implement
            List<Drug> drugList = ReadFromFile();
            Drug searchDrug = GetDrug(drug.Id);
            if (searchDrug != null)
            {
                return null;
            }
            drugList.Add(drug);
            WriteInFile(drugList);
            return drug;
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