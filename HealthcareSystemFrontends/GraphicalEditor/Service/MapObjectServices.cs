using GraphicalEditor.Models;
using GraphicalEditor.Repository;
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
        private IRepository _fileRepository;

        public MapObjectServices(IRepository fileRepository) {
            _fileRepository = fileRepository;
        }

        public void UpdateMapObject(MapObject mapObject)
        {
            _fileRepository.UpdateMapObject(mapObject);
        }
    }
}
