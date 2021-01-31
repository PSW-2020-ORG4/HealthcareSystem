using GraphicalEditor.Models;
using GraphicalEditor.Models.MapObjectRelated;
using GraphicalEditor.Repository;
using GraphicalEditor.Services;
using GraphicalEditor.Services.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GraphicalEditor.Controllers
{
    class MapObjectController
    {
        private IMapObjectServices mapObjectService;

        public MapObjectController(IMapObjectServices _mapObjectServices)
        {
            mapObjectService = _mapObjectServices;
        }

        public void UpdateMapObject(MapObject mapObject)
        {
            mapObjectService.UpdateMapObject(mapObject);
        }

        public MapObject GetMapObjectById(long id)
        {
            return mapObjectService.GetMapObjectById(id);
        }

        public List<MapObject> SearchMapObjects(MapObjectType searchedMapObjectType)
        {
            return mapObjectService.SearchMapObjects(searchedMapObjectType);
        }

        public List<MapObject> GetNeighboringRoomsForRoom(MapObject room)
        {
            return mapObjectService.GetNeighboringRoomsForRoom(room);
        }
    }
}
