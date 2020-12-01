/***********************************************************************
 * Module:  Ingredient.cs
 * Author:  Dragana Carapic
 * Purpose: Definition of the Class Manager.Ingredient
 ***********************************************************************/

using System;

namespace Model.Manager
{
   public class Ingredient
   {
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