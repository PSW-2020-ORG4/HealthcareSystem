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
    class ValidationUserName : ValidationRule
    {

        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            try
            {
                var pom = value as string;

                Regex regularExpression = new Regex(@"^[a-zA-Z0-9\.\-_]{5,13}$");

                if (regularExpression.IsMatch(pom))
                {
                    return new ValidationResult(true, null);
                }

                return new ValidationResult(false, "Morate uneti najmanje 5, a maksimum 13 karaktera primer luka31");
            }
            catch
            {
                return new ValidationResult(false, "Greska");
            }
        }
    }

}
