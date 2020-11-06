using GraphicalEditor.Enumerations;
using System.Windows.Media;

namespace GraphicalEditor.Models.MapObjectRelated
{
    public class MapObjectType
    {
        public Enumerations.TypeOfMapObject TypeOfMapObject { get; set; }
        public string ObjectTypeFullName
        {
            get
            {
                switch (TypeOfMapObject)
                {
                    case Enumerations.TypeOfMapObject.BUILDING:
                        return "Zgrada";
                    case Enumerations.TypeOfMapObject.EXAMINATION_ROOM:
                        return "Soba za pregled";
                    case Enumerations.TypeOfMapObject.OPERATION_ROOM:
                        return "Operaciona sala";
                    case Enumerations.TypeOfMapObject.WAITING_ROOM:
                        return "Čekaonica";
                    case Enumerations.TypeOfMapObject.PARKING:
                        return "Parking";
                    case Enumerations.TypeOfMapObject.RESTAURANT:
                        return "Restoran";
                    case Enumerations.TypeOfMapObject.HOSPITALIZATION_ROOM:
                        return "Soba za oporavak";
                    case Enumerations.TypeOfMapObject.WC:
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
                    case Enumerations.TypeOfMapObject.BUILDING:
                        return "Z";
                    case Enumerations.TypeOfMapObject.EXAMINATION_ROOM:
                        return "SP";
                    case Enumerations.TypeOfMapObject.OPERATION_ROOM:
                        return "OS";
                    case Enumerations.TypeOfMapObject.WAITING_ROOM:
                        return "Č";
                    case Enumerations.TypeOfMapObject.PARKING:
                        return "P";
                    case Enumerations.TypeOfMapObject.RESTAURANT:
                        return "R";
                    case Enumerations.TypeOfMapObject.HOSPITALIZATION_ROOM:
                        return "SO";
                    case Enumerations.TypeOfMapObject.WC:
                        return "WC";
                    default:
                        return "O";
                }
            }
        }

        public MapObjectType(Enumerations.TypeOfMapObject mapObjectType)
        {
            TypeOfMapObject = mapObjectType;
        }



        public SolidColorBrush getColor()
        {
            switch (TypeOfMapObject)
            {
                
                case Enumerations.TypeOfMapObject.BUILDING:
                    return Brushes.White;
                case Enumerations.TypeOfMapObject.EXAMINATION_ROOM:
                    return Brushes.Lavender;
                case Enumerations.TypeOfMapObject.OPERATION_ROOM:
                    return Brushes.LightCyan;
                case Enumerations.TypeOfMapObject.WAITING_ROOM:
                    return Brushes.Honeydew;
                case Enumerations.TypeOfMapObject.PARKING:
                    return Brushes.CornflowerBlue;
                case Enumerations.TypeOfMapObject.RESTAURANT:
                    return Brushes.BlanchedAlmond;
                case Enumerations.TypeOfMapObject.HOSPITALIZATION_ROOM:
                    return Brushes.Aquamarine;
                case Enumerations.TypeOfMapObject.WC:
                    return Brushes.LightYellow;
                default:
                    return Brushes.Moccasin;
            }
        }
    }
}
