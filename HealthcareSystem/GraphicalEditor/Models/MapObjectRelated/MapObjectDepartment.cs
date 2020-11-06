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
        public DepartmentOfMapObject DepartmentOfMapObject { get; set; }

        public string DepartmentFullName
        {
            get
            {
                switch (DepartmentOfMapObject)
                {
                    case DepartmentOfMapObject.GENERAL_MEDICINE:
                        return "Opšta medicina";
                    case DepartmentOfMapObject.PULMOLOGY:
                        return "Pulmologija";
                    case DepartmentOfMapObject.NEUROLOGY:
                        return "Neurologija";
                    case DepartmentOfMapObject.CARDIOLOGY:
                        return "Kardiologija";
                    default:
                        return "Opšta medicina";
                }
            }
        }


        public MapObjectDepartment(DepartmentOfMapObject mapObjectDepartment)
        {
            DepartmentOfMapObject = mapObjectDepartment;
        }
    }
}
