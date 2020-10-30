using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GraphicalEditor.Models.MapObjectRelated
{
   public  class IdGenerator
    {
        private long _id;

        public IdGenerator()
        {
            _id = GetNextID();
        }

        public long GetNextID()
        {
            MainWindow wnd = (MainWindow)App.Current.MainWindow;

            return GetMaxId(wnd.ObjectsList) + 1;
        }
        private long GetMaxId(List<MapObject> objects)
        {
            return objects.Count() == 0 ? 0 : objects.Max(entity => entity.MapObjectEntity.Id);
        }
        public long Id { get => _id; set => _id = value; }
    }
}
