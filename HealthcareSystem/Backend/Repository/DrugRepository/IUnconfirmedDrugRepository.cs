using Model.Manager;
using System.Collections.Generic;

namespace Backend.Repository.DrugRepository
{
    public interface IUnconfirmedDrugRepository
    {
        int getLastId();
        Drug GetDrugById(int id);
        List<Drug> GetAllDrugs();
        Drug SetDrug(Drug drug);
        void DeleteDrug(int id);
        Drug AddDrug(Drug drug);

    }
}
