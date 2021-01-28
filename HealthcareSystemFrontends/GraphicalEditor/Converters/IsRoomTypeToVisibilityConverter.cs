using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;
using GraphicalEditor.Enumerations;

namespace GraphicalEditor.Converters
{
    class IsRoomTypeToVisibilityConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            TypeOfMapObject typeOfSelectedObject = (TypeOfMapObject)value;

            if (CheckIsRoomType(typeOfSelectedObject))
                return Visibility.Visible;
            else
                return Visibility.Collapsed;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }

        private Boolean CheckIsRoomType(TypeOfMapObject typeOfMapObject)
           => typeOfMapObject == TypeOfMapObject.EXAMINATION_ROOM
               || typeOfMapObject == TypeOfMapObject.HOSPITALIZATION_ROOM
               || typeOfMapObject == TypeOfMapObject.OPERATION_ROOM;
    }
}
