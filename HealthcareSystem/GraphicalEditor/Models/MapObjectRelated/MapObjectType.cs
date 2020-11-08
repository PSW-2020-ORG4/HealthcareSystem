using GraphicalEditor.Enumerations;
using System;
using System.Collections.Generic;
using System.Windows.Documents;
using System.Windows.Media;

namespace GraphicalEditor.Models.MapObjectRelated
{
    public class MapObjectType
    {
        public TypeOfMapObject TypeOfMapObject { get; set; }

        public MapObjectType()
        { 
        }
   
        public string ObjectTypeFullName
        {
            get
            {
                switch (TypeOfMapObject)
                {
                    case TypeOfMapObject.BUILDING:
                        return "Building";
                    case TypeOfMapObject.EXAMINATION_ROOM:
                        return "ExaminationRoom";
                    case TypeOfMapObject.OPERATION_ROOM:
                        return "OperationRoom";
                    case TypeOfMapObject.WAITING_ROOM:
                        return "WaitingRoom";
                    case TypeOfMapObject.PARKING:
                        return "Parking";
                    case TypeOfMapObject.RESTAURANT:
                        return "Restaurant";
                    case TypeOfMapObject.HOSPITALIZATION_ROOM:
                        return "HospitalizationRoom";
                    case TypeOfMapObject.WC:
                        return "WC";
                    case TypeOfMapObject.ROAD:
                        return "Put";
                    default:
                        return "Objekat";
                }
            }
            set {
                switch (value.Trim())
                {
                    case "Building":
                        TypeOfMapObject = TypeOfMapObject.BUILDING;
                        break;
                    case "ExaminationRoom":
                         TypeOfMapObject = TypeOfMapObject.EXAMINATION_ROOM;
                        break;
                    case "OperationRoom":
                        TypeOfMapObject = TypeOfMapObject.OPERATION_ROOM;
                        break;
                    case "WaitingRoom":
                        TypeOfMapObject = TypeOfMapObject.WAITING_ROOM;
                        break;
                    case "Parking":
                        TypeOfMapObject = TypeOfMapObject.PARKING;
                        break;
                    case "Restaurant":
                        TypeOfMapObject = TypeOfMapObject.RESTAURANT;
                        break;
                    case "HospitalizationRoom":
                        TypeOfMapObject = TypeOfMapObject.HOSPITALIZATION_ROOM;
                        break;
                    case "WC":
                        TypeOfMapObject = TypeOfMapObject.WC;
                        break;
                    default:
                        break;                    
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
                    case TypeOfMapObject.ROAD:
                        return "";
                    default:
                        return "O";
                }
            }
        }

        public MapObjectType(TypeOfMapObject mapObjectType)
        {
            TypeOfMapObject = mapObjectType;
        }

        public SolidColorBrush ObjectTypeColor
        {   
            get
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
                    case TypeOfMapObject.ROAD:
                        return Brushes.LightGray;
                    default:
                        return Brushes.Moccasin;
                }
            }

        }

        public List<String> AllMapObjectTypes
        {
            get
            {
                List<String> allMapObjectTypes = new List<String>();
                Array typesOfMapObjects = Enum.GetValues(typeof(TypeOfMapObject));
                foreach (TypeOfMapObject enumValue in typesOfMapObjects)
                {
                    MapObjectType value = new MapObjectType(enumValue);
                    allMapObjectTypes.Add(value.ObjectTypeFullName);
                }

                return allMapObjectTypes;
            }
        }
    }
}
