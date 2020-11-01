using GraphicalEditor.Constants;
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
        public List<MapObject> MapObjects { get; set; }

        public long MapObjectsMaxId { get; set; }
        public MainWindow()
        {
            InitializeComponent();
            _canvas = this.Canvas;
            MapObjects = new List<MapObject>();
            MapObjectsMaxId = MapObjects.Count() == 0 ? 0 : MapObjects.Max(entity => entity.Id);
            loadMockupObjects();
        }

        public void loadMockupObjects()
        {
            MapObject firstBuilding = new MapObject(
                    new Building("Building 1", 3),
                    new MapObjectMetrics(10, 20, 100, 200),
                    new MapObjectDoor(MapObjectDoorOrientation.BOTTOM, 20, 0),
                    ++MapObjectsMaxId
                );
            MapObjects.Add(firstBuilding);
            Console.WriteLine(firstBuilding.Id);
            firstBuilding.AddToCanvas(_canvas);


            for (int i = 0; i <= 10; i++)
            {
                double startX = 20;
                double width = 70;

                double x = i == 0 ? startX : startX + width * i - i * AllConstants.RECTANGLE_STROKE_THICKNESS;

                MapObject parkingSpace = new MapObject(
                    new Room(
                        MapObjectTypes.PARKING, "Opis 1", MapObjectDepartment.CARDIOLOGY, 0, firstBuilding
                    ),
                    new MapObjectMetrics(x, 300, width, 120),
                    new MapObjectDoor(MapObjectDoorOrientation.BOTTOM, 0, 30),
                    ++MapObjectsMaxId
                );
                MapObjects.Add(parkingSpace);
                Console.WriteLine(parkingSpace.Id);
                parkingSpace.AddToCanvas(_canvas);
            }
        }

    }
}
