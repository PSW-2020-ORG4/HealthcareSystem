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
        Drug GetDrugById(int id);
        List<Drug> GetAllDrugs();
        Drug SetDrug(Drug drug);
        void DeleteDrug(int id);
        Drug AddDrug(Drug drug);

    }
}
