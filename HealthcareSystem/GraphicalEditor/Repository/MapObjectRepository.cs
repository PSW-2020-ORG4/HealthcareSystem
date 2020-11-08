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

        public void UpdateMapObject(MapObject mapObject)
        {
            fileRepository = new FileRepository("test.json");
            IEnumerable<MapObject> objects = fileRepository.LoadMap();
            var mObject = objects.FirstOrDefault(e => e.MapObjectEntity.Id == mapObject.MapObjectEntity.Id);
            if (mObject != null)
            {
                List<MapObject> objectToSave = new List<MapObject>();
                foreach (MapObject m in objects)
                {
                    objectToSave.Add(m);
                }
                for (int i = 0; i < objectToSave.Count; i++)
                {

                    if (objectToSave[i].MapObjectEntity.Id == mapObject.MapObjectEntity.Id)
                    {
                        objectToSave[i].MapObjectEntity.MapObjectType = mapObject.MapObjectEntity.MapObjectType;
                        objectToSave[i].MapObjectEntity.Description = mapObject.MapObjectEntity.Description;
                        objectToSave[i].MapObjectMetrics = mapObject.MapObjectMetrics;
                        //Uncoment this if you want to improve editing of mapObjects
                        //savingList[i].Rectangle = mapObject.Rectangle;  
                        //savingList[i].MapObjectDoor = mapObject.MapObjectDoor;                          
                        objectToSave[i].MapObjectNameTextBlock = mapObject.MapObjectNameTextBlock;
                        break;
                    }
                }
                fileRepository.SaveMap(objectToSave);
            }
        }
    }
}
