using Model.Manager;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Backend.Repository.DrugRepository
{
    public interface IUnconfirmedDrugRepository
    {
        int getLastId();
        Drug GetDrug(int id);
        List<Drug> GetAllDrugs();
        void UpdateDrug(Drug drug);
        void DeleteDrug(int id);
        void AddDrug(Drug drug);

    }
}
