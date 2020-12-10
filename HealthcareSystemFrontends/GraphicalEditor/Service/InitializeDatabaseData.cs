using Backend.Model.Manager;
using GraphicalEditor.Models;
using GraphicalEditor.Models.Drugs;
using GraphicalEditor.Models.Equipment;
using System;
using System.Collections.Generic;

namespace GraphicalEditor.Service
{
    public class InitializeDatabaseData
    {
        private RoomService _roomService;
        private EquipementService _equipementService;
        private EquipmentTypeService _equipmentTypeService;
        private DrugService _drugService;
        private DrugTypeService _drugTypeService;

        public InitializeDatabaseData()
        {
            _roomService = new RoomService();
            _equipementService = new EquipementService();
            _equipmentTypeService = new EquipmentTypeService();
            _drugService = new DrugService();
            _drugTypeService = new DrugTypeService();
        }

        public void InitiliazeData()
        {
            AddRooms();
            AddEquipmentTypes();
            AddEquipment();
<<<<<<< HEAD
            //AddDrugTypes();
            //AddDrugs();
=======
            AddDrugTypes();
            AddDrugs();
>>>>>>> develop
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

        public void AddDrugTypes()
        {
            _drugTypeService.AddDrugType(new DrugType("tableta", "lek za glavu"));
            _drugTypeService.AddDrugType(new DrugType("tableta", "lek za temperaturu"));

        }

        public void AddDrugs()
        {
            List<MapObject> allMapObjects = MainWindow._allMapObjects;

            int i = 0;
            var types = _drugTypeService.GetDrugTypes();
            if (types.Count == 0)
            {
                return;
            }
            foreach (MapObject mapObject in allMapObjects)
            {
                if (mapObject.CheckIfDBAddableRoom())
                {
                    i++;
                    Drug drug1 = new Drug(types[0], "Brufen", 20, DateTime.Now.AddDays(365), "Hemofram");
                    Drug drug2 = new Drug(types[1], "Metafeks", 10, DateTime.Now.AddDays(365), "Hemofram");
                    drug1.Id = Int32.Parse(_drugService.AddDrug(drug1));
                    drug2.Id = Int32.Parse(_drugService.AddDrug(drug2));
                    _drugService.AddDrugToRoom(mapObject, drug1);
                    _drugService.AddDrugToRoom(mapObject, drug2);
                }
            }
        }

    }
}

