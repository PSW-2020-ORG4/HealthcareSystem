using Backend.Model.Enums;
using Model.Manager;
using System.ComponentModel.DataAnnotations.Schema;

namespace Backend.Model.Manager
{
    public class BaseRenovation
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public int RoomId { get; set; }
        [ForeignKey("RenovationPeriod")]
        public int RenovationPeriod_Id { get; set; }
        public virtual RenovationPeriod RenovationPeriod { get; set; }
        public string Description { get; set; }
        public TypeOfRenovation TypeOfRenovation { get; set; }

        public BaseRenovation()
        {
        }

        public BaseRenovation(int roomId, RenovationPeriod renovationPeriod, string description, TypeOfRenovation typeOfRenovation)
        {
            RoomId = roomId;
            RenovationPeriod = renovationPeriod;
            Description = description;
            TypeOfRenovation = typeOfRenovation;
        }
    }
}
