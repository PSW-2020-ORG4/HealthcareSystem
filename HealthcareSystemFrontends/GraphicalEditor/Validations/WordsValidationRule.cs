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
    public class WordsValidationRule : ValidationRule
    {
        private static readonly Regex _regexForWord = new Regex(@"^[a-zA-ZŠšĐđČčĆćŽž]{2,}$");

        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            string charString = value as string;

            //Whitespaces
            if (charString.Trim().Equals(""))
            {
                return new ValidationResult(false, $"Izabrano polje ne sme biti prazno. Potrebno je uneti minimum 2 slova.");
            }
            //Only one letter
            else if (Regex.IsMatch(charString, @"^[a-zA-ZŠšĐđČčĆćŽž]{1}$"))
            {
                return new ValidationResult(false, $"Neispravan unos. Potrebno je uneti minimum 2 slova.");
            }
            //Invalid input
            else if (!_regexForWord.IsMatch(charString))
            {
                return new ValidationResult(false, $"Neispravan unos. Ovo polje prima samo slova!");
            }


            return new ValidationResult(true, null);
        }
    }
}
