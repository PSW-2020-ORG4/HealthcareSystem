using GraphicalEditor.Constants;
using GraphicalEditor.Enumerations;
using GraphicalEditor.Models.MapObjectRelated;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;

namespace GraphicalEditor.Models
{
    public class MapObject
    {
        public TextBlock MapObjectNameTextBlock { get; set; }
        public Rectangle Rectangle { get; set; }
        public MapObjectDoor MapObjectDoor { get; set; }
        public MapObjectEntity MapObjectEntity { get; set; }
        public MapObjectMetrics MapObjectMetrics { get; set; }

        public MapObject(MapObjectEntity mapObjectEntity, MapObjectMetrics mapObjectMetrics, MapObjectDoor mapObjectDoor)
        {
            MapObjectEntity = mapObjectEntity;
            MapObjectMetrics = mapObjectMetrics;

            MapObjectDoor = mapObjectDoor;
            MapObjectDoor.MapObjectMetrics = mapObjectMetrics;

            RectangleInitialization();
        //    PositionObjectNameTextBlock();
        }

        private void RectangleInitialization()
        {
            Rectangle = new Rectangle();
            Rectangle.Fill = MapObjectEntity.getColor();
            Rectangle.Height = MapObjectMetrics.HeightOfMapObject;
            Rectangle.Width = MapObjectMetrics.WidthOfMapObject;

            MapObjectEntity.setStrokeAndStrokeThickness(Rectangle);
        }
        /* this function needs to be changed !!!
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
        } */


        public void AddToCanvas(Canvas canvas)
        {
            canvas.Children.Add(Rectangle);
            Canvas.SetLeft(Rectangle, MapObjectMetrics.XOfCanvas);
            Canvas.SetTop(Rectangle, MapObjectMetrics.YOfCanvas);

            canvas.Children.Add(MapObjectDoor.GetDoor());
        //    canvas.Children.Add(MapObjectNameTextBlock);
        }
    }
}
