using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace ZdravoKorporacija.Validation
{

    class ValidationJMBG : ValidationRule
    {
        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            try
            {
                var pom = value as string;

                Regex regularExpression = new Regex(@"^[0-9]{13}$");

                if (regularExpression.IsMatch(pom))
                {
                    return new ValidationResult(true, null);
                }

                return new ValidationResult(false, "Morate uneti 13 brojeva");
            }
            catch
            {
                return new ValidationResult(false, "Greska");
            }
        }
    }
}
