using Model.Manager;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Backend.Repository.DrugTypeRepository
{
    public interface IDrugTypeRepository
    {
        DrugType GetDrugType(int id);
        List<DrugType> GetAllDrugTypes();
        DrugType UpdateDrugType(DrugType drugType);
        void DeleteDrugType(int id);
        DrugType AddDrugType(DrugType drugType);
    }
}
