/***********************************************************************
 * Module:  DrugTypeAndIngridientService.cs
 * Author:  Dragana Carapic
 * Purpose: Definition of the Class Service.Examination&Drug&PatientCard&TherapyService.DrugTypeAndIngridientService
 ***********************************************************************/

using Model.Manager;
using Repository;
using System;
using System.Collections.Generic;

namespace Service.DrugAndTherapy
{
    public class DrugTypeAndIngridientService
   {
        private IngridientRepository ingridientRepository = new IngridientRepository();
        private DrugTypeRepository drugTypeRepository = new DrugTypeRepository();
        public List<Ingredient> ViewIngridients()
      {
            // TODO: implement
            return ingridientRepository.GetAllIngridients();
      }
      
      public List<DrugType> ViewDrugTypes()
      {
            // TODO: implement
            return drugTypeRepository.GetAllDrugTypes();
      }
   
   
   }
}