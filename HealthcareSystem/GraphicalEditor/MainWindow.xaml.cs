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
        public long MapObjectsMaxId { get; set; }
        public MainWindow()
        {
            InitializeComponent();
            _canvas = this.Canvas;

            LoadMockupObjects();
        }

        private void LoadMockupObjects()
        {
            MapObject firstBuilding = new MapObject(
                    new Building("Building 1", 3),
                    new MapObjectMetrics(10, 20, 100, 200),
                    new MapObjectDoor(MapObjectDoorOrientation.BOTTOM, 20, 0)
            );

            firstBuilding.AddToCanvas(_canvas);

            MapObject parking
                = new MapObject(new Parking(),
                   new MapObjectMetrics(130, 20, 100, 200),
                   new MapObjectDoor(MapObjectDoorOrientation.NONE)
            );

            parking.AddToCanvas(_canvas);

            for (int i = 0; i <= 10; i++)
            {
                double startXOfCanvas = 20;
                double widthOfExamiantionRoom = 70;

                double xOfCanvas = i == 0 ? startXOfCanvas : startXOfCanvas + widthOfExamiantionRoom * i - i * AllConstants.RECTANGLE_STROKE_THICKNESS;

                MapObject examinationRoom = new MapObject(
                    new Room(
                        MapObjectTypes.EXAMINATION_ROOM, "Opis 1", MapObjectDepartment.CARDIOLOGY, firstBuilding, 0
                    ),
                    new MapObjectMetrics(xOfCanvas, 300, widthOfExamiantionRoom, 120),
                    new MapObjectDoor(MapObjectDoorOrientation.BOTTOM, 0, 30)
                );

                examinationRoom.AddToCanvas(_canvas);
            }


        }

    }
}
