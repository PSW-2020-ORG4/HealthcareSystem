using GraphicalEditor.Enumerations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace GraphicalEditor.Models.MapObjectRelated
{
    public class MapObjectType
    {
        private MapObjectTypes _mapObjectType;

        public MapObjectType()
        {
            this._mapObjectType = MapObjectTypes.PARKING;
        }
        public MapObjectType(MapObjectTypes mapObjectType)
        {
            this._mapObjectType = mapObjectType;
        }
        public MapObjectTypes MapObjectTypes { get => _mapObjectType; set => _mapObjectType = value; }
        public SolidColorBrush getColor()
        {
            switch (this._mapObjectType)
            {
                case MapObjectTypes.BUILDING:
                    return Brushes.White;
                case MapObjectTypes.EXAMINATION_ROOM:
                    return Brushes.Green;
                case MapObjectTypes.OPERATION_ROOM:
                    return Brushes.Red;
                case MapObjectTypes.WAITING_ROOM:
                    return Brushes.Purple;
                case MapObjectTypes.PARKING:
                    return Brushes.LightBlue;
                case MapObjectTypes.RESTAURANT:
                    return Brushes.LightGray;
                case MapObjectTypes.HOSPITALIZATION_ROOM:
                    return Brushes.Orange;
                default:
                    return Brushes.Yellow;
            }
        }
    }
}
