using GraphicalEditor.Models;
using GraphicalEditor.Repository;
using GraphicalEditor.Repository.Intefrace;
using GraphicalEditor.Services.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GraphicalEditor.Services
{
    public class MapObjectServices : IMapObjectServices
    {
        private IMapObjectRepository mapObjectRepository;

        public MapObjectServices(IMapObjectRepository _mapObjectRepository) {
            mapObjectRepository = _mapObjectRepository;
        }

        public bool Update(MapObject mapObject)
        {
           return mapObjectRepository.Update(mapObject);
        }
    }
}
