using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace GraphicalEditor.Converters
{
    class CapitalizeStringConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            String text = value.ToString();
            text = text.Replace("_", " ");

            Console.WriteLine(text.GetType() == typeof(String));

            if (String.IsNullOrEmpty(text))
            {
                return "";
            }

            text = text.First().ToString().ToUpper() + text.ToLower().Substring(1);
            Console.WriteLine(text);

            return text;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
