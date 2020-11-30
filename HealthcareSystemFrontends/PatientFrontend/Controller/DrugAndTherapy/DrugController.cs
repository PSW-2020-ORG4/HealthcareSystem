/***********************************************************************
 * Module:  DrugController.cs
 * Author:  Dragana Carapic
 * Purpose: Definition of the Class Controller.DrugController
 ***********************************************************************/

using Model.Manager;
using Service.DrugAndTherapy;
using System;
using System.Collections.Generic;

namespace Controller.DrugAndTherapy
{
   public class DrugController
   {
        private DrugService drugService = new DrugService();
		
		 public int getLastIdConfirmed()
        {
            return drugService.getLastIdConfirmed();
        }

        public int getLastIdUnconfirmed()
        {
            return drugService.getLastIdUnconfirmed();
        }
		
      public Drug AddDrug(Drug drug)
      {
            // TODO: implement
            return drugService.AddDrug(drug);
      }
      
      public Drug EditConfirmedDrug(Drug drug)
      {
            // TODO: implement
            return drugService.EditConfirmedDrug(drug);
      }
      
      public bool DeleteConfirmedDrug(int id)
      {
            // TODO: implement
            return drugService.DeleteConfirmedDrug(id);
      }
      
      public List<Drug> ViewConfirmedDrugs()
      {
            // TODO: implement
            return drugService.ViewConfirmedDrugs();
      }
      
      public void ConfirmDrug(Drug drug)
      {
            // TODO: implement
            drugService.ConfirmDrug(drug);
      }
      
      public Drug EditUnconfirmedDrug(Drug drug)
      {
            // TODO: implement
            return drugService.EditUnconfirmedDrug(drug);
      }
      
      public bool DeleteUnconfirmedDrug(int id)
      {
            // TODO: implement
            return drugService.DeleteUnconfirmedDrug(id);
      }
      
      public List<Drug> ViewUnconfirmedDrugs()
      {
            // TODO: implement
            return drugService.ViewUnconfirmedDrugs();
      }
      
      public Drug TransferUnconfirmedDrug(Drug drug)
      {
            // TODO: implement
            return drugService.TransferUnconfirmedDrug(drug);
      }     
   
   }
}