using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace UseMVVM.Infrastuctures.Convertors
{
    [ValueConversion(typeof(double), typeof(double))]
    internal class Linear : Base.Convertor
    {
        public double K { get; set; } = 1;
        public double B { get; set; }

        public Linear() { }

        public Linear(double k, double b)
        {
            K = k;
            B = b;
        }

        public override object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is null)
                throw new NotImplementedException();
            var x = System.Convert.ToDouble(value, culture);

            return K * x + B;
        }

        public override object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is null)
                throw new NotImplementedException();
            var x = System.Convert.ToDouble(value, culture);

            return (x-B) / B;
        }
    }
}
