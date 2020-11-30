/***********************************************************************
 * Module:  DrugTypeAndIngridentController.cs
 * Author:  Dragana Carapic
 * Purpose: Definition of the Class Controller.Examination&Drug&PatientCard&TherapyController.DrugTypeAndIngridentController
 ***********************************************************************/

using Model.Manager;
using Service.DrugAndTherapy;
using System;
using System.Collections.Generic;

namespace Controller.DrugAndTherapy
{
   public class DrugTypeAndIngridentController
   {
        private DrugTypeAndIngridientService drugTypeAndIngridientService = new DrugTypeAndIngridientService();
      public List<Ingredient> ViewIngridients()
      {
            // TODO: implement
            return drugTypeAndIngridientService.ViewIngridients();
      }
      
      public List<DrugType> ViewDrugTypes()
      {
            // TODO: implement
            return drugTypeAndIngridientService.ViewDrugTypes();
      }
          
   }
}