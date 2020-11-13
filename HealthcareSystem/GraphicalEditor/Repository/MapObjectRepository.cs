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
            IEnumerable<MapObject> allMapObjects = fileRepository.LoadMap();
            var singleMapObject = allMapObjects.FirstOrDefault(e => e.MapObjectEntity.Id == mapObject.MapObjectEntity.Id);
            if (singleMapObject != null)
            {
                List<MapObject> objectToSave = new List<MapObject>();
                foreach (MapObject anObject in allMapObjects)
                {
                        objectToSave.Add(anObject);
                    
                }

                for (int i = 0; i < objectToSave.Count; i++)
                {

                    if (objectToSave[i].MapObjectEntity.Id == mapObject.MapObjectEntity.Id)
                    {
                        objectToSave[i].MapObjectEntity.MapObjectType = mapObject.MapObjectEntity.MapObjectType;
                        objectToSave[i].MapObjectEntity.Description = mapObject.MapObjectEntity.Description;
                        objectToSave[i].MapObjectMetrics = mapObject.MapObjectMetrics;
                        objectToSave[i].Rectangle.Fill = mapObject.Rectangle.Fill;
                        objectToSave[i].MapObjectDoor = mapObject.MapObjectDoor;
                        objectToSave[i].MapObjectNameTextBlock = mapObject.MapObjectNameTextBlock;
                        break;
                    }
                }
                fileRepository.SaveMap(objectToSave);
            }
        }
    }
}
