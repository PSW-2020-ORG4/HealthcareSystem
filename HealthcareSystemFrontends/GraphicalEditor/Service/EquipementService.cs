using GraphicalEditor.Models;
using GraphicalEditor.Models.Equipment;
using System;
using System.Collections.Generic;

namespace GraphicalEditor.Service
{
    public class EquipementService : GenericHTTPService
    {
        public void AddConsumableEquipment(ConsumableEquipment consumableEquipment)
        {
            String JSONContent = ConsumableEquipmentToJSONConverter(consumableEquipment);
            AddHTTPPostRequest("equipment/consumable", JSONContent);
        }

        private string ConsumableEquipmentToJSONConverter(ConsumableEquipment consumableEquipment)
        {
            String JSONContent = "'Id': " + consumableEquipment.Id;
            JSONContent += ",'Type': " + (int)consumableEquipment.Type;
            JSONContent += ",'Quantity': " + consumableEquipment.Quantity;

            return JSONContent;
        }


        public void AddNonConsumableEquipment(NonConsumableEquipment nonConsumableEquipment)
        {
            String JSONContent = NonConsumableEquipmentToJSONConverter(nonConsumableEquipment);
            AddHTTPPostRequest("equipment/nonconsumable", JSONContent);
        }

        private string NonConsumableEquipmentToJSONConverter(NonConsumableEquipment NonConsumableEquipment)
        {
            String JSONContent = "'Id': " + NonConsumableEquipment.Id;
            JSONContent += ",'Type': " + (int)NonConsumableEquipment.Type;

            return JSONContent;
        }
        

        public void AddConsumableEquipmentToRoom(MapObject mapObject, ConsumableEquipment consumableEquipment)
        {
            if (mapObject.CheckIfDBAddableRoom())
            {
                String JSONContent = NonEquipmentInRoomToJSONConverter(mapObject, consumableEquipment);
                AddHTTPPostRequest("equipmentInRooms", JSONContent);
            }
        }

        private string NonEquipmentInRoomToJSONConverter(MapObject mapObject, ConsumableEquipment consumableEquipment)
        {
            String JSONContent = "'IdEquipment': " + consumableEquipment.Id;
            JSONContent += ",'RoomNumber': " + mapObject.MapObjectEntity.Id;
            JSONContent += ",'Quantity': " + consumableEquipment.Quantity;

            return JSONContent;
        }

        public void AddNonConsumableEquipmentToRoom(MapObject mapObject, NonConsumableEquipment nonConsumableEquipment)
        {
            if (mapObject.CheckIfDBAddableRoom())
            {
                String JSONContent = NonEquipmentInRoomToJSONConverter(mapObject, nonConsumableEquipment);
                AddHTTPPostRequest("equipmentInRooms", JSONContent);
            }
        }

        private string NonEquipmentInRoomToJSONConverter(MapObject mapObject, NonConsumableEquipment nonConsumableEquipment)
        {
            String JSONContent = "'IdEquipment': " + nonConsumableEquipment.Id;
            JSONContent += ",'RoomNumber': " + mapObject.MapObjectEntity.Id;
            JSONContent += ",'Quantity': -1";

            return JSONContent;
        }

        public List<NonConsumableEquipment> GetNonConsumableEquipmentByRoomNumber(int roomNumber)
        {
            return (List<NonConsumableEquipment>)HTTPGetRequest<NonConsumableEquipment>("nonConsumableEquipment/byRoomNumber/ " + roomNumber);
        }

        public List<ConsumableEquipment> GetConsumableEquipmentByRoomNumber(int roomNumber)
        {
            return (List<ConsumableEquipment>)HTTPGetRequest<ConsumableEquipment>("consumableEquipment/byRoomNumber/" + roomNumber);
        }
    }
}
