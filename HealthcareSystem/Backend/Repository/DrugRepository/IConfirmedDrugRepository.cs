using Model.Manager;
using System.Collections.Generic;

namespace Backend.Repository.DrugRepository
{
    public interface IConfirmedDrugRepository
    {
        int getLastId();
        Drug GetDrugById(int id);
        List<Drug> GetAllDrugs();
        Drug GetDrugByCode(string code);
        Drug SetDrug(Drug drug);
        void DeleteDrug(int id);
        Drug AddDrug(Drug drug);

    }
}
