using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using System.Windows.Ink;

namespace ViewModelLib.Converters
{
    [ValueConversion(typeof(DrawingAttributes), typeof(DrawingAttributes))]
    public class ConvertDrawingAttributes:IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            if (value == null)
                return null;
            var current = new DrawingAttributes();
            current.Height = 3;
            current.Color = (value as DrawingAttributes).Color;

            return current;
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
