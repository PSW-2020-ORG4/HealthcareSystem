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
        Equipment GetEquipmentById(int id);
        List<Equipment> GetAllEquipment();
        Equipment SetEquipment(Equipment equipment);
        void DeleteEquipment(int id);
        Equipment AddEquipment(Equipment equipment);


    }
}
