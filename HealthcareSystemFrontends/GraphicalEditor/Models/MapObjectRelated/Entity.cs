using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GraphicalEditor.Models.MapObjectRelated
{
    public class Entity
    {
        private static long TestId { get; set; } = 0;
        public long Id { get; set; }

        public Entity()
        {
            Id = GenerateId();
        }

        private long GenerateId()
        {
            return TestId++;
        }

        private long GetMaxId(List<MapObject> allMapObjects)
        {
            return allMapObjects.Count == 0 ? 0 : allMapObjects.Count;
        }
    }
}
