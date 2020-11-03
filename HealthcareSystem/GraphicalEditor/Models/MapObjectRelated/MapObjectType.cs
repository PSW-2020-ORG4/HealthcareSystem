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
        public MapObjectTypes TypeOfMapObject { get; set; }

        public MapObjectType(MapObjectTypes mapObjectType)
        {
            TypeOfMapObject = mapObjectType;
        }

        public SolidColorBrush getColor()
        {
            switch (TypeOfMapObject)
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

        public string getObjectTypeFullName()
        {
            switch(TypeOfMapObject)
            {
                case MapObjectTypes.BUILDING:
                    return "Zgrada";
                case MapObjectTypes.EXAMINATION_ROOM:
                    return "Soba za pregled";
                case MapObjectTypes.OPERATION_ROOM:
                    return "Operaciona sala";
                case MapObjectTypes.WAITING_ROOM:
                    return "Čekaonica";
                case MapObjectTypes.PARKING:
                    return "Parking";
                case MapObjectTypes.RESTAURANT:
                    return "Restoran";
                case MapObjectTypes.HOSPITALIZATION_ROOM:
                    return "Soba za oporavak";
                case MapObjectTypes.WC:
                    return "WC";
                default:
                    return "Objekat";
            }
        }

        public string getObjectTypeNameAbbreviation()
        {
            switch (TypeOfMapObject)
            {
                case MapObjectTypes.BUILDING:
                    return "Z";
                case MapObjectTypes.EXAMINATION_ROOM:
                    return "SP";
                case MapObjectTypes.OPERATION_ROOM:
                    return "OS";
                case MapObjectTypes.WAITING_ROOM:
                    return "Č";
                case MapObjectTypes.PARKING:
                    return "P";
                case MapObjectTypes.RESTAURANT:
                    return "R";
                case MapObjectTypes.HOSPITALIZATION_ROOM:
                    return "SO";
                case MapObjectTypes.WC:
                    return "WC";
                default:
                    return "O";
            }
        }
    }
}
