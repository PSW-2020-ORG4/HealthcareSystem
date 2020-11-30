using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfApp1
{
    class DrugDTO
    {
        public List<string> ingredient { get; set; }
        public string drugType { get; set; }
        public string Name { get; set; }
        public int Id { get; set; }
        public int Quantity { get; set; }
        public string ExpirationDate { get; set; }
        public string Producer { get; set; }

        public DrugDTO() { }

        public DrugDTO(DrugDTO d) {
            this.ingredient = d.ingredient;
            this.drugType = d.drugType;
            this.Name = d.Name;
            this.Id = d.Id;
            this.Quantity = d.Quantity;
            this.ExpirationDate = d.ExpirationDate;
            this.Producer = d.Producer;
        }
    }
}
