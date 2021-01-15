using Backend.Model.Enums;
using Model.Manager;
using System.ComponentModel.DataAnnotations.Schema;

namespace Backend.Model.Manager
{
    public class MergeRenovation : BaseRenovation
    {

        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public int SecondRoomId { get; set; }

        public string NewRoomDescription { get; set; }
        public TypeOfUsage RoomType { get; set; }

        public MergeRenovation(): base()
        {
           
        }

        public MergeRenovation(RenovationPeriod renovationPeriod, string description, TypeOfRenovation typeOfRenovation,
            int firstRoomId, int secondRoomId, string newRoomDescription, TypeOfUsage roomType) 
            : base(firstRoomId,renovationPeriod,description,typeOfRenovation)
        {
            SecondRoomId = secondRoomId;
            NewRoomDescription = newRoomDescription;
            RoomType = roomType;
        }
    }
}
