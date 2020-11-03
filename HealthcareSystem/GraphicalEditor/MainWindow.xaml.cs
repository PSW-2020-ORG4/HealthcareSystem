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
                    new MapObjectMetrics(70, 50, 400, 250),
                    new MapObjectDoor(MapObjectDoorOrientation.BOTTOM, 20, 0)
            );

            firstBuilding.AddToCanvas(_canvas);

            MapObject operationRoom1 = new MapObject(
                    new Room(
                        MapObjectTypes.OPERATION_ROOM, "Operation room 1", MapObjectDepartment.CARDIOLOGY, firstBuilding, 0
                    ),
                    new MapObjectMetrics(70, 50, 200, 70),
                    new MapObjectDoor(MapObjectDoorOrientation.BOTTOM, 30, 0)
                );

            operationRoom1.AddToCanvas(_canvas);

            MapObject operationRoom2 = new MapObject(
                   new Room(
                       MapObjectTypes.OPERATION_ROOM, "Operation room 2", MapObjectDepartment.CARDIOLOGY, firstBuilding, 0
                   ),
                   new MapObjectMetrics(267, 50, 203, 70),
                   new MapObjectDoor(MapObjectDoorOrientation.BOTTOM, 0, 30)
               );

            operationRoom2.AddToCanvas(_canvas);

            MapObject examinationRoom1 = new MapObject(
                    new Room(
                        MapObjectTypes.EXAMINATION_ROOM, "Examiantion room 3", MapObjectDepartment.CARDIOLOGY, firstBuilding, 0
                    ),
                    new MapObjectMetrics(70, 117, 80, 118),
                    new MapObjectDoor(MapObjectDoorOrientation.RIGHT, 0, 0)
                );

            examinationRoom1.AddToCanvas(_canvas);

            MapObject examinationRoom2 = new MapObject(
                   new Room(
                       MapObjectTypes.EXAMINATION_ROOM, "Room 4", MapObjectDepartment.CARDIOLOGY, firstBuilding, 0
                   ),
                   new MapObjectMetrics(70, 232, 100, 68),
                   new MapObjectDoor(MapObjectDoorOrientation.RIGHT, 0, 0)
               );

            examinationRoom2.AddToCanvas(_canvas);

            MapObject waitingRoomBuilding1 = new MapObject(
                  new Room(
                      MapObjectTypes.WAITING_ROOM, "Waiting room 1", MapObjectDepartment.CARDIOLOGY, firstBuilding, 0
                  ),
                  new MapObjectMetrics(215, 165, 140, 90),
                  new MapObjectDoor(MapObjectDoorOrientation.NONE)
              );

            waitingRoomBuilding1.AddToCanvas(_canvas);

            MapObject wcForMan = new MapObject(
                  new Room(
                      MapObjectTypes.WC, "WC 1", MapObjectDepartment.CARDIOLOGY, firstBuilding, 0
                  ),
                  new MapObjectMetrics(400, 117, 70, 45),
                  new MapObjectDoor(MapObjectDoorOrientation.LEFT, 0, 0)
              );

            wcForMan.AddToCanvas(_canvas);

            MapObject wcForWoman = new MapObject(
                  new Room(
                      MapObjectTypes.WC, "WC 2", MapObjectDepartment.CARDIOLOGY, firstBuilding, 0
                  ),
                  new MapObjectMetrics(400, 159, 70, 45),
                  new MapObjectDoor(MapObjectDoorOrientation.LEFT, 0, 0)
              );

            wcForWoman.AddToCanvas(_canvas);

            MapObject examiantionRoom3 = new MapObject(
                  new Room(
                      MapObjectTypes.EXAMINATION_ROOM, "Room 5", MapObjectDepartment.CARDIOLOGY, firstBuilding, 0
                  ),
                  new MapObjectMetrics(400, 201, 70, 99),
                  new MapObjectDoor(MapObjectDoorOrientation.LEFT, 0, 0)
              );

            MapObject restaurant = new MapObject(
                   new Restaurant(),
                   new MapObjectMetrics(500, 50, 370, 200),
                   new MapObjectDoor(MapObjectDoorOrientation.BOTTOM, 0, 0)
            );

            restaurant.AddToCanvas(_canvas);


            MapObject secondBuilding = new MapObject(
                    new Building("Building 2", 3),
                    new MapObjectMetrics(900, 50, 330, 620),
                    new MapObjectDoor(MapObjectDoorOrientation.LEFT, 0, 0)
            );

            secondBuilding.AddToCanvas(_canvas);


            MapObject wc1ForManBuilding2 = new MapObject(
                new Room(
                    MapObjectTypes.WC, "WC 3", MapObjectDepartment.CARDIOLOGY, secondBuilding, 0
                ),
                new MapObjectMetrics(1015, 50, 45, 70),
                new MapObjectDoor(MapObjectDoorOrientation.BOTTOM, 0, 0)
            );

            wc1ForManBuilding2.AddToCanvas(_canvas);

            MapObject wc1ForWomanBuilding2 = new MapObject(
                  new Room(
                      MapObjectTypes.WC, "WC 4", MapObjectDepartment.CARDIOLOGY, secondBuilding, 0
                  ),
                  new MapObjectMetrics(1060 - Constants.AllConstants.RECTANGLE_STROKE_THICKNESS, 50, 45, 70),
                  new MapObjectDoor(MapObjectDoorOrientation.BOTTOM, 0, 0)
              );

            wc1ForWomanBuilding2.AddToCanvas(_canvas);


            MapObject wc2ForManBuilding2 = new MapObject(
               new Room(
                   MapObjectTypes.WC, "WC 5", MapObjectDepartment.CARDIOLOGY, secondBuilding, 0
               ),
               new MapObjectMetrics(1015, 600, 45, 70),
               new MapObjectDoor(MapObjectDoorOrientation.TOP, 0, 0)
           );

            wc2ForManBuilding2.AddToCanvas(_canvas);

            MapObject wc2ForWomanBuilding2 = new MapObject(
                  new Room(
                      MapObjectTypes.WC, "WC 6", MapObjectDepartment.CARDIOLOGY, secondBuilding, 0
                  ),
                  new MapObjectMetrics(1060 - Constants.AllConstants.RECTANGLE_STROKE_THICKNESS, 600, 45, 70),
                  new MapObjectDoor(MapObjectDoorOrientation.TOP, 0, 0)
              );

            wc2ForWomanBuilding2.AddToCanvas(_canvas);


            MapObject waitingRoom1Building2 = new MapObject(
                  new Room(
                      MapObjectTypes.WAITING_ROOM, "Waiting room 2", MapObjectDepartment.CARDIOLOGY, secondBuilding, 0
                  ),
                  new MapObjectMetrics(985, 140, 160, 170),
                  new MapObjectDoor(MapObjectDoorOrientation.NONE)
              );

            waitingRoom1Building2.AddToCanvas(_canvas);


            MapObject waitingRoom2Building2 = new MapObject(
                 new Room(
                     MapObjectTypes.WAITING_ROOM, "Waiting room 3", MapObjectDepartment.CARDIOLOGY, secondBuilding, 0
                 ),
                 new MapObjectMetrics(985, 400, 160, 170),
                 new MapObjectDoor(MapObjectDoorOrientation.NONE)
             );

            waitingRoom2Building2.AddToCanvas(_canvas);


            for (int i = 0; i <= 6; i++)
            {
                double startXOfDrawing = 900;
                double startYOfDrawing = 50;
                double widthOfObject = 70;
                double heightOfObject = 90;



                double yOfObject = i == 0 ? startYOfDrawing : startYOfDrawing + heightOfObject * i - i * Constants.AllConstants.RECTANGLE_STROKE_THICKNESS;


                if (i == 3)
                {
                    yOfObject += 80 + i * Constants.AllConstants.RECTANGLE_STROKE_THICKNESS;
                }


                MapObject examiantionRoomInBuilding2 = new MapObject(
               new Room(
                   MapObjectTypes.EXAMINATION_ROOM, "Examination room " + i, MapObjectDepartment.CARDIOLOGY, secondBuilding, 0
               ),
               new MapObjectMetrics(startXOfDrawing, yOfObject, widthOfObject, heightOfObject),
               new MapObjectDoor(MapObjectDoorOrientation.RIGHT, 0, 0)
            );

                examiantionRoomInBuilding2.AddToCanvas(_canvas);

            }

            for (int i = 0; i <= 6; i++)
            {
                double startXOfDrawing = 970 + 190;
                double startYOfDrawing = 50;
                double widthOfObject = 70;
                double heightOfObject = 90;



                double yOfObject = i == 0 ? startYOfDrawing : startYOfDrawing + heightOfObject * i - i * Constants.AllConstants.RECTANGLE_STROKE_THICKNESS;


                if (i == 3)
                {
                    yOfObject += 80 + i * Constants.AllConstants.RECTANGLE_STROKE_THICKNESS;
                }


                MapObject operationRoomInBuilding2 = new MapObject(
               new Room(
                   MapObjectTypes.OPERATION_ROOM, "Operation Room " + i, MapObjectDepartment.CARDIOLOGY, secondBuilding, 0
               ),
               new MapObjectMetrics(startXOfDrawing, yOfObject, widthOfObject, heightOfObject),
               new MapObjectDoor(MapObjectDoorOrientation.LEFT, 0, 0)
            );

                operationRoomInBuilding2.AddToCanvas(_canvas);


            }


            for (int i = 0; i < 12; i++)
            {
                double startXOfDrawing = 70;
                double widthOfObject = 45;

                double xOfObject = i == 0 ? startXOfDrawing : startXOfDrawing + widthOfObject * i - i * Constants.AllConstants.RECTANGLE_STROKE_THICKNESS;

                MapObject parking = new MapObject(
                    new Parking(),
                    new MapObjectMetrics(xOfObject, 480, widthOfObject, 70),
                    new MapObjectDoor(MapObjectDoorOrientation.NONE)
                );

                parking.AddToCanvas(_canvas);
            }

            for (int i = 0; i < 12; i++)
            {
                double startXOfDrawing = 70;
                double widthOfParkingPlace = 45;

                double xOfObject = i == 0 ? startXOfDrawing : startXOfDrawing + widthOfParkingPlace * i - i * Constants.AllConstants.RECTANGLE_STROKE_THICKNESS;

                MapObject parking = new MapObject(
                    new Parking(),
                    new MapObjectMetrics(xOfObject, 600, widthOfParkingPlace, 70),
                    new MapObjectDoor(MapObjectDoorOrientation.NONE)
                );

                parking.AddToCanvas(_canvas);
            }

        }
    }

}
