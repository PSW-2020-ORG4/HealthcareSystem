using GraphicalEditor.Enumerations;
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
            List<Button> floorButtons = BuildingLayersButtons.GetBuildingLayersButtons(NumOfFloors);
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

        public void ClearOtherFloors(MapObject mapObject, int choosenFloor, Canvas canvas)
        {
            if (mapObject.MapObjectEntity.GetType() == typeof(Room) && ((Room)mapObject.MapObjectEntity).BuildingId == Id && ((Room)mapObject.MapObjectEntity).Floor != choosenFloor)
            {
                mapObject.RemoveFromCanvas(canvas);
            }
        }

        public void ShowChoosenFloor(object sender, RoutedEventArgs e)
        {
            int choosenFloor = Int32.Parse((e.Source as Button).Content.ToString());
            ChangeFloorButtonsEnablement(e.Source as Button);
            foreach (MapObject mapObject in MainWindow._allMapObjects)
            {
                ClearOtherFloors(mapObject, choosenFloor, MainWindow._canvas);
                LoadChoosenFloor(mapObject, choosenFloor, MainWindow._canvas);
            }
        }

        private void ChangeFloorButtonsEnablement(Button choosenButton)
        {
            List<Button> floorButtons = BuildingLayersButtons.LayersSelectButtons;
            foreach (Button button in floorButtons)
            {
                if (!button.Content.ToString().Equals(choosenButton.Content.ToString()))
                {
                    button.IsEnabled = true;
                }
                else
                {
                    button.IsEnabled = false;
                }
            }
        }
    }
}
