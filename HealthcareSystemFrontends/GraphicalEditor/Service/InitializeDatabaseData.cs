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

        EquipmentTypeService _equipmentTypeService;

        public InitializeDatabaseData()
        {
            _roomService = new RoomService();
            _equipementService = new EquipementService();
            _equipmentTypeService = new EquipmentTypeService();
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
            int i = 0;
            var types = _equipmentTypeService.GetEquipmentTypes();
            foreach (var type in types)
            {
                Equipment equipment = new Equipment(new Random().Next(0, 50), type);
                _equipementService.AddEquipment(equipment);
            }
            if (types.Count == 0) {
                return;
            }
            foreach (MapObject mapObject in allMapObjects)
            {
                if (mapObject.CheckIfDBAddableRoom())
                {
                    i++;
                    Equipment equipment = new Equipment(new Random().Next(0, 50),types[(i%types.Count)]);
                    equipment.Id =Int32.Parse(_equipementService.AddEquipment(equipment));
                    _equipementService.AddEquipmentToRoom(mapObject, equipment);
                }
            }
        }
    }
}

