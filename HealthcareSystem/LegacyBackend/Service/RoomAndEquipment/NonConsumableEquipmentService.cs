/***********************************************************************
 * Module:  NonConsumableEquipmentService.cs
 * Author:  Sladjana Savkovic
 * Purpose: Definition of the Class Service.Room&EquipmentService.NonConsumableEquipmentService
 ***********************************************************************/


using System;
using Model.Manager;
using Model.Users;
using Repository;
using System.Collections.Generic;

namespace Service.RoomAndEquipment
{
   public class NonConsumableEquipmentService
{
    private NonConsumableEquipmentRepository nonConsumableEquipmentRepository = new NonConsumableEquipmentRepository();

        public NonConsumableEquipment newNonConsumableEquipment(NonConsumableEquipment equipment)
        {
            return nonConsumableEquipmentRepository.NewEquipment(equipment);
        }
        public List<NonConsumableEquipment> ViewNonConsumableEquipment()
    {
            return nonConsumableEquipmentRepository.GetAllEquipment();
     }

    public Model.Manager.NonConsumableEquipment EditNonConsumableEquipment(Model.Manager.NonConsumableEquipment equipment)
    {
            return nonConsumableEquipmentRepository.SetEquipment(equipment);
     }

    public bool DeleteNonConsumableEquipment(int id)
    {
            return nonConsumableEquipmentRepository.DeleteEquipment(id);
     }

   

}
}