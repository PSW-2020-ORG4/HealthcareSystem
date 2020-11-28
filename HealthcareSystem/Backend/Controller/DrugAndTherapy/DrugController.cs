/***********************************************************************
 * Module:  DrugController.cs
 * Author:  Jelena Budisa
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
        private DrugService _drugService;

        public int getLastIdConfirmed()
        {
            return _drugService.getLastIdConfirmed();
        }

        public int getLastIdUnconfirmed()
        {
            return _drugService.getLastIdUnconfirmed();
        }

        public void AddDrug(Drug drug)
        {
            // TODO: implement
            _drugService.AddDrug(drug);
        }

        public void UpdateConfirmedDrug(Drug drug)
        {
            // TODO: implement
            _drugService.UpdateConfirmedDrug(drug);
        }

        public void DeleteConfirmedDrug(int id)
        {
            // TODO: implement
            _drugService.DeleteConfirmedDrug(id);
        }

        public List<Drug> ViewConfirmedDrugs()
        {
            // TODO: implement
            return _drugService.ViewConfirmedDrugs();
        }

        public void ConfirmDrug(Drug drug)
        {
            // TODO: implement
            _drugService.ConfirmDrug(drug);
        }

        public void UpdateUnconfirmedDrug(Drug drug)
        {
            // TODO: implement
            _drugService.UpdateUnconfirmedDrug(drug);
        }

        public void DeleteUnconfirmedDrug(int id)
        {
            // TODO: implement
            _drugService.DeleteUnconfirmedDrug(id);
        }

        public List<Drug> ViewUnconfirmedDrugs()
        {
            // TODO: implement
            return _drugService.ViewUnconfirmedDrugs();
        }

        public Drug TransferUnconfirmedDrug(Drug drug)
        {
            // TODO: implement
            return _drugService.TransferUnconfirmedDrug(drug);
        }

    }
}