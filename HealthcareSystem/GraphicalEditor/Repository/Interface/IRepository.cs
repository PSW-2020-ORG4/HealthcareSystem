using GraphicalEditor.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GraphicalEditor.Repository
{
    public interface IRepository
    {
        void SaveMap(List<MapObject> allMapObjects);
        IEnumerable<MapObject> LoadMap();       
    }
}
