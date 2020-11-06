using GraphicalEditor.Constants;
using GraphicalEditor.Enumerations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GraphicalEditor.Models.MapObjectRelated
{
   public class MocupObjects
    {
        private List<MapObject> AllMapObjects { get; set; }

        public MocupObjects()
        {
            AllMapObjects = new List<MapObject>();
        }

        public List<MapObject> getAllMapObjects()
        {
            MapObject firstBuilding = new MapObject(
                   new Building("Building 1", 3),
                   new MapObjectMetrics(40, 50, 400, 250),
                   new MapObjectDoor(MapObjectDoorOrientation.BOTTOM, 20, 0)
           );

            AllMapObjects.Add(firstBuilding);

            MapObject operationRoom1InBuilding1 = new MapObject(
                    new Room(
                        MapObjectTypes.OPERATION_ROOM, "Operation room 1", MapObjectDepartments.CARDIOLOGY, firstBuilding, 0
                    ),
                    new MapObjectMetrics(40, 50, 200, 70),
                    new MapObjectDoor(MapObjectDoorOrientation.BOTTOM, 30, 0)
                );

            AllMapObjects.Add(operationRoom1InBuilding1);

            MapObject operationRoom2InBuilding1 = new MapObject(
                   new Room(
                       MapObjectTypes.OPERATION_ROOM, "Operation room 2", MapObjectDepartments.CARDIOLOGY, firstBuilding, 0
                   ),
                   new MapObjectMetrics(240 - AllConstants.RECTANGLE_STROKE_THICKNESS, 50, 200 + AllConstants.RECTANGLE_STROKE_THICKNESS, 70),
                   new MapObjectDoor(MapObjectDoorOrientation.BOTTOM, 0, 30)
               );

            AllMapObjects.Add(operationRoom2InBuilding1);

            MapObject examinationRoom1InBuilding1 = new MapObject(
                    new Room(
                        MapObjectTypes.EXAMINATION_ROOM, "Examiantion room 3", MapObjectDepartments.CARDIOLOGY, firstBuilding, 0
                    ),
                    new MapObjectMetrics(40, 120 - AllConstants.RECTANGLE_STROKE_THICKNESS, 80, 120 + AllConstants.RECTANGLE_STROKE_THICKNESS),
                    new MapObjectDoor(MapObjectDoorOrientation.RIGHT, 0, 0)
                );

            AllMapObjects.Add(examinationRoom1InBuilding1);

            MapObject examinationRoom2InBuilding1 = new MapObject(
                   new Room(
                       MapObjectTypes.EXAMINATION_ROOM, "Room 4", MapObjectDepartments.CARDIOLOGY, firstBuilding, 0
                   ),
                   new MapObjectMetrics(40, 240 - AllConstants.RECTANGLE_STROKE_THICKNESS, 100, 60 + AllConstants.RECTANGLE_STROKE_THICKNESS),
                   new MapObjectDoor(MapObjectDoorOrientation.RIGHT, 0, 0)
               );

            AllMapObjects.Add(examinationRoom2InBuilding1);

            MapObject waitingRoomBuilding1 = new MapObject(
                  new Room(
                      MapObjectTypes.WAITING_ROOM, "Waiting room 1", MapObjectDepartments.CARDIOLOGY, firstBuilding, 0
                  ),
                  new MapObjectMetrics(185, 165, 140, 90),
                  new MapObjectDoor(MapObjectDoorOrientation.NONE)
              );

            AllMapObjects.Add(waitingRoomBuilding1);

            MapObject wcForManBuilding1 = new MapObject(
                  new Room(
                      MapObjectTypes.WC, "WC 1", MapObjectDepartments.CARDIOLOGY, firstBuilding, 0
                  ),
                  new MapObjectMetrics(370, 120 - AllConstants.RECTANGLE_STROKE_THICKNESS, 70, 55 + AllConstants.RECTANGLE_STROKE_THICKNESS),
                  new MapObjectDoor(MapObjectDoorOrientation.LEFT, 0, 0)
              );

            AllMapObjects.Add(wcForManBuilding1);

            MapObject wcForWomanBuilding1 = new MapObject(
                  new Room(
                      MapObjectTypes.WC, "WC 2", MapObjectDepartments.CARDIOLOGY, firstBuilding, 0
                  ),
                  new MapObjectMetrics(370, 175 - AllConstants.RECTANGLE_STROKE_THICKNESS, 70, 55 + AllConstants.RECTANGLE_STROKE_THICKNESS),
                  new MapObjectDoor(MapObjectDoorOrientation.LEFT, 0, 0)
              );

            AllMapObjects.Add(wcForWomanBuilding1);

            MapObject examiantionRoom3InBuilding1 = new MapObject(
                  new Room(
                      MapObjectTypes.EXAMINATION_ROOM, "Room 5", MapObjectDepartments.CARDIOLOGY, firstBuilding, 0
                  ),
                  new MapObjectMetrics(370, 230 - AllConstants.RECTANGLE_STROKE_THICKNESS, 70, 70 + AllConstants.RECTANGLE_STROKE_THICKNESS),
                  new MapObjectDoor(MapObjectDoorOrientation.LEFT, 0, 0)
              );

            AllMapObjects.Add(examiantionRoom3InBuilding1);

            MapObject restaurant = new MapObject(
                   new Restaurant(),
                   new MapObjectMetrics(485, 50, 200, 130),
                   new MapObjectDoor(MapObjectDoorOrientation.BOTTOM, 0, 0)
            );

            AllMapObjects.Add(restaurant);


            MapObject secondBuilding = new MapObject(
                    new Building("Building 2", 3),
                    new MapObjectMetrics(730, 50, 330, 612),
                    new MapObjectDoor(MapObjectDoorOrientation.LEFT, 0, 0)
            );

            AllMapObjects.Add(secondBuilding);


            MapObject wc1ForManBuilding2 = new MapObject(
                new Room(
                    MapObjectTypes.WC, "WC 3", MapObjectDepartments.CARDIOLOGY, secondBuilding, 0
                ),
                new MapObjectMetrics(835, 50, 55, 70),
                new MapObjectDoor(MapObjectDoorOrientation.BOTTOM, 0, 0)
            );

            AllMapObjects.Add(wc1ForManBuilding2);

            MapObject wc1ForWomanBuilding2 = new MapObject(
                  new Room(
                      MapObjectTypes.WC, "WC 4", MapObjectDepartments.CARDIOLOGY, secondBuilding, 0
                  ),
                  new MapObjectMetrics(890 - Constants.AllConstants.RECTANGLE_STROKE_THICKNESS, 50, 55, 70),
                  new MapObjectDoor(MapObjectDoorOrientation.BOTTOM, 0, 0)
              );
            AllMapObjects.Add(wc1ForWomanBuilding2);


            MapObject wc2ForManBuilding2 = new MapObject(
               new Room(
                   MapObjectTypes.WC, "WC 5", MapObjectDepartments.CARDIOLOGY, secondBuilding, 0
               ),
               new MapObjectMetrics(835, 592, 55, 70),
               new MapObjectDoor(MapObjectDoorOrientation.TOP, 0, 0)
           );

            AllMapObjects.Add(wc2ForManBuilding2);

            MapObject wc2ForWomanBuilding2 = new MapObject(
                  new Room(
                      MapObjectTypes.WC, "WC 6", MapObjectDepartments.CARDIOLOGY, secondBuilding, 0
                  ),
                  new MapObjectMetrics(890 - Constants.AllConstants.RECTANGLE_STROKE_THICKNESS, 592, 55, 70),
                  new MapObjectDoor(MapObjectDoorOrientation.TOP, 0, 0)
              );

            AllMapObjects.Add(wc2ForWomanBuilding2);


            MapObject waitingRoom1Building2 = new MapObject(
                  new Room(
                      MapObjectTypes.WAITING_ROOM, "Waiting room 2", MapObjectDepartments.CARDIOLOGY, secondBuilding, 0
                  ),
                  new MapObjectMetrics(815, 140, 160, 170),
                  new MapObjectDoor(MapObjectDoorOrientation.NONE)
              );

            AllMapObjects.Add(waitingRoom1Building2);


            MapObject waitingRoom2Building2 = new MapObject(
                 new Room(
                     MapObjectTypes.WAITING_ROOM, "Waiting room 3", MapObjectDepartments.CARDIOLOGY, secondBuilding, 0
                 ),
                 new MapObjectMetrics(815, 400, 160, 170),
                 new MapObjectDoor(MapObjectDoorOrientation.NONE)
             );

            AllMapObjects.Add(waitingRoom2Building2);


            for (int i = 0; i <= 6; i++)
            {
                double startXOfDrawing = 730;
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
                   MapObjectTypes.EXAMINATION_ROOM, "Examination room " + i, MapObjectDepartments.CARDIOLOGY, secondBuilding, 0
               ),
               new MapObjectMetrics(startXOfDrawing, yOfObject, widthOfObject, heightOfObject),
               new MapObjectDoor(MapObjectDoorOrientation.RIGHT, 0, 0)
            );

                AllMapObjects.Add(examiantionRoomInBuilding2);

            }

            for (int i = 0; i <= 6; i++)
            {
                double startXOfDrawing = 800 + 190;
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
                   MapObjectTypes.OPERATION_ROOM, "Operation Room " + i, MapObjectDepartments.CARDIOLOGY, secondBuilding, 0
               ),
               new MapObjectMetrics(startXOfDrawing, yOfObject, widthOfObject, heightOfObject),
               new MapObjectDoor(MapObjectDoorOrientation.LEFT, 0, 0)
            );

                AllMapObjects.Add(operationRoomInBuilding2);


            }


            for (int i = 0; i < 11; i++)
            {
                double startXOfDrawing = 50;
                double widthOfObject = 40;

                double xOfObject = i == 0 ? startXOfDrawing : startXOfDrawing + widthOfObject * i - i * Constants.AllConstants.RECTANGLE_STROKE_THICKNESS;

                MapObject parking = new MapObject(
                    new Parking(),
                    new MapObjectMetrics(xOfObject, 440, widthOfObject, 60),
                    new MapObjectDoor(MapObjectDoorOrientation.NONE)
                );

                AllMapObjects.Add(parking); ;
            }

            for (int i = 0; i < 11; i++)
            {
                double startXOfDrawing = 50;
                double widthOfParkingPlace = 40;

                double xOfObject = i == 0 ? startXOfDrawing : startXOfDrawing + widthOfParkingPlace * i - i * Constants.AllConstants.RECTANGLE_STROKE_THICKNESS;

                MapObject parking = new MapObject(
                    new Parking(),
                    new MapObjectMetrics(xOfObject, 565, widthOfParkingPlace, 60),
                    new MapObjectDoor(MapObjectDoorOrientation.NONE)
                );

                AllMapObjects.Add(parking);
            }

            return AllMapObjects;
        }
    }
}
