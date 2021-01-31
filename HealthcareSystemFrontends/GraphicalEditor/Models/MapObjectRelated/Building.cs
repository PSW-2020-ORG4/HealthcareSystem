using GraphicalEditor.DTO.EventSourcingDTO;
using GraphicalEditor.Enumerations;
using GraphicalEditor.Service;
using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;

namespace GraphicalEditor.Models.MapObjectRelated
{
    public class Building : MapObjectEntity
    {
        public int NumOfFloors { get; set; }

        public BuildingLayersButtons BuildingLayersButtons { get; set; }
        public Building(int numOfFloors, BuildingLayersButtons buildingLayersComboBox, String description = "")
            : base(TypeOfMapObject.BUILDING, description)
        {
            NumOfFloors = numOfFloors;
            BuildingLayersButtons = buildingLayersComboBox;
            FormatObjectDescription(Description);

        }

        public override void FormatObjectDescription(string description)
        {
            if (String.IsNullOrEmpty(description))
            {
                Description = MapObjectType.ObjectTypeFullName + " " + Id + " ima " + NumOfFloors + " spratova.";
            }
        }

        public void AddBuildinLayersButtonsToCanvas(Canvas canvas)
        {
            List<Button> floorButtons = BuildingLayersButtons.CreateBuildingLayersButtons(NumOfFloors);
            foreach (Button button in floorButtons)
            {
                button.Click += ShowChoosenFloor;
                canvas.Children.Add(button);
            }

        }

        public void LoadChoosenFloor(MapObject mapObject, int choosenFloor, Canvas canvas)
        {
            if (mapObject.MapObjectEntity.GetType() == typeof(Room) && ((Room)mapObject.MapObjectEntity).BuildingId == Id && ((Room)mapObject.MapObjectEntity).Floor == choosenFloor)
            {
                mapObject.AddToCanvas(canvas);
            }
        }

        public void ClearAllFloors(MapObject mapObject, int choosenFloor, Canvas canvas)
        {
            if (mapObject.MapObjectEntity.GetType() == typeof(Room) && ((Room)mapObject.MapObjectEntity).BuildingId == Id)
            {
                mapObject.RemoveFromCanvas(canvas);
            }
        }

        public void ShowChoosenFloor(object sender, RoutedEventArgs e)
        {
            int choosenFloorNumber = Int32.Parse((e.Source as Button).Content.ToString());
            ChangeFloorButtonsEnablement(choosenFloorNumber);
            ShowFloorByFloorNumber(choosenFloorNumber);

            AddFloorChangeEvent(choosenFloorNumber);
        }

        public void AddFloorChangeEvent(int choosenFloorNumber)
        {
            AddBuildingSelectionEvent();

            FloorChangeEventDTO floorChangeEventDTO = new FloorChangeEventDTO(MainWindow._currentUsername, (int)Id, choosenFloorNumber);

            EventSourcingService eventSourcingService = new EventSourcingService();
            eventSourcingService.AddFloorChangeEvent(floorChangeEventDTO);
        }

        public void AddBuildingSelectionEvent()
        {
            BuildingSelectionEventDTO buildingSelectionEventDTO = new BuildingSelectionEventDTO(MainWindow._currentUsername, (int)Id);

            EventSourcingService eventSourcingService = new EventSourcingService();
            eventSourcingService.AddBuildingSelectionEvent(buildingSelectionEventDTO);
        }

        private void ShowFloorByFloorNumber(int choosenFloorNumber)
        {
            foreach (MapObject mapObject in MainWindow._allMapObjects)
            {
                ClearAllFloors(mapObject, choosenFloorNumber, MainWindow._canvas);
                LoadChoosenFloor(mapObject, choosenFloorNumber, MainWindow._canvas);
            }
        }

        private void ChangeFloorButtonsEnablement(int choosenFloorNumber)
        {
            List<Button> floorButtons = BuildingLayersButtons.LayersSelectButtons;
            foreach (Button button in floorButtons)
            {
                if (Int32.Parse(button.Content.ToString()) != (choosenFloorNumber))
                {
                    button.IsEnabled = true;
                }
                else
                {
                    button.IsEnabled = false;
                }
            }
        }

        public void ShowFloorForSpecificMapObject(MapObject mapObject)
        {
            ChangeFloorButtonsEnablement(((Room)mapObject.MapObjectEntity).Floor);
            ShowFloorByFloorNumber(((Room)mapObject.MapObjectEntity).Floor);
        }
    }
}
