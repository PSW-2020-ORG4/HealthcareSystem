using GraphicalEditor.Models;
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

        public bool Update(MapObject mapObject)
        {
            return mapObjectService.Update(mapObject);
        }
    }
}
