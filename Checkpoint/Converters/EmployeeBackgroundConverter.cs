using System;
using System.Globalization;
using System.Windows.Data;
using System.Windows.Media;

namespace Checkpoint.Converters
{
    public class EmployeeBackgroundConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            bool isCurrentEmployee = (bool)value;
            if (isCurrentEmployee)
            {
                // Цвет для текущего сотрудника
                return new SolidColorBrush(Colors.Red);
            }
            else
            {
                // Цвет для остальных сотрудников
                return new SolidColorBrush((Color)ColorConverter.ConvertFromString("#3AB19B"));
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
