/***********************************************************************
 * Module:  DrugService.cs
 * Author:  Dragana Carapic
 * Purpose: Definition of the Class Service.DrugService
 ***********************************************************************/

using Model.Manager;
using Repository;
using System;
using System.Collections.Generic;

namespace Service.DrugAndTherapy
{
   public class DrugService
   {
        private ConfirmedDrugRepository confirmedDrugRepository = new ConfirmedDrugRepository();
        private UnconfirmedDrugRepository unconfirmedDrugRepository = new UnconfirmedDrugRepository();
		
		 public int getLastIdConfirmed()
        {
            return confirmedDrugRepository.getLastId();
        }

        public int getLastIdUnconfirmed()
        {
            return unconfirmedDrugRepository.getLastId();
        }
	
      public Drug EditConfirmedDrug(Drug drug)
      {
            // TODO: implement
            return confirmedDrugRepository.SetDrug(drug);
      }
      
      public bool DeleteConfirmedDrug(int id)
      {
            // TODO: implement
            return confirmedDrugRepository.DeleteDrug(id);
      }
      
      public List<Drug> ViewConfirmedDrugs()
      {
            // TODO: implement
            return confirmedDrugRepository.GetAllDrugs();
      }
         
      public void ConfirmDrug(Drug drug)
      {
            // TODO: implement
            TransferUnconfirmedDrug(drug);
            
      }
      
      public Drug EditUnconfirmedDrug(Drug drug)
      {
            // TODO: implement
            return unconfirmedDrugRepository.SetDrug(drug);
      }
      
      public bool DeleteUnconfirmedDrug(int id)
      {
            // TODO: implement
            return unconfirmedDrugRepository.DeleteDrug(id);
      }
      
      public List<Drug> ViewUnconfirmedDrugs()
      {
            // TODO: implement
            return unconfirmedDrugRepository.GetAllDrugs();
      }
      
      public Drug AddDrug(Drug drug)
      {
            // TODO: implement
            foreach (Drug d in confirmedDrugRepository.GetAllDrugs()) {
                if (d.Id == drug.Id) {
                    return confirmedDrugRepository.SetDrug(drug);
                }
            }
            foreach (Drug d in unconfirmedDrugRepository.GetAllDrugs())
            {
                if (d.Id == drug.Id)
                {
                    return unconfirmedDrugRepository.SetDrug(drug);
                }
            }

            return AddUnconfirmedDrug(drug);
      }
      
      public Drug TransferUnconfirmedDrug(Drug drug)
      {
            // TODO: implement
            unconfirmedDrugRepository.DeleteDrug(drug.Id);
            return confirmedDrugRepository.NewDrug(drug);
            
      }
   
      private Drug AddUnconfirmedDrug(Drug drug)
      {
            // TODO: implement
            return unconfirmedDrugRepository.NewDrug(drug);
      }
   
   
   }
}