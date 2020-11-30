using GraphicalEditor.Models;
using Model.Manager;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GraphicalEditor.Service
{
    public class InitializeDatabaseData
    {
        private RoomService _roomService;
        private EquipementService _equipementService;

        public InitializeDatabaseData()
        {
            _roomService = new RoomService();
            _equipementService = new EquipementService();           
        }

        public void InitiliazeData()
        {
            AddRooms();
            AddEquipment();
        }

        public void AddRooms()
        {
            List<MapObject> allMapObjects = MainWindow._allMapObjects;

            foreach (MapObject mapObject in allMapObjects)
            {
                if (mapObject.CheckIfDBAddableRoom())
                    _roomService.AddRoom(mapObject);
            }
        }

        public void AddEquipment()
        {
            List<MapObject> allMapObjects = MainWindow._allMapObjects;

            Random random = new Random();
            bool isConsumable = false;
            int equipmentId = 0;

            foreach (MapObject mapObject in allMapObjects)
            {
                if (mapObject.CheckIfDBAddableRoom())
                {
                    if (!isConsumable)
                    {                        
                        NonConsumableEquipment nonConsumableEquipment = new NonConsumableEquipment(equipmentId++, (TypeOfNonConsumable) random.Next(0, 6));
                        _equipementService.AddNonConsumableEquipment(nonConsumableEquipment);
                        _equipementService.AddNonConsumableEquipmentToRoom(mapObject, nonConsumableEquipment);
                        isConsumable = true;
                    }
                    else
                    {
                        ConsumableEquipment consumableEquipment = new ConsumableEquipment(equipmentId++, random.Next(0, 10), (TypeOfConsumable)random.Next(0, 4));
                        _equipementService.AddConsumableEquipment(consumableEquipment);
                        _equipementService.AddConsumableEquipmentToRoom(mapObject, consumableEquipment);
                        isConsumable = false;
                    }
                }
            }
        }
    }
}
