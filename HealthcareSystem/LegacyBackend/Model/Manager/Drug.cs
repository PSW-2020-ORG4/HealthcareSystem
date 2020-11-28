/***********************************************************************
 * Module:  Drug.cs
 * Author:  Dragana Carapic
 * Purpose: Definition of the Class Drug
 ***********************************************************************/

using System;
using System.Collections;
using System.Collections.Generic;

namespace Model.Manager
{
   public class Drug
   {
        public List<Ingredient> ingredient { get; set; }
        public DrugType drugType { get; set; }
        public string Name { get; set; }
        public int Id { get; set; }
        public int Quantity { get; set; }
        public DateTime ExpirationDate { get; set; }
        public string Producer { get; set; }

        public Drug() { }

        public Drug(List<Ingredient> ingredients, DrugType drugType, string name, int id, int quantity, DateTime expirationDate, string producer)
        {
            if (ingredients == null)
            {
                this.ingredient = new List<Ingredient>();
            }
            else
            {
                this.ingredient = ingredients;
            }
            if (drugType == null)
            {
                this.drugType = new DrugType();
            }
            else
            {
                this.drugType = new DrugType(drugType);
            }
            this.Name = name;
            this.Id = id;
            this.Quantity = quantity;
            this.ExpirationDate = expirationDate;
            this.Producer = producer;
        }

        public Drug(Drug drug)
        {
            if (drug.ingredient == null)
            {
                this.ingredient = new List<Ingredient>();
            }
            else
            {
                this.ingredient = drug.ingredient;
            }
            if (drug.drugType == null)
            {
                this.drugType = new DrugType();
            }
            else
            {
                this.drugType = new DrugType(drug.drugType);
            }
            this.Name = drug.Name;
            this.Id = drug.Id;
            this.Quantity = drug.Quantity;
            this.ExpirationDate = drug.ExpirationDate;
            this.Producer = drug.Producer;
        }

    }
}