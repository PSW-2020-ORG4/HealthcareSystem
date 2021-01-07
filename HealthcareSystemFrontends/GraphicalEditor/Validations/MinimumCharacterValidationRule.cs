using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace GraphicalEditor.Validations
{
    public class MinimumCharacterValidationRule : ValidationRule
    {
        public int MinimumCharacters { get; set; }

        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            string charString = value as string;

            if (charString.Trim().Equals(""))
            {
                return new ValidationResult(false, $"Izabrano polje ne sme biti prazno. Potrebno je uneti minimum {MinimumCharacters} karaktera.");
            }
            else if (charString.Length < MinimumCharacters)
            {
                return new ValidationResult(false, $"Morate uneti najmanje {MinimumCharacters} karaktera. (Nedostaje još " + (MinimumCharacters - charString.Length) + " karaktera)");
            }

            return new ValidationResult(true, null);
        }
    }
}
