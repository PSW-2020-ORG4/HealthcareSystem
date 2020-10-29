using GraphicalEditor.Enumerations;
using GraphicalEditor.Models;
using GraphicalEditor.Models.MapObjectRelated;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace GraphicalEditor
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        private Canvas _canvas;

        public MainWindow()
        {
            InitializeComponent();
            _canvas = this.Canvas;

            // Dodavanje jednog objekta na mapu
            MapObject mp = new MapObject(
                    new MapObjectBasics(
                        new MapObjectType(MapObjectTypes.PARKING), "Opis 1", MapObjectDepartment.CARDIOLOGY, 0
                    ),
                    new MapObjectMetrics(10, 20, 100, 200),
                    new MapObjectDoor(MapObjectDoorOrientation.BOTTOM)
                );

            mp.AddToCanvas(_canvas);

            // Dodavanje vise objekata od jednom
            for (int i = 0; i <= 10; i+= 2)
            {
                double startX = 20;
                double width = 70;
                double x = i == 0 ? startX : startX + width * i - i * MapObject.RECTANGLE_STROKE_THICKNESS;

                MapObject mp2 = new MapObject(
                    new MapObjectBasics(
                        new MapObjectType(MapObjectTypes.OPERATION_ROOM), "Opis 1", MapObjectDepartment.CARDIOLOGY, 0
                    ),
                    new MapObjectMetrics(x, 300, width, 120),
                    new MapObjectDoor(MapObjectDoorOrientation.RIGHT, 0, 30)
                );

                mp2.AddToCanvas(_canvas);
            }
        }
    }
}
