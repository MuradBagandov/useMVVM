using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace UseMVVM.Infrastuctures.Convertors
{
    class Ratio : IValueConverter
    {
        public double K { get; set; } = 1;

        public Ratio() { }

        public Ratio(double k) => K = k;

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is null)
                throw new NotImplementedException();
            var v = System.Convert.ToDouble(value, culture);
            return v * K;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is null)
                throw new NotImplementedException();
            var v = System.Convert.ToDouble(value, culture);
            return v / K;
        }
    }
}
