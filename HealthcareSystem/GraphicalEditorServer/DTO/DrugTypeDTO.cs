using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GraphicalEditorServer.DTO
{
    public class DrugTypeDTO
    {
        public int Id { get; set; }
        public string Type { get; set; }
        public string Purpose { get; set; }

        public DrugTypeDTO() { }

        public DrugTypeDTO(string type, string purpose)
        {

            this.Type = type;
            this.Purpose = purpose;
        }

        public DrugTypeDTO(DrugTypeDTO drugType)
        {

            this.Type = drugType.Type;
            this.Purpose = drugType.Purpose;
        }
    }
}
