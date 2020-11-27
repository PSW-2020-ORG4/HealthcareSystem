using Model.Manager;
using RestSharp;
using System.Collections.Generic;

namespace GraphicalEditor.Service
{
    public class ServerService
    {
        /*public List<NonConsumableEquipment> GetNonConsumableEquipmentByRoomNumber(int roomNumber)
        {
            var client = GetClient();
            var request = new RestRequest("api/nonConsumableEquipment/byRoomNumber/" + roomNumber, Method.GET);
            var response = client.Get<List<NonConsumableEquipment>>(request);
            List<NonConsumableEquipment> nonConsumableEquipment = (List<NonConsumableEquipment>)response.Data;
            return nonConsumableEquipment;
        }

        public List<ConsumableEquipment> GetConsumableEquipmentByRoomNumber(int roomNumber)
        {
            var client = GetClient();
            var request = new RestRequest("api/consumableEquipment/byRoomNumber/" + roomNumber, Method.GET);
            var response = client.Get<List<ConsumableEquipment>>(request);
            List<ConsumableEquipment> consumableEquipment = (List<ConsumableEquipment>)response.Data;
            return consumableEquipment;
        }*/

    }
}
