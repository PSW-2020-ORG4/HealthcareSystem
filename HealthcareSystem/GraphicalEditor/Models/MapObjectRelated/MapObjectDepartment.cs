using GraphicalEditor.Enumerations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GraphicalEditor.Models.MapObjectRelated
{
    public class MapObjectDepartment
    {
        public Enumerations.DepartmentOfMapObject DepartmentOfMapObject { get; set; }

        public string DepartmentFullName
        {
            get
            {
                switch (DepartmentOfMapObject)
                {
                    case Enumerations.DepartmentOfMapObject.GENERAL_MEDICINE:
                        return "Opšta medicina";
                    case Enumerations.DepartmentOfMapObject.PULMOLOGY:
                        return "Pulmologija";
                    case Enumerations.DepartmentOfMapObject.NEUROLOGY:
                        return "Neurologija";
                    case Enumerations.DepartmentOfMapObject.CARDIOLOGY:
                        return "Kardiologija";
                    default:
                        return "Opšta medicina";
                }
            }
        }


        public MapObjectDepartment(Enumerations.DepartmentOfMapObject mapObjectDepartment)
        {
            DepartmentOfMapObject = mapObjectDepartment;
        }
    }
}
