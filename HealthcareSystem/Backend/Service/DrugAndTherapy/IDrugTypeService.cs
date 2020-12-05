using Model.Manager;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
