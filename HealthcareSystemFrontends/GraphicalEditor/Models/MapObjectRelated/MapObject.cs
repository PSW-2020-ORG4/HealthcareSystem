﻿using GraphicalEditor.DTO;
using GraphicalEditor.Enumerations;
using GraphicalEditor.Models.MapObjectRelated;
using GraphicalEditor.Service;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;

namespace GraphicalEditor.Models
{
    public class MapObject
    {
        [JsonIgnore]
        public TextBlock MapObjectNameTextBlock { get; set; }
        public Rectangle Rectangle { get; set; }
        public MapObjectDoor MapObjectDoor { get; set; }
        public MapObjectEntity MapObjectEntity { get; set; }
        public MapObjectMetrics MapObjectMetrics { get; set; }

        public double MapObjectArea 
        {
            get
            {
                return Rectangle.Width * Rectangle.Height;
            }
        }

        public Line Line { get; private set; }

        public MapObject(MapObjectEntity mapObjectEntity, MapObjectMetrics mapObjectMetrics, MapObjectDoor mapObjectDoor)
        {
            MapObjectEntity = mapObjectEntity;

            if (MapObjectEntity.GetType() == typeof(Building))
            {
                ((Building)MapObjectEntity).BuildingLayersButtons.MapObjectMetrics = mapObjectMetrics;
            }

            MapObjectMetrics = mapObjectMetrics;

            MapObjectDoor = mapObjectDoor;
            MapObjectDoor.MapObjectMetrics = mapObjectMetrics;

            RectangleInitialization();
            if (mapObjectEntity.MapObjectType.TypeOfMapObject == TypeOfMapObject.ROAD) {
                LineInitialization();
            }
            PositionObjectNameTextBlock();
        }
        private void LineInitialization()
        {
            Line = new Line();
            Line.Fill = Brushes.White;
            Line.X1 = Rectangle.RadiusX;
            Line.X2 = Rectangle.Width+ Rectangle.RadiusX;
            Line.Y1 = Rectangle.Height / 2-2;
            Line.Y2 = Rectangle.Height / 2 + 2;
        }


        private void RectangleInitialization()
        {
            Rectangle = new Rectangle();
            Rectangle.Fill = MapObjectEntity.ObjectEntityColor;
            Rectangle.Height = MapObjectMetrics.HeightOfMapObject;
            Rectangle.Width = MapObjectMetrics.WidthOfMapObject;
            MapObjectEntity.MapObjectType.SetStrokeColorAndThickness(Rectangle);
        }

        private void PositionObjectNameTextBlock()
        {
            MapObjectNameTextBlock = new TextBlock();
            MapObjectNameTextBlock.Text = MapObjectEntity.MapObjectType.ObjectTypeNameAbbreviation;
            MapObjectNameTextBlock.FontSize = 20;
            MapObjectNameTextBlock.HorizontalAlignment = HorizontalAlignment.Center;
            MapObjectNameTextBlock.SetValue(Canvas.LeftProperty, MapObjectMetrics.XOfCanvas);
            MapObjectNameTextBlock.SetValue(Canvas.WidthProperty, Rectangle.Width);
            MapObjectNameTextBlock.SetValue(Canvas.TopProperty, (MapObjectMetrics.YOfCanvas + (Rectangle.Height - MapObjectNameTextBlock.FontSize) / 2));
            MapObjectNameTextBlock.TextWrapping = TextWrapping.Wrap;
            MapObjectNameTextBlock.TextAlignment = TextAlignment.Center;
        }

        public void AddToCanvas(Canvas canvas)
        {
            canvas.Children.Add(Rectangle);
            Canvas.SetLeft(Rectangle, MapObjectMetrics.XOfCanvas);
            Canvas.SetTop(Rectangle, MapObjectMetrics.YOfCanvas);
            canvas.Children.Add(MapObjectDoor.GetDoor());
            canvas.Children.Add(MapObjectNameTextBlock);

            if (MapObjectEntity.GetType() == typeof(Building))
            {
                ((Building)MapObjectEntity).AddBuildinLayersButtonsToCanvas(canvas);
            }
        }

        public void RemoveFromCanvas(Canvas canvas)
        {
            canvas.Children.Remove(MapObjectNameTextBlock);
            canvas.Children.Remove(MapObjectDoor.Rectangle);
            canvas.Children.Remove(Rectangle);
        }

       
        public List<EquipmentDTO> GetEquipmentInObject()
        {
            EquipmentService equipmentService = new EquipmentService();
            List<EquipmentDTO> equipmentInObject = equipmentService.GetEquipmentByRoomNumber((int)MapObjectEntity.Id);
            return equipmentInObject;
        }

        public List<DrugDTO> GetMedicineInObject()
        {
            DrugService drugService = new DrugService();
            List<DrugDTO> medicinetInObject = drugService.GetDrugsByRoomNumber((int)MapObjectEntity.Id);
            return medicinetInObject;
        }

        public Boolean CheckIsRoom()
            => MapObjectEntity.MapObjectType.TypeOfMapObject == TypeOfMapObject.EXAMINATION_ROOM
                || MapObjectEntity.MapObjectType.TypeOfMapObject == TypeOfMapObject.HOSPITALIZATION_ROOM
                || MapObjectEntity.MapObjectType.TypeOfMapObject == TypeOfMapObject.OPERATION_ROOM;

    }
}
