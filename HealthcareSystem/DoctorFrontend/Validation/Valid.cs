using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Globalization;
using System.Windows.Controls;
using System.Windows.Data;

namespace WpfApp1.Validation
{
    class Valid : ValidationRule
    {
        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            BindingExpression binExp = value as BindingExpression;
            MainWindow mw = binExp.DataItem as MainWindow;
            if (String.IsNullOrEmpty(mw.Username))
            {
                return new ValidationResult(false, "Morate uneti korisničko ime.");
            }
            else
            {
                return new ValidationResult(true, null);
            }
        }

       
    }

    class LogInPass : ValidationRule
    {
        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            BindingExpression binExp = value as BindingExpression;
            MainWindow mw = binExp.DataItem as MainWindow;
            if (String.IsNullOrEmpty(mw.Username))
            {
                return new ValidationResult(false, "Morate uneti lozinku.");
            }
            else
            {
                return new ValidationResult(true, null);
            }
        }


    }

    class Selection : ValidationRule
    {
        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            BindingExpression binExp = value as BindingExpression;
            MainWindow mw = binExp.DataItem as MainWindow;
            if (String.IsNullOrEmpty(mw.Username))
            {
                return new ValidationResult(false, "Morate selektovati red u tabeli.");
            }
            else
            {
                return new ValidationResult(true, null);
            }
        }


    }

    class Choice : ValidationRule
    {
        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            BindingExpression binExp = value as BindingExpression;
            MainWindow mw = binExp.DataItem as MainWindow;
            if (String.IsNullOrEmpty(mw.Username))
            {
                return new ValidationResult(false, "Morate izabrati pacijenta.");
            }
            else
            {
                return new ValidationResult(true, null);
            }
        }


    }
    class DatePic : ValidationRule
    {
        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            BindingExpression binExp = value as BindingExpression;
            MainWindow mw = binExp.DataItem as MainWindow;
            if (String.IsNullOrEmpty(mw.Username))
            {
                return new ValidationResult(false, "Morate izabrati datum.");
            }
            else
            {
                return new ValidationResult(true, null);
            }
        }


    }

    class Combo : ValidationRule
    {
        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            BindingExpression binExp = value as BindingExpression;
            MainWindow mw = binExp.DataItem as MainWindow;
            if (String.IsNullOrEmpty(mw.Username))
            {
                return new ValidationResult(false, "Morate izabrati prostoriju.");
            }
            else
            {
                return new ValidationResult(true, null);
            }
        }


    }

    class DatePicPeriod : ValidationRule
    {
        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            BindingExpression binExp = value as BindingExpression;
            MainWindow mw = binExp.DataItem as MainWindow;
            if (String.IsNullOrEmpty(mw.Username))
            {
                return new ValidationResult(false, "Krajnji datum je stariji od pocetnog.");
            }
            else
            {
                return new ValidationResult(true, null);
            }
        }


    }
}
