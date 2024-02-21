using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MauiWeather.Converters
{
    class ShortDateConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            
            if (value is DateTime)
            {
                string d = ((DateTime)value).Day.ToString();
                string m = ((DateTime)value).Month.ToString();
                if (d.Length == 1) d = "0" + d;
                if (m.Length == 1) m = "0" + m;

                return d + "." + m + ".";
            }
            else
            {
                return value;
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
