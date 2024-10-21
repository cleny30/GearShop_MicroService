
using System.Globalization;
using System.Windows.Data;

namespace DashboardAdmin.OrderManagement
{
    public class EndDateConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {

            if (value == null || (value is DateTime && ((DateTime)value) == DateTime.MinValue))
            {
                return "In Process";
            }
            return value;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
