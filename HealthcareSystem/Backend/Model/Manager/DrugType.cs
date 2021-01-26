/***********************************************************************
 * Module:  DrugType.cs
 * Author:  Dragana Carapic
 * Purpose: Definition of the Class Manager.DrugType
 ***********************************************************************/

using System.ComponentModel.DataAnnotations.Schema;

namespace Model.Manager
{
    public class DrugType
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string Type { get; set; }
        public string Purpose { get; set; }

        public DrugType() { }

        public DrugType(string type, string purpose)
        {

            this.Type = type;
            this.Purpose = purpose;
        }

        public DrugType(DrugType drugType)
        {

            this.Type = drugType.Type;
            this.Purpose = drugType.Purpose;
        }

    }
}