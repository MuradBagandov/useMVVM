using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;
using System.Windows.Markup;
using MapControl;

namespace UseMVVM.Infrastuctures.Convertors
{
    [ValueConversion(typeof(Point), typeof(string))]
    [MarkupExtensionReturnType(typeof(LocationPointToString))]
    internal class PointToMapCoordinates : Base.Convertor
    {
        public override object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (!(value is Point point))
                throw new NotImplementedException();


            return new Location(point.X, point.Y);
        }

        public override object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (!(value is Location Location))
                throw new NotImplementedException();

            return new Point(Location.Latitude, Location.Longitude);
        }
    }
}
