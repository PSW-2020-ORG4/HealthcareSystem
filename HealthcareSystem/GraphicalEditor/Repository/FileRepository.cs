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
    }
}
