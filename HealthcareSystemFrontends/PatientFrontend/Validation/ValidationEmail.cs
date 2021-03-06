﻿using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace ZdravoKorporacija.Validation
{
    class ValidationEmail : ValidationRule
    {
        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            try
            {
                var pom = value as string;

                Regex regularExpression = new Regex(@"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,4})+)$");

                if (regularExpression.IsMatch(pom))
                {
                    return new ValidationResult(true, null);
                }

                return new ValidationResult(false, "Morate uneti ispravno e-mail adresu npr. lukapetrovic@gmail.com");
            }
            catch
            {
                return new ValidationResult(false, "Greska");
            }
        }
    }
}
