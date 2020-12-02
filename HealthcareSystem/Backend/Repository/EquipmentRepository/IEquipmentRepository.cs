using Model.Manager;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Backend.Repository
{
   public interface IEquipmentRepository
    {
        Equipment GetEquipment(int id);
        List<Equipment> GetAllEquipment();
        Equipment SetEquipment(Equipment equipment);
        bool DeleteEquipment(int id);
        Equipment NewEquipment(Equipment equipment);


    }
}
