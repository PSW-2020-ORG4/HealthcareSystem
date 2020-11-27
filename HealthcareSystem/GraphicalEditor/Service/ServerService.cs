using GraphicalEditor.Enumerations;
using GraphicalEditor.Models;
using GraphicalEditor.Models.MapObjectRelated;
using Model.Manager;
using RestSharp;
using System;
using System.Collections.Generic;

namespace GraphicalEditor.Service
{
    public class ServerService
    {
        public void AddAllRooms()
        {
            var client = new RestSharp.RestClient("http://localhost:61631/");
            List<MapObject> allMapObjects = MainWindow._allMapObjects;

            foreach(MapObject mapObject in allMapObjects)
            {
                if (mapObject.MapObjectEntity.MapObjectType.TypeOfMapObject == TypeOfMapObject.EXAMINATION_ROOM ||
                    mapObject.MapObjectEntity.MapObjectType.TypeOfMapObject == TypeOfMapObject.HOSPITALIZATION_ROOM ||
                    mapObject.MapObjectEntity.MapObjectType.TypeOfMapObject == TypeOfMapObject.OPERATION_ROOM)
                {
                    string content = "'Id':";
                    content += mapObject.MapObjectEntity.Id + ",'Usage':";
                    content += (int)mapObject.MapObjectEntity.MapObjectType.TypeOfMapObject;

                    var request = new RestRequest("api/room");

                    request.AddJsonBody(content);
                    IRestResponse response = client.Post(request);
                }

            }
        }

        public List<NonConsumableEquipment> GetNonConsumableEquipmentByRoomNumber(int roomNumber)
        {
            var client = new RestSharp.RestClient("http://localhost:61631/");
            var request = new RestRequest("api/nonConsumableEquipment/byRoomNumber/"+roomNumber, Method.GET);
            var response = client.Get<List<NonConsumableEquipment>>(request);
            List<NonConsumableEquipment> nonConsumableEquipment =(List<NonConsumableEquipment>) response.Data;
            return nonConsumableEquipment;
        }

        public List<ConsumableEquipment> GetConsumableEquipmentByRoomNumber(int roomNumber)
        {
            var client = new RestSharp.RestClient("http://localhost:61631/");
            var request = new RestRequest("api/consumableEquipment/byRoomNumber/" + roomNumber, Method.GET);
            var response = client.Get<List<ConsumableEquipment>>(request);
            List<ConsumableEquipment> consumableEquipment = (List<ConsumableEquipment>)response.Data;
            return consumableEquipment;
        }

        public void AddEquipment()
        {
            List<MapObject> allMapObjects = MainWindow._allMapObjects;
            var client = new RestSharp.RestClient("http://localhost:61631/");

            int typeOfNonConsumable = 0;
            int equipmentID = 0;
            bool isConsumeable = false;
            foreach (MapObject mapObject in allMapObjects)
            {
                if (mapObject.MapObjectEntity.MapObjectType.TypeOfMapObject == TypeOfMapObject.EXAMINATION_ROOM ||
                       mapObject.MapObjectEntity.MapObjectType.TypeOfMapObject == TypeOfMapObject.HOSPITALIZATION_ROOM ||
                       mapObject.MapObjectEntity.MapObjectType.TypeOfMapObject == TypeOfMapObject.OPERATION_ROOM)
                {
                    if (!isConsumeable)
                    {
                        Random randomNumber = new Random();
                        string content = "'Id': " + ++equipmentID;
                        content += ",'Type': " + (randomNumber.Next(5));
                        var request = new RestRequest("api/equipment");
                        request.AddJsonBody(content);
                        IRestResponse response = client.Post(request);
                        isConsumeable = true;
                    }
                    else
                    {
                        string content = "'Id': " + ++equipmentID;
                        Random randomNumber = new Random();
                        content += ",'Type': " + (randomNumber.Next(4));
                        content += ",'Quantity': " + (equipmentID + mapObject.MapObjectEntity.Id) * 2;
                        var request = new RestRequest("api/equipment");
                        request.AddJsonBody(content);
                        IRestResponse response = client.Post(request);
                        isConsumeable = false;

                    }
                }
            }
        }

        public void AddEquipmentInRooms()
        {
            List<MapObject> allMapObjects = MainWindow._allMapObjects;
            var client = new RestSharp.RestClient("http://localhost:61631/");
            int equipmentId = 0;


            foreach (MapObject mapObject in allMapObjects)
            {
                if (mapObject.MapObjectEntity.MapObjectType.TypeOfMapObject == TypeOfMapObject.EXAMINATION_ROOM ||
                       mapObject.MapObjectEntity.MapObjectType.TypeOfMapObject == TypeOfMapObject.HOSPITALIZATION_ROOM ||
                       mapObject.MapObjectEntity.MapObjectType.TypeOfMapObject == TypeOfMapObject.OPERATION_ROOM)
                {
                    string content = "'IdEquipment': " + ++equipmentId;
                    content += ",'RoomNumber': " + mapObject.MapObjectEntity.Id;
                    content += ",'Quantity': " + (equipmentId + mapObject.MapObjectEntity.Id) * 2;
                    var request = new RestRequest("api/equipmentInRooms");
                    request.AddJsonBody(content);
                    IRestResponse response = client.Post(request);
                }
            }          
        }       

    }
}
