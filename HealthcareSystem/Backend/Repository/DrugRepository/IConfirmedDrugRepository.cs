using System;
using Model.Users;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model.Manager;

namespace Backend.Repository.DrugRepository
{
    public interface IConfirmedDrugRepository
    {
        int getLastId();
        Drug GetDrug(int id);
        List<Drug> GetAllDrugs();
        void UpdateDrug(Drug drug);
        void DeleteDrug(int id);
        void AddDrug(Drug drug);

    }
}
