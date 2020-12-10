using GraphicalEditor.Models;
using GraphicalEditor.Models.MapObjectRelated;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GraphicalEditor.Services.Interface
{
    public interface IMapObjectServices
    {
        void UpdateMapObject(MapObject mapObject);
        MapObject GetMapObjectById(long id);
        List<MapObject> SearchMapObjects(MapObjectType searchedMapObjectType);
    }
}
