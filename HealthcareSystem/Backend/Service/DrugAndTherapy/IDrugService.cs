using Model.Manager;
using System.Collections.Generic;

namespace Backend.Service.DrugAndTherapy
{
    public interface IDrugService
    {
        int getLastIdConfirmed();
        int getLastIdUnconfirmed();
        void UpdateConfirmedDrug(Drug drug);
        void DeleteConfirmedDrug(int id);
        List<Drug> ViewConfirmedDrugs();
        void ConfirmDrug(Drug drug);
        void UpdateUnconfirmedDrug(Drug drug);
        void DeleteUnconfirmedDrug(int id);
        List<Drug> ViewUnconfirmedDrugs();
        void AddDrug(Drug drug);
        public List<Drug> GetDrugsByRoomNumber(int roomNumber);
        void AddConfirmedDrug(Drug drug);
        List<Drug> GetDrugWithRoomForSearchTerm(string searchTerm);
        void AddDrugQuantity(string code, int quantity);
    }
}
