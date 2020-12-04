using Backend.Model.Manager;
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
            AddEquipmentTypes();
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
        private void AddEquipmentTypes()
        {
            _equipmentTypeService.AddEquipmentType(new EquipmentType("needle", true));
            _equipmentTypeService.AddEquipmentType(new EquipmentType("bend", true));
            _equipmentTypeService.AddEquipmentType(new EquipmentType("mask", true));
            _equipmentTypeService.AddEquipmentType(new EquipmentType("table", true));

            _equipmentTypeService.AddEquipmentType(new EquipmentType("bed", false));
            _equipmentTypeService.AddEquipmentType(new EquipmentType("table", false));
            _equipmentTypeService.AddEquipmentType(new EquipmentType("computer", false));
            _equipmentTypeService.AddEquipmentType(new EquipmentType("chair", false));
            _equipmentTypeService.AddEquipmentType(new EquipmentType("instrument", false));
        }

        public void AddEquipment()
        {
            List<MapObject> allMapObjects = MainWindow._allMapObjects;

            Random random = new Random();

            var types = _equipmentTypeService.GetEquipmentTypes();
            if (types.Count == 0) {
                return;
            }

            foreach (MapObject mapObject in allMapObjects)
            {
                if (mapObject.CheckIfDBAddableRoom())
                {
                    Equipment equipment = new Equipment(random.Next(0, 50),types[(random.Next() % types.Count)]);
                    equipment.Id =Int32.Parse(_equipementService.AddEquipment(equipment));
                    _equipementService.AddEquipmentToRoom(mapObject, equipment);
                }
            }
        }
    }
}

