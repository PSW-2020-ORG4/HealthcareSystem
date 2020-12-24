using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GraphicalEditorServer.DTO
{
    public class DrugDTO
    {
        public string Name { get; set; }
        public DrugTypeDTO DrugTypeDTO { get; set; }
        public int Quantity { get; set; }
        public DateTime ExpirationDate { get; set; }
        public string Producer { get; set; }

        public DrugDTO() { }

        public DrugDTO(DrugTypeDTO drugTypeDTO, string name, int quantity, DateTime expirationDate, string producer)
        {
            this.DrugTypeDTO = drugTypeDTO;
            this.Name = name;
            this.Quantity = quantity;
            this.ExpirationDate = expirationDate;
            this.Producer = producer;
        }


        public DrugDTO(DrugDTO drug)
        {
            this.DrugTypeDTO = drug.DrugTypeDTO;
            this.Name = drug.Name;
            this.Quantity = drug.Quantity;
            this.ExpirationDate = drug.ExpirationDate;
            this.Producer = drug.Producer;
        }
    }
}
