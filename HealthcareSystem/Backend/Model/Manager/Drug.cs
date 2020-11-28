/***********************************************************************
 * Module:  Drug.cs
 * Author:  Jelena Budisa
 * Purpose: Definition of the Class Drug
 ***********************************************************************/

using Castle.Components.DictionaryAdapter;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics;
using System.Drawing.Printing;

namespace Model.Manager
{
    public class Drug
    {
        public virtual List<Ingredient> Ingredient { get; set; }
        public virtual DrugType DrugType { get; set; }
        public string Name { get; set; }
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public int Quantity { get; set; }
        public DateTime ExpirationDate { get; set; }
        public string Producer { get; set; }

        public Drug() { }

        public Drug(List<Ingredient> ingredients, DrugType drugType, string name, int id, int quantity, DateTime expirationDate, string producer)
        {
            if (ingredients == null)
            {
                this.Ingredient = new List<Ingredient>();
            }
            else
            {
                this.Ingredient = ingredients;
            }
            if (drugType == null)
            {
                this.DrugType = new DrugType();
            }
            else
            {
                this.DrugType = new DrugType(drugType);
            }
            this.Name = name;
            this.Id = id;
            this.Quantity = quantity;
            this.ExpirationDate = expirationDate;
            this.Producer = producer;
        }

        public Drug(Drug drug)
        {
            if (drug.Ingredient == null)
            {
                this.Ingredient = new List<Ingredient>();
            }
            else
            {
                this.Ingredient = drug.Ingredient;
            }
            if (drug.DrugType == null)
            {
                this.DrugType = new DrugType();
            }
            else
            {
                this.DrugType = new DrugType(drug.DrugType);
            }
            this.Name = drug.Name;
            this.Id = drug.Id;
            this.Quantity = drug.Quantity;
            this.ExpirationDate = drug.ExpirationDate;
            this.Producer = drug.Producer;
        }

    }
}