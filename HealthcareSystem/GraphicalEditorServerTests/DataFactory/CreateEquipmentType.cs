﻿using Backend.Model.Manager;
using System;
using System.Collections.Generic;
using System.Text;

namespace GraphicalEditorServerTests.DataFactory
{
    public class CreateEquipmentType 
    {
        public EquipmentType CreateValidTestObject()
        {
            return new EquipmentType("table",true);
        }
    }
}