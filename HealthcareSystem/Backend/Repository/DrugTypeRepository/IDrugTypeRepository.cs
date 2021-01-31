using Model.Manager;
using System.Collections.Generic;

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
