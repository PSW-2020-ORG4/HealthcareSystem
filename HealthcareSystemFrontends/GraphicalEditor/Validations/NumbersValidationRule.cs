using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Controls;


namespace GraphicalEditor.Validations
{
    public class NumbersValidationRule : ValidationRule
    {
        private static readonly Regex _regexForNumbers = new Regex(@"^[0-9]+$");

        public int MinimumCharacters { get; set; }
        public int MaximumCharacters { get; set; }

        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            string charString = value as string;

            if (String.IsNullOrEmpty(charString) || String.IsNullOrWhiteSpace(charString))
            {
                if (MinimumCharacters == MaximumCharacters)
                {
                    return new ValidationResult(false, $"Ovo polje ne sme biti prazno. Potrebno je uneti " + MinimumCharacters + " cifara.");
                }
                else
                {
                    return new ValidationResult(false, $"Ovo polje ne sme biti prazno. Potrebno je uneti izmedju {MinimumCharacters} i {MaximumCharacters} cifara.");
                }
            }

            //Non digit input
            if (!_regexForNumbers.IsMatch(charString))
            {
                return new ValidationResult(false, $"Ovo polje podržava samo unos cifara.");
            }

            //Less characters than minimum
            else if (charString.Length < MinimumCharacters)
            {
                if (MinimumCharacters == MaximumCharacters)
                {
                    return new ValidationResult(false, $"Morate uneti {MinimumCharacters} cifara. (Nedostaje još " + (MinimumCharacters - charString.Length) + " cifara)");
                }

                return new ValidationResult(false, $"Morate uneti najmanje {MinimumCharacters} cifara. (Nedostaje još " + (MinimumCharacters - charString.Length) + " cifara)");
            }

            //More characters than maximum
            else if (charString.Length > MaximumCharacters)
            {
                return new ValidationResult(false, $"Maksimalan broj karaktera je {MaximumCharacters}. (Uneli ste " + (charString.Length - MaximumCharacters) + " karaktera viška)");
            }

            return new ValidationResult(true, null);
        }
    }
}
