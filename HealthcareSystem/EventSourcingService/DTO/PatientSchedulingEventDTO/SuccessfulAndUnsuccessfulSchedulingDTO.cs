using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EventSourcingService.DTO
{
    public class SuccessfulAndUnsuccessfulSchedulingDTO
    {
        public int NumberOfSuccessfulScheduling { get; set; }
        public int NumberOfUnsuccessfulScheduling { get; set; }
        public int NumberOfScheduling { get; set; }
    }
}
