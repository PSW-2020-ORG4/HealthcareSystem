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
        public MapObjectDepartments DepartmentOfMapObject { get; set; }

        public string DepartmentFullName
        {
            get
            {
                switch (DepartmentOfMapObject)
                {
                    case MapObjectDepartments.GENERAL_MEDICINE:
                        return "Opšta medicina";
                    case MapObjectDepartments.PULMOLOGY:
                        return "Pulmologija";
                    case MapObjectDepartments.NEUROLOGY:
                        return "Neurologija";
                    case MapObjectDepartments.CARDIOLOGY:
                        return "Kardiologija";
                    default:
                        return "Opšta medicina";
                }
            }
        }


        public MapObjectDepartment(MapObjectDepartments mapObjectDepartment)
        {
            DepartmentOfMapObject = mapObjectDepartment;
        }
    }
}
