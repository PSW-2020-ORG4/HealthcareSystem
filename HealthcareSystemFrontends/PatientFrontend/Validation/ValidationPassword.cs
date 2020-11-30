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
    class ValidationPassword : ValidationRule
    {
        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            try
            {
                var pom = value as string;

                Regex regularExpression = new Regex(@"(?!^[0-9]*$)(?!^[a-zA-Z]*$)^([a-zA-Z0-9]{8,15})$");
            
                if(regularExpression.IsMatch(pom))
                {
                    return new ValidationResult(true, null);
                }

                return new ValidationResult(false, "Unesite najmanje 8 karaktera, jedan broj i bar jedno malo i veliko slovo");
            }
            catch
            {
                return new ValidationResult(false, "Greska");
            }
        }
    }
}
