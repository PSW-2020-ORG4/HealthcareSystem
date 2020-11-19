/***********************************************************************
 * Module:  DrugService.cs
 * Author:  Dragana Carapic
 * Purpose: Definition of the Class Service.DrugService
 ***********************************************************************/

using Backend.Repository.DrugRepository;
using Backend.Service.DrugAndTherapy;
using Model.Manager;
using Repository;
using System;
using System.Collections.Generic;

namespace Service.DrugAndTherapy
{
    public class DrugService : IDrugService
    {
        private IConfirmedDrugRepository _confirmedDrugRepository;
        private IUnconfirmedDrugRepository _unconfirmedDrugRepository;

        public DrugService(IConfirmedDrugRepository confirmedDrugRepository, IUnconfirmedDrugRepository unconfirmedDrugRepository)
        {
            _confirmedDrugRepository = confirmedDrugRepository;
            _unconfirmedDrugRepository = unconfirmedDrugRepository;
        }

        public int getLastIdConfirmed()
        {
            return _confirmedDrugRepository.getLastId();
        }

        public int getLastIdUnconfirmed()
        {
            return _unconfirmedDrugRepository.getLastId();
        }

        public void UpdateConfirmedDrug(Drug drug)
        {
            // TODO: implement
            _confirmedDrugRepository.UpdateDrug(drug);
        }

        public void DeleteConfirmedDrug(int id)
        {
            // TODO: implement
            _confirmedDrugRepository.DeleteDrug(id);
        }

        public List<Drug> ViewConfirmedDrugs()
        {
            // TODO: implement
            return _confirmedDrugRepository.GetAllDrugs();
        }

        public void ConfirmDrug(Drug drug)
        {
            // TODO: implement
            TransferUnconfirmedDrug(drug);

        }

        public void UpdateUnconfirmedDrug(Drug drug)
        {
            // TODO: implement
            _unconfirmedDrugRepository.UpdateDrug(drug);
        }

        public void DeleteUnconfirmedDrug(int id)
        {
            // TODO: implement
            _unconfirmedDrugRepository.DeleteDrug(id);
        }

        public List<Drug> ViewUnconfirmedDrugs()
        {
            // TODO: implement
            return _unconfirmedDrugRepository.GetAllDrugs();
        }

        public void AddDrug(Drug drug)
        {
            // TODO: implement
            foreach (Drug d in _confirmedDrugRepository.GetAllDrugs())
            {
                if (d.Id == drug.Id)
                {
                    _confirmedDrugRepository.UpdateDrug(drug);
                }
            }
            foreach (Drug d in _unconfirmedDrugRepository.GetAllDrugs())
            {
                if (d.Id == drug.Id)
                {
                    _unconfirmedDrugRepository.UpdateDrug(drug);
                }
            }

            AddUnconfirmedDrug(drug);
        }

        public Drug TransferUnconfirmedDrug(Drug drug)
        {
            // TODO: implement
            _unconfirmedDrugRepository.DeleteDrug(drug.Id);
            _confirmedDrugRepository.AddDrug(drug);
            return drug;
        }

        private void AddUnconfirmedDrug(Drug drug)
        {
            // TODO: implement
            _unconfirmedDrugRepository.AddDrug(drug);
        }


    }
}