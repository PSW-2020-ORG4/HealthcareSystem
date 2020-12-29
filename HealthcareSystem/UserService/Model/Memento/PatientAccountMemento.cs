﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace UserService.Model.Memento
{
    public class PatientAccountMemento : UserAccountMemento
    {
        public DateTime DateOfRegistration { get; set; }
        public bool IsActivated { get; set; }
        public bool IsBlocked { get; set; }
        public string ImageName { get; set; }
        public IEnumerable<MaliciousActionMemento> MaliciousActions { get; set; }
    }
}
