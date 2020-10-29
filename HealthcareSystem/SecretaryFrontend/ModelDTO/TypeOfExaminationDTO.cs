using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjekatZdravoKorporacija.ModelDTO
{
    class TypeOfExaminationDTO
    {
        public string Type { get; set; }

        public TypeOfExaminationDTO(string type)
        {
            this.Type = type;
        }
    }
}
