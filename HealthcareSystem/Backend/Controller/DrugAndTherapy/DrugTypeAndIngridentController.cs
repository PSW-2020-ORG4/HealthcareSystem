/***********************************************************************
 * Module:  DrugTypeAndIngridentController.cs
 * Author:  Jelena Budisa
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
        private DrugTypeAndIngridientService _drugTypeAndIngridientService;
        public List<Ingredient> ViewIngridients()
        {
            // TODO: implement
            return _drugTypeAndIngridientService.ViewIngridients();
        }

        public List<DrugType> ViewDrugTypes()
        {
            // TODO: implement
            return _drugTypeAndIngridientService.ViewDrugTypes();
        }

    }
}