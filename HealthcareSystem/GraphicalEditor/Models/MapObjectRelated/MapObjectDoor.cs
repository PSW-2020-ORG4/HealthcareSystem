﻿using GraphicalEditor.Constants;
using GraphicalEditor.Enumerations;
using GraphicalEditor.Enumerations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;

namespace GraphicalEditor.Models.MapObjectRelated
{
    public class MapObjectDoor
    {
        public Rectangle Rectangle { get; set; }
        public MapObjectMetrics MapObjectMetrics { get; set; }
        public MapObjectDoorOrientation MapObjectDoorOrientation { get; set; }
        public double XShiftFromCenter { get; set; }
        public double YShiftFromCenter { get; set; }

        public MapObjectDoor(MapObjectDoorOrientation mapObjectDoorOrientations, double XShift = 0, double YShift = 0)
        {
            MapObjectDoorOrientation = mapObjectDoorOrientations;
            XShiftFromCenter = XShift;
            YShiftFromCenter = YShift;
        }

        public Rectangle GetDoor()
        {
            Rectangle = new Rectangle();
            Rectangle.Fill = Brushes.DarkGreen;
            Rectangle.Width = DoorWidth;
            Rectangle.Height = DoorHeight;

            Rectangle.SetValue(Canvas.LeftProperty, CalculateDoorX());
            Rectangle.SetValue(Canvas.TopProperty, CalculateDoorY());

            return Rectangle;
        }

        private double CalculateDoorX()
        {
            switch (MapObjectDoorOrientation)
            {
                case MapObjectDoorOrientation.LEFT:
                    return CalculateXForLeft();
                case MapObjectDoorOrientation.RIGHT:
                    return CalculateXForRight();
                default:
                    return CalculateXShifted();
            }
        }

        private double CalculateXForLeft()
            => MapObjectMetrics.X - DoorWidth + AllConstants.RECTANGLE_STROKE_THICKNESS;

        private double CalculateXForRight()
            => MapObjectMetrics.X + MapObjectMetrics.Width - AllConstants.RECTANGLE_STROKE_THICKNESS;

        private double CalculateDoorY()
        {
            switch (MapObjectDoorOrientation)
            {
                case MapObjectDoorOrientation.TOP:
                    return CalculateYForTop();
                case MapObjectDoorOrientation.BOTTOM:
                    return CalculateYForBottom();
                default:
                    return CalculateYShifted();
            }
        }        

        private double CalculateYForTop()
            => MapObjectMetrics.Y - DoorHeight + AllConstants.RECTANGLE_STROKE_THICKNESS;

        private double CalculateYForBottom()
            => MapObjectMetrics.Y + MapObjectMetrics.Height - AllConstants.RECTANGLE_STROKE_THICKNESS;


        
        private double CalculateXShifted()
        {
            double currentShiftedX = MapObjectMetrics.X + MapObjectMetrics.Width / 2 - DoorWidth / 2 + this.XShiftFromCenter;
            if (currentShiftedX < MapObjectMetrics.X)
                return MapObjectMetrics.X;
            else if (currentShiftedX + DoorWidth > MapObjectMetrics.X + MapObjectMetrics.Width)
                return MapObjectMetrics.X + MapObjectMetrics.Width - DoorWidth;
            else return currentShiftedX;
        }

        
        private double CalculateYShifted()
        {
            double currentShiftedY = MapObjectMetrics.Y + MapObjectMetrics.Height / 2 - DoorHeight / 2 + this.YShiftFromCenter;
            if (currentShiftedY < MapObjectMetrics.Y)
                return MapObjectMetrics.Y;
            else if (currentShiftedY + DoorHeight > MapObjectMetrics.Y + MapObjectMetrics.Height)
                return MapObjectMetrics.Y + MapObjectMetrics.Height - DoorHeight;
            else return currentShiftedY;
        }

        
        private double DoorWidth
        {
            get
            {
                if (MapObjectDoorOrientation == MapObjectDoorOrientation.TOP || MapObjectDoorOrientation == MapObjectDoorOrientation.BOTTOM)
                    return AllConstants.DOOR_WIDTH;
                else return AllConstants.DOOR_HEIGHT;
            }
        }

        
        private double DoorHeight
        {
            get
            {
                if (MapObjectDoorOrientation == MapObjectDoorOrientation.TOP || MapObjectDoorOrientation == MapObjectDoorOrientation.BOTTOM)
                    return AllConstants.DOOR_HEIGHT;
                else return AllConstants.DOOR_WIDTH;
            }
        }
    }
}
