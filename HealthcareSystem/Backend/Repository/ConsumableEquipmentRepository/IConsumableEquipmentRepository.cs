using Model.Manager;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Backend.Repository
{
   public interface IConsumableEquipmentRepository
    {
        ConsumableEquipment GetEquipment(int id);
        List<ConsumableEquipment> GetAllEquipment();
        ConsumableEquipment SetEquipment(ConsumableEquipment equipment);
        bool DeleteEquipment(int id);
        ConsumableEquipment NewEquipment(ConsumableEquipment equipment);


    }
}
