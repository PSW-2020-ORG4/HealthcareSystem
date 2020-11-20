﻿using Model.Manager;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Backend.Service.DrugAndTherapy
{
    public interface IDrugTypeAndIngridientService
    {
        List<Ingredient> ViewIngridients();
        List<DrugType> ViewDrugTypes();
    }
}
