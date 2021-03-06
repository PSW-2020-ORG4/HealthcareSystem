/***********************************************************************
 * Module:  ConsumableEquipmentService.cs
 * Author:  Jelena Budisa
 * Purpose: Definition of the Class Service.ConsumableEquipmentService
 ***********************************************************************/

using System;
using Model.Manager;
using Model.Users;
using Repository;
using System.Collections.Generic;

namespace Service.RoomAndEquipment
{
   public class ConsumableEquipmentService
{
    public Repository.ConsumableEquipmentRepository consumableEquipmentRepository = new ConsumableEquipmentRepository();

    public ConsumableEquipment newConsumableEquipment(ConsumableEquipment equipment)
        {
            return consumableEquipmentRepository.NewEquipment(equipment);
        }



    public List<ConsumableEquipment> ViewConsumableEquipment()
    {
            return consumableEquipmentRepository.GetAllEquipment();
     }

    public Model.Manager.ConsumableEquipment AddConsumableEqipment(Model.Manager.ConsumableEquipment equipment)
    {
            return consumableEquipmentRepository.NewEquipment(equipment);
     }

    public Model.Manager.ConsumableEquipment EditConsumableEquipment(Model.Manager.ConsumableEquipment equipment)
    {
            return consumableEquipmentRepository.SetEquipment(equipment);
     }

    public bool DeleteConsumableEquipment(int id)
    {
            return consumableEquipmentRepository.DeleteEquipment(id);
     }

   

}
}