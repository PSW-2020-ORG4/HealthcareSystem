using GraphicalEditor.Constants;
using GraphicalEditor.Enumerations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Media;

namespace GraphicalEditor.Models.MapObjectRelated
{
   public class BuildingLayersButtons
    {
        public List<Button> LayersSelectButtons { get; set; }
        public MapObjectMetrics MapObjectMetrics { get; set; }
        public BuildingLayersButtonsOrientation BuildingLayersButtonsOrientation { get; set; }
        public double XShiftFromCenter { get; set; }
        public double YShiftFromCenter { get; set; }

        public BuildingLayersButtons(BuildingLayersButtonsOrientation buildingLayersButtonsOrientation, double XShift = 0, double YShift = 0)
        {

            BuildingLayersButtonsOrientation = buildingLayersButtonsOrientation;
            XShiftFromCenter = XShift;
            YShiftFromCenter = YShift;
        }

        public List<Button> CreateBuildingLayersButtons(int numberOfFloors)
        {
            LayersSelectButtons = new List<Button>();
            for (int i = 0; i < numberOfFloors; i++)
            {
                LayersSelectButtons.Add(new Button());
                LayersSelectButtons[i].Content = i;
                LayersSelectButtons[i].Width = AllConstants.LAYERS_BUTTON_WIDTH;
                LayersSelectButtons[i].Height = AllConstants.LAYERS_BUTTON_HEIGHT;
                LayersSelectButtons[i].Background = new SolidColorBrush(Colors.Plum);
                LayersSelectButtons[i].SetValue(Canvas.LeftProperty, CalculateLayersButtonsX(i, numberOfFloors));
                LayersSelectButtons[i].SetValue(Canvas.TopProperty, CalculateLayersButtonsY(i, numberOfFloors));
            }
            LayersSelectButtons[0].IsEnabled = false;
            return LayersSelectButtons;
        }

        private double CalculateLayersButtonsX(int floorNumber, int numberOfFloors)
        {
            switch (BuildingLayersButtonsOrientation)
            {
                case BuildingLayersButtonsOrientation.LEFT:
                    return CalculateXForLeft();
                case BuildingLayersButtonsOrientation.RIGHT:
                    return CalculateXForRight();
                default:
                    return CalculateXShifted(floorNumber, numberOfFloors);
            }
        }

        private double CalculateXForLeft()
            => MapObjectMetrics.XOfCanvas - 35;

        private double CalculateXForRight()
            => MapObjectMetrics.XOfCanvas + MapObjectMetrics.WidthOfMapObject + 35;

        private double CalculateLayersButtonsY(int floorNumber, int numberOfFloors)
        {
            switch (BuildingLayersButtonsOrientation)
            {
                case BuildingLayersButtonsOrientation.TOP:
                    return CalculateYForTop();
                case BuildingLayersButtonsOrientation.BOTTOM:
                    return CalculateYForBottom();
                default:
                    return CalculateYShifted(floorNumber, numberOfFloors);
            }
        }

        private double CalculateYForTop()
            => MapObjectMetrics.YOfCanvas - 5;

        private double CalculateYForBottom()
            => MapObjectMetrics.YOfCanvas + MapObjectMetrics.HeightOfMapObject + 5;

        private double CalculateXShifted(int floorNumber, int numberOfFloors)
        {
            double currentShiftedX = MapObjectMetrics.XOfCanvas + MapObjectMetrics.WidthOfMapObject / 2 - numberOfFloors * AllConstants.LAYERS_BUTTON_WIDTH / 2 + floorNumber * AllConstants.LAYERS_BUTTON_WIDTH + this.XShiftFromCenter;
            if (currentShiftedX < MapObjectMetrics.XOfCanvas)
                return MapObjectMetrics.XOfCanvas;
            else if (currentShiftedX + AllConstants.LAYERS_BUTTON_WIDTH > MapObjectMetrics.XOfCanvas + MapObjectMetrics.WidthOfMapObject)
                return MapObjectMetrics.XOfCanvas + MapObjectMetrics.WidthOfMapObject - AllConstants.LAYERS_BUTTON_WIDTH;
            else return currentShiftedX;
        }


        private double CalculateYShifted(int floorNumber, int numberOfFloors)
        {
            double currentShiftedY = MapObjectMetrics.YOfCanvas + MapObjectMetrics.HeightOfMapObject / 2 - numberOfFloors * AllConstants.LAYERS_BUTTON_HEIGHT / 2 + floorNumber * AllConstants.LAYERS_BUTTON_HEIGHT + this.YShiftFromCenter;
            if (currentShiftedY < MapObjectMetrics.YOfCanvas)
                return MapObjectMetrics.YOfCanvas;
            else if (currentShiftedY + AllConstants.LAYERS_BUTTON_HEIGHT > MapObjectMetrics.YOfCanvas + MapObjectMetrics.HeightOfMapObject)
                return MapObjectMetrics.YOfCanvas + MapObjectMetrics.HeightOfMapObject - AllConstants.LAYERS_BUTTON_HEIGHT;
            else return currentShiftedY;
        }

    }
}
