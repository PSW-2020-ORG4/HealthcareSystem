using Backend.Model.Manager;
using GraphicalEditorServer.DTO;
using Model.Manager;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GraphicalEditorServer.Mappers
{
    public class BaseRenovationMapper
    {
        public static BaseRenovation BaseRenovationDTOToBaseRenovation(BaseRenovationDTO baseRenovationDTO)
        {
            BaseRenovation baseRenovation = new BaseRenovation();
            baseRenovation.RoomId = baseRenovationDTO.RoomId;
            baseRenovation.RenovationPeriod = new RenovationPeriod(baseRenovationDTO.StartTime, baseRenovationDTO.EndTime);
            baseRenovation.Description = baseRenovationDTO.Description;
            baseRenovation.TypeOfRenovation = baseRenovationDTO.TypeOfRenovation;
            return baseRenovation;
        }
    }
}
