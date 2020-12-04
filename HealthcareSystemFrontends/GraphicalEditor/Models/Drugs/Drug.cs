/***********************************************************************
 * Module:  Drug.cs
 * Author:  Jelena Budisa
 * Purpose: Definition of the Class Drug
 ***********************************************************************/

using System;
using System.Collections.Generic;

namespace GraphicalEditor.Models.Drugs
{
    public class Drug
    {
       
        public int DrugType_Id { get; set; }
        public virtual DrugType DrugType { get; set; }
        public string Name { get; set; }
      
        public int Id { get; set; }
        public int Quantity { get; set; }
        public DateTime ExpirationDate { get; set; }
        public string Producer { get; set; }

         public Drug() { }

        public Drug( DrugType drugType, string name, int id, int quantity, DateTime expirationDate, string producer)
        {
            this.DrugType = drugType;
            this.Name = name;
            this.Id = id;
            this.Quantity = quantity;
            this.ExpirationDate = expirationDate;
            this.Producer = producer;
        }
	
        public Drug( DrugType drugType, string name, int quantity, DateTime expirationDate, string producer)
        {
            this.DrugType = drugType;
            this.Name = name;
            this.Quantity = quantity;
            this.ExpirationDate = expirationDate;
            this.Producer = producer;
        }


        public Drug(Drug drug)
        {
            this.DrugType = drug.DrugType;
            this.Name = drug.Name;
            this.Id = drug.Id;
            this.Quantity = drug.Quantity;
            this.ExpirationDate = drug.ExpirationDate;
            this.Producer = drug.Producer;
        }


    }
}