using GraphicalEditor.Models;
using GraphicalEditor.Models.Equipment;
using System;
using System.Collections.Generic;

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
            int equipmentId = 0;

            foreach (MapObject mapObject in allMapObjects)
            {
                if (mapObject.CheckIfDBAddableRoom())
                {
                    Equipment singleEquipment = new Equipment(random.Next(0, 10), (TypeOfEquipment)random.Next(0, 9));
                    singleEquipment.Id =Int32.Parse(_equipementService.AddEquipment(singleEquipment));
                    _equipementService.AddEquipmentToRoom(mapObject, singleEquipment);
                }
            }
        }
    }
}

