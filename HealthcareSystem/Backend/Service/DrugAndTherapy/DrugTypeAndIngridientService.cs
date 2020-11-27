/***********************************************************************
 * Module:  DrugTypeAndIngridientService.cs
 * Author:  Dragana Carapic
 * Purpose: Definition of the Class Service.Examination&Drug&PatientCard&TherapyService.DrugTypeAndIngridientService
 ***********************************************************************/

using Backend.Repository.DrugTypeRepository;
using Backend.Repository.IngridientRepository;
using Backend.Service.DrugAndTherapy;
using Model.Manager;
using Repository;
using System;
using System.Collections.Generic;

namespace Service.DrugAndTherapy
{
    public class DrugTypeAndIngridientService : IDrugTypeAndIngridientService
    {
        private IDrugTypeRepository _drugTypeRepository;
        private IIngridientRepository _ingridientRepository;

        public DrugTypeAndIngridientService(IDrugTypeRepository drugTypeRepository, IIngridientRepository ingridientRepository)
        {
            _drugTypeRepository = drugTypeRepository;
            _ingridientRepository = ingridientRepository;
        }
        public List<Ingredient> ViewIngridients()
        {
            // TODO: implement
            return _ingridientRepository.GetAllIngridients();
        }

        public List<DrugType> ViewDrugTypes()
        {
            // TODO: implement
            return _drugTypeRepository.GetAllDrugTypes();
        }


    }
}