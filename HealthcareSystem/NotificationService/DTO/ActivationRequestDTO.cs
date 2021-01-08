using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NotificationService
{
    public class ActivationRequestDTO
    {
        public string ActivationLink { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
    }
}
