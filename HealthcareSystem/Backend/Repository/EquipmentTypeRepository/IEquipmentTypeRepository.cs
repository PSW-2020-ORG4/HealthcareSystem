using Backend.Model.Manager;
using Model.Manager;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Backend.Repository
{
   public interface IEquipmentTypeRepository
    {
        EquipmentType GetEquipmentType(int id);
        List<EquipmentType> GetAllEquipmentTypes();
        EquipmentType UpdateEquipmentType(EquipmentType equipmentType);
        bool DeleteEquipmentType(int id);
        EquipmentType AddEquipmentType(EquipmentType equipmentType);


    }
}
