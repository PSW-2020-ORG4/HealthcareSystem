/***********************************************************************
 * Module:  Ingredient.cs
 * Author:  Dragana Carapic
 * Purpose: Definition of the Class Manager.Ingredient
 ***********************************************************************/

using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Model.Manager
{
   public class Ingredient
   {
      [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
      public int Id { get; set; }
      public string Name { get; set; }
      public bool Alergen { get; set; }

        public Ingredient() {
            this.Alergen = false;
        }

        public Ingredient(string name, bool alergen)
        {
            this.Name = name;
            this.Alergen = alergen;
        }

        public Ingredient(Ingredient ingredient)
        {
            this.Name = ingredient.Name;
            this.Alergen = ingredient.Alergen;
        }
}
}