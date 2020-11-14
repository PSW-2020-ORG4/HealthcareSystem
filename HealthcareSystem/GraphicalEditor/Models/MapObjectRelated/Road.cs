using System;
using GraphicalEditor.Enumerations;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Shapes;
using System.Windows.Media;

namespace GraphicalEditor.Models.MapObjectRelated
{
    public class Road : MapObjectEntity
    {
        public Road(String description = "")
                   : base(TypeOfMapObject.ROAD, description)
        {
            FormatObjectDescription(Description);
        }
        
        public override void FormatObjectDescription(string description)
        {
            if (String.IsNullOrEmpty(description))
            {
                Description = MapObjectType.ObjectTypeFullName;
            }
        }

    }
}
