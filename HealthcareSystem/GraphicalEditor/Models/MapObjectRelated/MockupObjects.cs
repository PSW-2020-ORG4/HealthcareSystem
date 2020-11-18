using GraphicalEditor.Constants;
using GraphicalEditor.Enumerations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GraphicalEditor.Models.MapObjectRelated
{
   public class MockupObjects
    {
        public List<MapObject> AllMapObjects { get; set; }
        public MapObject FirstBuilding { get; set; }
        public MapObject SecondBuilding { get;set; }
        public MockupObjects()
        {
            AllMapObjects = new List<MapObject>();

            AddRoadToMap();

            AddFirstBuildingToMap();
            AddSecondBuildingToMap();

            AddRestaurantToMap();

            AddObjectsOnFirstFloorBuilding1();
            AddObjectsOnGroundLevelBuilding1();

            AddObjectsOnFirstFloorBuilding2();
            AddObjectsOnGroundLevelBuilding2();

            AddParkingPlacesToMap();

        }
           
        private void AddFirstBuildingToMap()
        {
            FirstBuilding = new MapObject(
                  new Building(2, new BuildingLayersButtons(BuildingLayersButtonsOrientation.LEFT, 0, 0), "Building 1"),
                  new MapObjectMetrics(40, 50, 400, 250),
                  new MapObjectDoor(MapObjectDoorOrientation.BOTTOM, 20, 0)
          );

            AllMapObjects.Add(FirstBuilding);
        }

        private void AddSecondBuildingToMap()
        {
             SecondBuilding = new MapObject(
                           new Building(2, new BuildingLayersButtons(BuildingLayersButtonsOrientation.BOTTOM, 0, 0), "Building 2"),
                           new MapObjectMetrics(730, 50, 330, 612),
                           new MapObjectDoor(MapObjectDoorOrientation.LEFT, 0, 0)
                   );

            AllMapObjects.Add(SecondBuilding);

        }

        private void AddRestaurantToMap()
        {
            MapObject restaurant = new MapObject(
                  new Restaurant(),
                  new MapObjectMetrics(485, 50, 200, 130),
                  new MapObjectDoor(MapObjectDoorOrientation.BOTTOM, 0, 0)
           );

            AllMapObjects.Add(restaurant);
        }

        private void AddObjectsOnGroundLevelBuilding1()
        {
            MapObject operationRoom1InBuilding1GroundLevel = new MapObject(
                   new Room(
                       TypeOfMapObject.OPERATION_ROOM, DepartmentOfMapObject.CARDIOLOGY, FirstBuilding, 0, "Operation room 1"
                   ),
                   new MapObjectMetrics(40, 50, 200, 70),
                   new MapObjectDoor(MapObjectDoorOrientation.BOTTOM, 30, 0)
               );

            AllMapObjects.Add(operationRoom1InBuilding1GroundLevel);

            MapObject operationRoom2InBuilding1GroundLevel = new MapObject(
                   new Room(
                      TypeOfMapObject.OPERATION_ROOM, DepartmentOfMapObject.CARDIOLOGY, FirstBuilding, 0, "Operation room 2"
                   ),
                   new MapObjectMetrics(240 - AllConstants.RECTANGLE_STROKE_THICKNESS, 50, 200 + AllConstants.RECTANGLE_STROKE_THICKNESS, 70),
                   new MapObjectDoor(MapObjectDoorOrientation.BOTTOM, 0, 30)
               );

            AllMapObjects.Add(operationRoom2InBuilding1GroundLevel);

            MapObject examinationRoom1InBuilding1GroundLevel = new MapObject(
                    new Room(
                         TypeOfMapObject.EXAMINATION_ROOM, DepartmentOfMapObject.CARDIOLOGY, FirstBuilding, 0, "Examiantion room 3"
                    ),
                    new MapObjectMetrics(40, 120 - AllConstants.RECTANGLE_STROKE_THICKNESS, 80, 120 + AllConstants.RECTANGLE_STROKE_THICKNESS),
                    new MapObjectDoor(MapObjectDoorOrientation.RIGHT, 0, 0)
                );

            AllMapObjects.Add(examinationRoom1InBuilding1GroundLevel);

            MapObject examinationRoom2InBuilding1GroundLevel = new MapObject(
                   new Room(
                       TypeOfMapObject.EXAMINATION_ROOM, DepartmentOfMapObject.CARDIOLOGY, FirstBuilding, 0, "Room 4"
                   ),
                   new MapObjectMetrics(40, 240 - AllConstants.RECTANGLE_STROKE_THICKNESS, 100, 60 + AllConstants.RECTANGLE_STROKE_THICKNESS),
                   new MapObjectDoor(MapObjectDoorOrientation.RIGHT, 0, 0)
               );

            AllMapObjects.Add(examinationRoom2InBuilding1GroundLevel);

            MapObject waitingRoomBuilding1GroundLevel = new MapObject(
                  new Room(
                       TypeOfMapObject.WAITING_ROOM, DepartmentOfMapObject.CARDIOLOGY, FirstBuilding, 0, "Waiting room 1"
                  ),
                  new MapObjectMetrics(185, 165, 140, 90),
                  new MapObjectDoor(MapObjectDoorOrientation.NONE)
              );

            AllMapObjects.Add(waitingRoomBuilding1GroundLevel);

            MapObject wcForManBuilding1GroundLevel = new MapObject(
                  new Room(
                       TypeOfMapObject.WC, DepartmentOfMapObject.CARDIOLOGY, FirstBuilding, 0, "WC 1"
                  ),
                  new MapObjectMetrics(370, 120 - AllConstants.RECTANGLE_STROKE_THICKNESS, 70, 55 + AllConstants.RECTANGLE_STROKE_THICKNESS),
                  new MapObjectDoor(MapObjectDoorOrientation.LEFT, 0, 0)
              );

            AllMapObjects.Add(wcForManBuilding1GroundLevel);

            MapObject wcForWomanBuilding1GroundLevel = new MapObject(
                  new Room(
                    TypeOfMapObject.WC, DepartmentOfMapObject.CARDIOLOGY, FirstBuilding, 0
                  ),
                  new MapObjectMetrics(370, 175 - AllConstants.RECTANGLE_STROKE_THICKNESS, 70, 55 + AllConstants.RECTANGLE_STROKE_THICKNESS),
                  new MapObjectDoor(MapObjectDoorOrientation.LEFT, 0, 0)
              );

            AllMapObjects.Add(wcForWomanBuilding1GroundLevel);

            MapObject examiantionRoom3InBuilding1GroundLevel = new MapObject(
                  new Room(
                     TypeOfMapObject.EXAMINATION_ROOM, DepartmentOfMapObject.CARDIOLOGY, FirstBuilding, 0, "Room 5"
                  ),
                  new MapObjectMetrics(370, 230 - AllConstants.RECTANGLE_STROKE_THICKNESS, 70, 70 + AllConstants.RECTANGLE_STROKE_THICKNESS),
                  new MapObjectDoor(MapObjectDoorOrientation.LEFT, 0, 0)
              );

            AllMapObjects.Add(examiantionRoom3InBuilding1GroundLevel);
        }

        private void AddObjectsOnFirstFloorBuilding1()
        {
            MapObject examinationRoom1InBuilding1FirstFloor = new MapObject(
                    new Room(
                        TypeOfMapObject.EXAMINATION_ROOM, DepartmentOfMapObject.NEUROLOGY, FirstBuilding, 1,"Examiantion room 3"
                    ),
                    new MapObjectMetrics(40, 50, 145, 70),
                    new MapObjectDoor(MapObjectDoorOrientation.BOTTOM, 30, 0)
                );

            AllMapObjects.Add(examinationRoom1InBuilding1FirstFloor);

            MapObject wcForManBuilding1FirstFloor = new MapObject(
                 new Room(
                     TypeOfMapObject.WC, DepartmentOfMapObject.NEUROLOGY, FirstBuilding, 1, "WC 1"
                 ),
                 new MapObjectMetrics(185 - AllConstants.RECTANGLE_STROKE_THICKNESS, 50, 55 + AllConstants.RECTANGLE_STROKE_THICKNESS, 70),
                 new MapObjectDoor(MapObjectDoorOrientation.BOTTOM, 0, 0)
             );

            AllMapObjects.Add(wcForManBuilding1FirstFloor);

            MapObject wcForWomanBuilding1FirstFloor = new MapObject(
                  new Room(
                      TypeOfMapObject.WC, DepartmentOfMapObject.NEUROLOGY, FirstBuilding, 1, "WC 2"
                  ),
                  new MapObjectMetrics(240 - AllConstants.RECTANGLE_STROKE_THICKNESS, 50, 55 + AllConstants.RECTANGLE_STROKE_THICKNESS, 70),
                  new MapObjectDoor(MapObjectDoorOrientation.BOTTOM, 0, 0)
              );

            AllMapObjects.Add(wcForWomanBuilding1FirstFloor);

            MapObject examinationRoom2InBuilding1FirstFloor = new MapObject(
                  new Room(
                      TypeOfMapObject.EXAMINATION_ROOM, DepartmentOfMapObject.NEUROLOGY, FirstBuilding, 1, "Room 4"
                  ),
                  new MapObjectMetrics(40, 120 - AllConstants.RECTANGLE_STROKE_THICKNESS, 75, 120 + AllConstants.RECTANGLE_STROKE_THICKNESS),
                  new MapObjectDoor(MapObjectDoorOrientation.RIGHT, 0, 0)
              );

            AllMapObjects.Add(examinationRoom2InBuilding1FirstFloor);


            MapObject examinationRoom3InBuilding1FirstFloor = new MapObject(
                   new Room(
                       TypeOfMapObject.EXAMINATION_ROOM, DepartmentOfMapObject.NEUROLOGY, FirstBuilding, 1, "Room 4"
                   ),
                   new MapObjectMetrics(40, 240 - AllConstants.RECTANGLE_STROKE_THICKNESS, 120, 60 + AllConstants.RECTANGLE_STROKE_THICKNESS),
                   new MapObjectDoor(MapObjectDoorOrientation.RIGHT, 0, 0)
               );

            AllMapObjects.Add(examinationRoom3InBuilding1FirstFloor);

            MapObject operationRoom1InBuilding1FirstFloor = new MapObject(
                   new Room(
                       TypeOfMapObject.OPERATION_ROOM, DepartmentOfMapObject.NEUROLOGY, FirstBuilding, 1, "Operation room 1"
                   ),
                   new MapObjectMetrics(295 - AllConstants.RECTANGLE_STROKE_THICKNESS, 50, 145 + AllConstants.RECTANGLE_STROKE_THICKNESS, 70),
                   new MapObjectDoor(MapObjectDoorOrientation.BOTTOM, -30, 0)
               );


            AllMapObjects.Add(operationRoom1InBuilding1FirstFloor);

            MapObject operationRoom2InBuilding1FirstFloor = new MapObject(
                   new Room(
                       TypeOfMapObject.OPERATION_ROOM, DepartmentOfMapObject.NEUROLOGY, FirstBuilding, 1, "Operation room 1"
                   ),
                   new MapObjectMetrics(365, 120 - AllConstants.RECTANGLE_STROKE_THICKNESS, 75, 120 + AllConstants.RECTANGLE_STROKE_THICKNESS),
                   new MapObjectDoor(MapObjectDoorOrientation.LEFT, 0, 0)
               );


            AllMapObjects.Add(operationRoom2InBuilding1FirstFloor);

            MapObject operationRoom3InBuilding1FirstFloor = new MapObject(
                   new Room(
                       TypeOfMapObject.OPERATION_ROOM, DepartmentOfMapObject.NEUROLOGY, FirstBuilding, 1, "Operation room 1"
                   ),
                   new MapObjectMetrics(320, 240 - AllConstants.RECTANGLE_STROKE_THICKNESS, 120, 60 + AllConstants.RECTANGLE_STROKE_THICKNESS),
                   new MapObjectDoor(MapObjectDoorOrientation.LEFT, 0, 0)
               );


            AllMapObjects.Add(operationRoom3InBuilding1FirstFloor);

            MapObject waitingRoomBuilding1FirstFloor = new MapObject(
                 new Room(
                     TypeOfMapObject.WAITING_ROOM, DepartmentOfMapObject.NEUROLOGY, FirstBuilding, 1, "Waiting room 1"
                 ),
                 new MapObjectMetrics(165, 145, 150, 80),
                 new MapObjectDoor(MapObjectDoorOrientation.NONE)
             );

            AllMapObjects.Add(waitingRoomBuilding1FirstFloor);

        }

        private void AddObjectsOnGroundLevelBuilding2()
        {
            MapObject wc1ForManBuilding2GroundLevel = new MapObject(
                           new Room(
                               TypeOfMapObject.WC, DepartmentOfMapObject.PULMOLOGY, SecondBuilding, 0, "WC 3"
                           ),
                           new MapObjectMetrics(840, 50, 55, 70),
                           new MapObjectDoor(MapObjectDoorOrientation.BOTTOM, 0, 0)
                       );

            AllMapObjects.Add(wc1ForManBuilding2GroundLevel);

            MapObject wc1ForWomanBuilding2GroundLevel = new MapObject(
                  new Room(
                      TypeOfMapObject.WC, DepartmentOfMapObject.PULMOLOGY, SecondBuilding, 0, "WC 4"
                  ),
                  new MapObjectMetrics(895 - Constants.AllConstants.RECTANGLE_STROKE_THICKNESS, 50, 55, 70),
                  new MapObjectDoor(MapObjectDoorOrientation.BOTTOM, 0, 0)
              );
            AllMapObjects.Add(wc1ForWomanBuilding2GroundLevel);


            MapObject wc2ForManBuilding2GroundLevel = new MapObject(
               new Room(
                   TypeOfMapObject.WC, DepartmentOfMapObject.PULMOLOGY, SecondBuilding, 0, "WC 5"
               ),
               new MapObjectMetrics(840, 592, 55, 70),
               new MapObjectDoor(MapObjectDoorOrientation.TOP, 0, 0)
           );

            AllMapObjects.Add(wc2ForManBuilding2GroundLevel);

            MapObject wc2ForWomanBuilding2GroundLevel = new MapObject(
                  new Room(
                      TypeOfMapObject.WC, DepartmentOfMapObject.PULMOLOGY, SecondBuilding, 0, "WC 6"
                  ),
                  new MapObjectMetrics(895 - Constants.AllConstants.RECTANGLE_STROKE_THICKNESS, 592, 55, 70),
                  new MapObjectDoor(MapObjectDoorOrientation.TOP, 0, 0)
              );

            AllMapObjects.Add(wc2ForWomanBuilding2GroundLevel);


            MapObject waitingRoom1Building2GroundLevel = new MapObject(
                  new Room(
                      TypeOfMapObject.WAITING_ROOM, DepartmentOfMapObject.PULMOLOGY, SecondBuilding, 0, "Waiting room 2"
                  ),
                  new MapObjectMetrics(825, 140, 140, 150),
                  new MapObjectDoor(MapObjectDoorOrientation.NONE)
              );

            AllMapObjects.Add(waitingRoom1Building2GroundLevel);


            MapObject waitingRoom2Building2GroundLevel = new MapObject(
                 new Room(
                     TypeOfMapObject.WAITING_ROOM, DepartmentOfMapObject.PULMOLOGY, SecondBuilding, 0, "Waiting room 3"
                 ),
                 new MapObjectMetrics(825, 415, 140, 150),
                 new MapObjectDoor(MapObjectDoorOrientation.NONE)
             );

            AllMapObjects.Add(waitingRoom2Building2GroundLevel);


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


                MapObject examiantionRoomInBuilding2GroundLevel = new MapObject(
               new Room(
                   TypeOfMapObject.EXAMINATION_ROOM, DepartmentOfMapObject.PULMOLOGY, SecondBuilding, 0, "Examination room " + i
               ),
               new MapObjectMetrics(startXOfDrawing, yOfObject, widthOfObject, heightOfObject),
               new MapObjectDoor(MapObjectDoorOrientation.RIGHT, 0, 0)
            );

                AllMapObjects.Add(examiantionRoomInBuilding2GroundLevel);

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


                MapObject operationRoomInBuilding2GroundLevel = new MapObject(
               new Room(
                   TypeOfMapObject.OPERATION_ROOM, DepartmentOfMapObject.PULMOLOGY, SecondBuilding, 0, "Operation Room " + i
               ),
               new MapObjectMetrics(startXOfDrawing, yOfObject, widthOfObject, heightOfObject),
               new MapObjectDoor(MapObjectDoorOrientation.LEFT, 0, 0)
            );

                AllMapObjects.Add(operationRoomInBuilding2GroundLevel);

            }
        }

        private void AddObjectsOnFirstFloorBuilding2()
        {
            MapObject wc1ForManBuilding2FirstFloor = new MapObject(
                           new Room(
                               TypeOfMapObject.WC, DepartmentOfMapObject.GENERAL_MEDICINE, SecondBuilding, 1, "WC 3"
                           ),
                           new MapObjectMetrics(840, 50, 55, 70),
                           new MapObjectDoor(MapObjectDoorOrientation.BOTTOM, 0, 0)
                       );

            AllMapObjects.Add(wc1ForManBuilding2FirstFloor);

            MapObject wc1ForWomanBuilding2FirstFloor = new MapObject(
                  new Room(
                      TypeOfMapObject.WC, DepartmentOfMapObject.GENERAL_MEDICINE, SecondBuilding, 1, "WC 4"
                  ),
                  new MapObjectMetrics(895 - Constants.AllConstants.RECTANGLE_STROKE_THICKNESS, 50, 55, 70),
                  new MapObjectDoor(MapObjectDoorOrientation.BOTTOM, 0, 0)
              );
            AllMapObjects.Add(wc1ForWomanBuilding2FirstFloor);


            MapObject wc2ForManBuilding2FirstFloor = new MapObject(
               new Room(
                   TypeOfMapObject.WC, DepartmentOfMapObject.GENERAL_MEDICINE, SecondBuilding, 1, "WC 5"
               ),
               new MapObjectMetrics(840, 592, 55, 70),
               new MapObjectDoor(MapObjectDoorOrientation.TOP, 0, 0)
           );

            AllMapObjects.Add(wc2ForManBuilding2FirstFloor);

            MapObject wc2ForWomanBuilding2FirstFloor = new MapObject(
                  new Room(
                      TypeOfMapObject.WC, DepartmentOfMapObject.GENERAL_MEDICINE, SecondBuilding, 1, "WC 6"
                  ),
                  new MapObjectMetrics(895 - Constants.AllConstants.RECTANGLE_STROKE_THICKNESS, 592, 55, 70),
                  new MapObjectDoor(MapObjectDoorOrientation.TOP, 0, 0)
              );

            AllMapObjects.Add(wc2ForWomanBuilding2FirstFloor);


            MapObject waitingRoom1Building2FirstFloor = new MapObject(
                  new Room(
                      TypeOfMapObject.WAITING_ROOM, DepartmentOfMapObject.GENERAL_MEDICINE, SecondBuilding, 1, "Waiting room 2"
                  ),
                  new MapObjectMetrics(825, 140, 140, 150),
                  new MapObjectDoor(MapObjectDoorOrientation.NONE)
              );

            AllMapObjects.Add(waitingRoom1Building2FirstFloor);


            MapObject waitingRoom2Building2FirstFloor = new MapObject(
                 new Room(
                     TypeOfMapObject.WAITING_ROOM, DepartmentOfMapObject.GENERAL_MEDICINE, SecondBuilding, 1, "Waiting room 3"
                 ),
                 new MapObjectMetrics(825, 410, 120, 150),
                 new MapObjectDoor(MapObjectDoorOrientation.NONE)
             );

            AllMapObjects.Add(waitingRoom2Building2FirstFloor);


            for (int i = 0; i <= 2; i++)
            {
                double startXOfDrawing = 730;
                double startYOfDrawing = 50;
                double widthOfObject = 70;
                double heightOfObject = 90;

                double yOfObject = i == 0 ? startYOfDrawing : startYOfDrawing + heightOfObject * i - i * Constants.AllConstants.RECTANGLE_STROKE_THICKNESS;

                MapObject hospitalizationRoomInBuilding2FirstFloor = new MapObject(
               new Room(
                   TypeOfMapObject.HOSPITALIZATION_ROOM, DepartmentOfMapObject.GENERAL_MEDICINE, SecondBuilding, 1, "Examination room " + i
               ),
               new MapObjectMetrics(startXOfDrawing, yOfObject, widthOfObject, heightOfObject),
               new MapObjectDoor(MapObjectDoorOrientation.RIGHT, 0, 0)
            );

                AllMapObjects.Add(hospitalizationRoomInBuilding2FirstFloor);

            }

            for (int i = 0; i <= 2; i++)
            {
                double startXOfDrawing = 800 + 190;
                double startYOfDrawing = 50;
                double widthOfObject = 70;
                double heightOfObject = 90;

                double yOfObject = i == 0 ? startYOfDrawing : startYOfDrawing + heightOfObject * i - i * Constants.AllConstants.RECTANGLE_STROKE_THICKNESS;

                MapObject hospitalizationInBuilding2FirstFloor = new MapObject(
               new Room(
                   TypeOfMapObject.HOSPITALIZATION_ROOM, DepartmentOfMapObject.GENERAL_MEDICINE, SecondBuilding, 1, "Operation Room " + i
               ),
               new MapObjectMetrics(startXOfDrawing, yOfObject, widthOfObject, heightOfObject),
               new MapObjectDoor(MapObjectDoorOrientation.LEFT, 0, 0)
            );

                AllMapObjects.Add(hospitalizationInBuilding2FirstFloor);
            }

            for (int i = 0; i <= 2; i++)
            {
                double startXOfDrawing = 730;
                double startYOfDrawing = 320 + 78;
                double widthOfObject = 70;
                double heightOfObject = 90;

                double yOfObject = i == 0 ? startYOfDrawing : startYOfDrawing + heightOfObject * i - i * Constants.AllConstants.RECTANGLE_STROKE_THICKNESS;

                MapObject examinationInBuilding2FirstFloor = new MapObject(
               new Room(
                   TypeOfMapObject.HOSPITALIZATION_ROOM, DepartmentOfMapObject.GENERAL_MEDICINE, SecondBuilding, 1, "Examination room " + i
               ),
               new MapObjectMetrics(startXOfDrawing, yOfObject, widthOfObject, heightOfObject),
               new MapObjectDoor(MapObjectDoorOrientation.RIGHT, 0, 0)
            );

                AllMapObjects.Add(examinationInBuilding2FirstFloor);

            }

            MapObject operationRoom1InBuilding2FirstFloor = new MapObject(
               new Room(
                   TypeOfMapObject.OPERATION_ROOM, DepartmentOfMapObject.GENERAL_MEDICINE, SecondBuilding, 1, "Examination room 1"
               ),
               new MapObjectMetrics(800 + 190, 320 + 78, 70, 100),
               new MapObjectDoor(MapObjectDoorOrientation.LEFT, 0, 0)
            );

            AllMapObjects.Add(operationRoom1InBuilding2FirstFloor);

            MapObject operationRoom2InBuilding2FirstFloor = new MapObject(
               new Room(
                   TypeOfMapObject.OPERATION_ROOM, DepartmentOfMapObject.GENERAL_MEDICINE, SecondBuilding, 1, "Examination room 1"
               ),
               new MapObjectMetrics(800 + 170, 420 + 78 - AllConstants.RECTANGLE_STROKE_THICKNESS, 90, 170 - AllConstants.RECTANGLE_STROKE_THICKNESS),
               new MapObjectDoor(MapObjectDoorOrientation.LEFT, 0, 0)
            );

            AllMapObjects.Add(operationRoom2InBuilding2FirstFloor);
        }

        private void AddParkingPlacesToMap()
        {
            for (int i = 0; i < 12; i++)
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

            for (int i = 0; i < 12; i++)
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
        }

        private void AddRoadToMap()
        {
            MapObject roadPartOne = new MapObject(
                 new Road(),
                 new MapObjectMetrics(550, 500, 80, 200),
                 new MapObjectDoor(MapObjectDoorOrientation.NONE)
             );

            AllMapObjects.Add(roadPartOne);

            MapObject roadPartTwo = new MapObject(
                new Road(),
                new MapObjectMetrics(50, 500, 13 * 40, 65),
                new MapObjectDoor(MapObjectDoorOrientation.NONE)
            );

            AllMapObjects.Add(roadPartTwo);

            MapObject roadPartThree = new MapObject(
                new Road(),
                new MapObjectMetrics(565, 360, 40, 200),
                new MapObjectDoor(MapObjectDoorOrientation.NONE)
            );

            AllMapObjects.Add(roadPartThree);

            MapObject roadPartFour = new MapObject(
               new Road(),
               new MapObjectMetrics(40, 331, 690, 50),
               new MapObjectDoor(MapObjectDoorOrientation.NONE)
           );

            AllMapObjects.Add(roadPartFour);

            MapObject roadPartFive = new MapObject(
              new Road(),
              new MapObjectMetrics(240, 300, 40, 40),
              new MapObjectDoor(MapObjectDoorOrientation.NONE)
          );

            AllMapObjects.Add(roadPartFive);

            MapObject roadPartSix = new MapObject(
             new Road(),
             new MapObjectMetrics(565, 180, 40, 180),
             new MapObjectDoor(MapObjectDoorOrientation.NONE)
         );

            AllMapObjects.Add(roadPartSix);
        }
    }
}
