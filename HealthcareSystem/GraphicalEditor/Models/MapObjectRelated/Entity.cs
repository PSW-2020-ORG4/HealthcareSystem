using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GraphicalEditor.Models.MapObjectRelated
{
    public class Entity
    {
        public long Id { get; set; }

        public Entity()
        {
            Id = GenerateId();
        }

        private long GenerateId()
        {
            MainWindow mainWindow = (MainWindow)App.Current.MainWindow;
            return GetMaxId(mainWindow.AllMapObjects) + 1;
        }

        private long GetMaxId(List<MapObject> allMapObjects)
        {
            return allMapObjects.Count == 0 ? 0 : allMapObjects.Count;
        }
    }
}
