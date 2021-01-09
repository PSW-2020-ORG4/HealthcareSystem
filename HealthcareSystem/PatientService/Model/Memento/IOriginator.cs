﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PatientService.Model.Memento
{
    public interface IOriginator<T> where T : IMemento
    {
        T GetMemento();
    }
}
