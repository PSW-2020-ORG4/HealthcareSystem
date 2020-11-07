using GraphicalEditor.Models;
using GraphicalEditor.Repository.Intefrace;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GraphicalEditor.Repository
{
    class MapObjectRepository : IMapObjectRepository
    {
        protected IEnumerable<MapObject> objects;
        protected IRepository fileRepository;
        protected MapObjectRepository() {
            objects = fileRepository.LoadMap();
        }

        public bool Update(MapObject mapObject)
        {
            var ob = objects.FirstOrDefault(e => e.MapObjectEntity.Id == mapObject.MapObjectEntity.Id);
            if (ob != null)
            {
                objects.ToList().Remove(ob);
                objects.ToList().Add(mapObject);
                fileRepository.SaveMap(objects.ToList());
                return true;
            }else{
                 return false;
            }
        }
    }
}
