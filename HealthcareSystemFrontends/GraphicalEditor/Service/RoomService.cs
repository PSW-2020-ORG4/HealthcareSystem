using GraphicalEditor.Enumerations;
using GraphicalEditor.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GraphicalEditor.Service
{
    public class RoomService : GenericHTTPService
    {
        public void AddRoom(MapObject mapObject)
        {            
            String JSONContent = RoomToJSONStringConverter(mapObject);
            AddHTTPPostRequest("room", JSONContent);
        }

        private string RoomToJSONStringConverter(MapObject mapObject)
        {
            string JSONContent = "'Id':";
            JSONContent += mapObject.MapObjectEntity.Id + ",'Usage':";
            JSONContent += (int)mapObject.MapObjectEntity.MapObjectType.TypeOfMapObject;

            return JSONContent;
        }
    }
}
