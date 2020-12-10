/***********************************************************************
 * Module:  DrugService.cs
 * Author:  Dragana Carapic
 * Purpose: Definition of the Class Service.DrugService
 ***********************************************************************/

using Backend.Model.Exceptions;
using Backend.Model.Manager;
using Backend.Repository.DrugInRoomRepository;
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
	    private IDrugInRoomRepository _drugInRoomRepository;

        public DrugService(IConfirmedDrugRepository confirmedDrugRepository, IUnconfirmedDrugRepository unconfirmedDrugRepository,IDrugInRoomRepository drugInRoomRepository)
        {
            _confirmedDrugRepository = confirmedDrugRepository;
            _unconfirmedDrugRepository = unconfirmedDrugRepository;
	        _drugInRoomRepository = drugInRoomRepository;
        }

        public int getLastIdConfirmed()
        {
            int id = _confirmedDrugRepository.getLastId();
            if (id == 0)
                throw new NotFoundException("Confirmed drugs do not exist in database.");
            return id;
        }

        public int getLastIdUnconfirmed()
        {
            int id = _unconfirmedDrugRepository.getLastId();
            if (id == 0)
                throw new NotFoundException("Unconfirmed drugs do not exist in database.");
            return id;
        }

        public void UpdateConfirmedDrug(Drug drug)
        {
            // TODO: implement
            _confirmedDrugRepository.SetDrug(drug);
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
            _unconfirmedDrugRepository.SetDrug(drug);
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
                    _confirmedDrugRepository.SetDrug(drug);
                }
            }
            foreach (Drug d in _unconfirmedDrugRepository.GetAllDrugs())
            {
                if (d.Id == drug.Id)
                {
                    _unconfirmedDrugRepository.SetDrug(drug);
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
	
	    public void AddConfirmedDrug(Drug drug)
        {

            _confirmedDrugRepository.AddDrug(drug);
        }

        public List<Drug> GetDrugsByRoomNumber(int roomNumber)
        {
            List<DrugInRoom> drugsInRoom = _drugInRoomRepository.GetDrugsInRoomByRoomNumber(roomNumber);
            List<Drug> confirmedDrugsInRoom = new List<Drug>();
            foreach (DrugInRoom drugInRoom in drugsInRoom)
            {
                Drug confimredDrug = _confirmedDrugRepository.GetDrugById(drugInRoom.DrugId);
                if (confimredDrug != null)
                {
                    confirmedDrugsInRoom.Add(confimredDrug);
                }
            }
            return confirmedDrugsInRoom;
        }

        public List<Drug> GetDrugWithRoomForSearchTerm(string searchTerm)
        {
            List<Drug> drugs = ViewConfirmedDrugs();
            List<Drug> validEDrugs = new List<Drug>();
            foreach (Drug d in drugs)
            {
                if (CheckIfDrugNameContainsSearchTerm(d, searchTerm))
                    validEDrugs.Add(d);
            }

            return validEDrugs;
        }

        private bool CheckIfDrugNameContainsSearchTerm(Drug drug, string searchTerm)
        {
            if (drug.Name.ToString().ToLower().Contains(searchTerm.ToLower()))
                return true;
            else return false;
        }

    }
}