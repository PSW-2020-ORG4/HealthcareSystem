using Model.Manager;
using System.Collections.Generic;

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
