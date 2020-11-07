using GraphicalEditor.Enumerations;
using System.Windows.Media;

namespace GraphicalEditor.Models.MapObjectRelated
{
    public class MapObjectType
    {
        public TypeOfMapObject TypeOfMapObject { get; set; }
        public string ObjectTypeFullName
        {
            get
            {
                switch (TypeOfMapObject)
                {
                    case TypeOfMapObject.BUILDING:
                        return "Zgrada";
                    case TypeOfMapObject.EXAMINATION_ROOM:
                        return "Soba za pregled";
                    case TypeOfMapObject.OPERATION_ROOM:
                        return "Operaciona sala";
                    case TypeOfMapObject.WAITING_ROOM:
                        return "Čekaonica";
                    case TypeOfMapObject.PARKING:
                        return "Parking";
                    case TypeOfMapObject.RESTAURANT:
                        return "Restoran";
                    case TypeOfMapObject.HOSPITALIZATION_ROOM:
                        return "Soba za oporavak";
                    case TypeOfMapObject.WC:
                        return "WC";
                    default:
                        return "Objekat";
                }
            }
        }


        public string ObjectTypeNameAbbreviation
        {
            get
            {
                switch (TypeOfMapObject)
                {
                    case TypeOfMapObject.BUILDING:
                        return "Z";
                    case TypeOfMapObject.EXAMINATION_ROOM:
                        return "SP";
                    case TypeOfMapObject.OPERATION_ROOM:
                        return "OS";
                    case TypeOfMapObject.WAITING_ROOM:
                        return "Č";
                    case TypeOfMapObject.PARKING:
                        return "P";
                    case TypeOfMapObject.RESTAURANT:
                        return "R";
                    case TypeOfMapObject.HOSPITALIZATION_ROOM:
                        return "SO";
                    case TypeOfMapObject.WC:
                        return "WC";
                    default:
                        return "O";
                }
            }
        }

        public MapObjectType(TypeOfMapObject mapObjectType)
        {
            TypeOfMapObject = mapObjectType;
        }

        public SolidColorBrush getColor()
        {
            switch (TypeOfMapObject)
            {
                
                case TypeOfMapObject.BUILDING:
                    return Brushes.White;
                case TypeOfMapObject.EXAMINATION_ROOM:
                    return Brushes.Lavender;
                case TypeOfMapObject.OPERATION_ROOM:
                    return Brushes.LightCyan;
                case TypeOfMapObject.WAITING_ROOM:
                    return Brushes.Honeydew;
                case TypeOfMapObject.PARKING:
                    return Brushes.CornflowerBlue;
                case TypeOfMapObject.RESTAURANT:
                    return Brushes.BlanchedAlmond;
                case TypeOfMapObject.HOSPITALIZATION_ROOM:
                    return Brushes.Aquamarine;
                case TypeOfMapObject.WC:
                    return Brushes.LightYellow;
                default:
                    return Brushes.Moccasin;
            }
        }
    }
}
