using GraphicalEditor.Models;
using GraphicalEditor.Repository.Intefrace;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GraphicalEditor.Repository
{
    public class MapObjectRepository : IMapObjectRepository
    {
        protected IEnumerable<MapObject> objects;
        protected IRepository fileRepository;
        protected MapObjectRepository() {
            objects = fileRepository.LoadMap();
        }

        public MapObjectRepository(string path)
        {
        }

        public bool Update(MapObject mapObject)
        {

            fileRepository = new FileRepository("test.json");
            IEnumerable<MapObject> objects = fileRepository.LoadMap();
            var ob = objects.FirstOrDefault(e => e.MapObjectEntity.Id == mapObject.MapObjectEntity.Id);
            if (ob != null)
            {
                List<MapObject> savingList = new List<MapObject>();
                foreach (MapObject m in objects)
                {
                    savingList.Add(m);
                }
                for (int i = 0; i < savingList.Count; i++)
                {

                    if (savingList[i].MapObjectEntity.Id == mapObject.MapObjectEntity.Id)
                    {
                        savingList[i].MapObjectEntity.MapObjectType = mapObject.MapObjectEntity.MapObjectType;
                        savingList[i].MapObjectEntity.Description = mapObject.MapObjectEntity.Description;
                        break;
                    }
                }
                fileRepository.SaveMap(savingList);
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
