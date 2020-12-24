/***********************************************************************
 * Module:  DrugType.cs
 * Author:  Dragana Carapic
 * Purpose: Definition of the Class Manager.DrugType
 ***********************************************************************/

using System;


namespace GraphicalEditor.Models.Drugs
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

        public DrugTypeDTO(int id,string type, string purpose)
        {
            this.Id = id;
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