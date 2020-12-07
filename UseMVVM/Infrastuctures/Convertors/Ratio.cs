using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using System.Windows.Markup;

namespace UseMVVM.Infrastuctures.Convertors
{
    [ValueConversion(typeof(double), typeof(double))]
    [MarkupExtensionReturnType(typeof(Ratio))]
    internal class Ratio : Base.Convertor
    {
        [ConstructorArgument(nameof(K))]
        public double K { get; set; } = 1;

        public Ratio() { }

        public Ratio(double K) => this.K = K;

        public override object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is null)
                throw new NotImplementedException();
            var v = System.Convert.ToDouble(value, culture);
            return v * K;
        }

        public override object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is null)
                throw new NotImplementedException();
            var v = System.Convert.ToDouble(value, culture);
            return v / K;
        }
    }
}
