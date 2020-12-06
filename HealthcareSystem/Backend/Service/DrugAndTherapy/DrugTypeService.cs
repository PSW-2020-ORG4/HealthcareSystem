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
    public class DrugTypeService : IDrugTypeService
    {
        private IDrugTypeRepository _drugTypeRepository;
       

        public DrugTypeService(IDrugTypeRepository drugTypeRepository)
        {
            _drugTypeRepository = drugTypeRepository;
        }

        public DrugType AddDrugType(DrugType drugType)
        {
            return _drugTypeRepository.AddDrugType(drugType);
        }

        public void DeleteDrugType(int id)
        {
             _drugTypeRepository.DeleteDrugType(id);
        }

        public DrugType GetDrugTypeById(int id)
        {
            return _drugTypeRepository.GetDrugType(id);
        }

        public List<DrugType> GetDrugTypes()
        {
            return _drugTypeRepository.GetAllDrugTypes();
        }

        public DrugType UpdateDrugType(DrugType drugType)
        {
            return _drugTypeRepository.UpdateDrugType(drugType);
        }
    }
}