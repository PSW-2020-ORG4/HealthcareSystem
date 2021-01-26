using Model.Manager;
using System.Collections.Generic;

namespace Backend.Service.DrugAndTherapy
{
    public interface IDrugTypeService
    {

        DrugType AddDrugType(DrugType drugType);
        DrugType GetDrugTypeById(int id);
        List<DrugType> GetDrugTypes();
        DrugType UpdateDrugType(DrugType drugType);
        void DeleteDrugType(int id);
    }
}
