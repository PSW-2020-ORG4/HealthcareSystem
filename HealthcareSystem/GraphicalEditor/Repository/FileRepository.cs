using GraphicalEditor.Enumerations;
using GraphicalEditor.Models;
using GraphicalEditor.Models.MapObjectRelated;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GraphicalEditor.Repository
{
    public class FileRepository : IRepository
    {

        private String Path { get; set; }

        public FileRepository(String path)
        {
            Path = path;
        }

        public void SaveMap(List<MapObject> allMapObjects)
        {
            JsonSerializer serializer = new JsonSerializer();
            serializer.TypeNameHandling = TypeNameHandling.All;
            serializer.Formatting = Formatting.Indented;
            serializer.ContractResolver = new ContractResolver();
            serializer.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
            using (StreamWriter writer = new StreamWriter("test.json"))
            using (JsonWriter jwriter = new JsonTextWriter(writer))
            {
                serializer.Serialize(jwriter, allMapObjects);
            }
        }

        public IEnumerable<MapObject> LoadMap()
        {
            string jsonString = File.Exists("test.json") ? File.ReadAllText("test.json") : "";
            var settings = new JsonSerializerSettings() { TypeNameHandling = TypeNameHandling.All };
            if (!string.IsNullOrEmpty(jsonString))
            {
                return JsonConvert.DeserializeObject<List<MapObject>>(jsonString, settings);
            }
            else
            {
                return new List<MapObject>();
            }
        }
        public void UpdateMapObject(MapObject mapObject)
        {
            IEnumerable<MapObject> allMapObjects = LoadMap();
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
                SaveMap(objectToSave);
            }
        }
    }
}
