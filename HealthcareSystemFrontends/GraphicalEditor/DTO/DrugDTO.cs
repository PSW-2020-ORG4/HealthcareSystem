/***********************************************************************
 * Module:  Drug.cs
 * Author:  Jelena Budisa
 * Purpose: Definition of the Class Drug
 ***********************************************************************/

using System;

namespace GraphicalEditor.Models.Drugs
{
    public class DrugDTO
    {
        public int DrugType_Id { get; set; }
        public DrugTypeDTO DrugTypeDTO { get; set; }
        public string Name { get; set; }
        public int Id { get; set; }
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
            this.Id = drug.Id;
            this.Quantity = drug.Quantity;
            this.ExpirationDate = drug.ExpirationDate;
            this.Producer = drug.Producer;
        }

    }
}