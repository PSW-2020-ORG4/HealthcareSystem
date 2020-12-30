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
        Drug GetDrugById(int id);
        List<Drug> GetAllDrugs();
        Drug GetDrugByCode(string code);
        Drug SetDrug(Drug drug);
        void DeleteDrug(int id);
        Drug AddDrug(Drug drug);

    }
}
