using Model.Manager;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Backend.Repository
{
   public interface INonConsumableEquipmentRepository
    {
        NonConsumableEquipment GetEquipment(int id);
        List<NonConsumableEquipment> GetAllEquipment();
        NonConsumableEquipment SetEquipment(NonConsumableEquipment equipment);
        bool DeleteEquipment(int id);
        NonConsumableEquipment NewEquipment(NonConsumableEquipment equipment);
    

    }
}
