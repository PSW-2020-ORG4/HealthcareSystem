using GraphicalEditor.Models;
using GraphicalEditor.Models.Equipment;
using RestSharp;
using System;
using System.Collections.Generic;

namespace GraphicalEditor.Service
{
    public class EquipementService : GenericHTTPService
    {
        public string AddEquipment(Equipment equipment)
        {
            IRestResponse response = AddHTTPPostRequest("equipment", equipment);
            return response.Content;
        }
        public void AddEquipmentToRoom(MapObject mapObject, Equipment equipment)
        {
            if (mapObject.CheckIfDBAddableRoom())
            {
                String JSONContent = EquipmentInRoomToJSONConverter(mapObject, equipment);
                AddHTTPPostRequest("equipmentInRooms", JSONContent);
            }
        }

        private string EquipmentInRoomToJSONConverter(MapObject mapObject, Equipment equipment)
        {
            String JSONContent = "'IdEquipment': " + equipment.Id;
            JSONContent += ",'RoomNumber': " + mapObject.MapObjectEntity.Id;
            JSONContent += ",'Quantity': " + equipment.Quantity;

            return JSONContent;
        }

        public List<Equipment> GetEquipmentByRoomNumber(int roomNumber)
        {
            return (List<Equipment>)HTTPGetRequest<Equipment>("equipmentInRooms/byRoomNumber/ " + roomNumber);
        }
    }
}
