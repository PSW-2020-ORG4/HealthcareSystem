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

        //PowerDesigner generated methods
        /*
        /// <pdGenerated>default getter</pdGenerated>
        public ArrayList GetIngredient()
        {
        if (ingredient == null)
        {
            ingredient = new ArrayList();
        }
            
         return ingredient;
        }
      
      /// <pdGenerated>default setter</pdGenerated>
      public void SetIngredient(ArrayList newIngredient)
      {
         RemoveAllIngredient();
         foreach (Ingredient oIngredient in newIngredient)
            AddIngredient(oIngredient);
      }
      
      /// <pdGenerated>default Add</pdGenerated>
      public void AddIngredient(Ingredient newIngredient)
      {
         if (newIngredient == null)
            return;
         if (this.ingredient == null)
            this.ingredient = new ArrayList();
         if (!this.ingredient.Contains(newIngredient))
            this.ingredient.Add(newIngredient);
      }
      
      /// <pdGenerated>default Remove</pdGenerated>
      public void RemoveIngredient(Ingredient oldIngredient)
      {
         if (oldIngredient == null)
            return;
         if (this.ingredient != null)
            if (this.ingredient.Contains(oldIngredient))
               this.ingredient.Remove(oldIngredient);
      }
      
      /// <pdGenerated>default removeAll</pdGenerated>
      public void RemoveAllIngredient()
      {
         if (ingredient != null)
            ingredient.Clear();
      }
      */

    }
}