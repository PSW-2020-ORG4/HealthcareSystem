using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GraphicalEditor.DTO
{
    public class DrugDTO
    {
        public string Name { get; set; }
        public DrugTypeDTO DrugTypeDTO { get; set; }
        public int Quantity { get; set; }
        public DateTime ExpirationDate { get; set; }
        public string Producer { get; set; }

        public DrugDTO() { }

        public DrugDTO(string name, int quantity, DateTime expirationDate, string producer, DrugTypeDTO drugTypeDTO)
        {
            this.Name = name;
            this.Quantity = quantity;
            this.ExpirationDate = expirationDate;
            this.Producer = producer;
            this.DrugTypeDTO = drugTypeDTO;
        }        
    }
}