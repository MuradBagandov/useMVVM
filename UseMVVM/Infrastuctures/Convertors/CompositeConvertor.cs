using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace UseMVVM.Infrastuctures.Convertors
{
    [ValueConversion(typeof(IValueConverter), typeof(IValueConverter))]
    class CompositeConvertor : Base.Convertor
    {
        public IValueConverter First { get; set; }

        public IValueConverter Second { get; set; }

        public override object Convert(object v, Type t, object p, CultureInfo c)
        {
            var result_1 = First?.Convert(v, t, p, c) ?? v;
            var result_2 = Second?.Convert(result_1, t, p, c) ?? result_1;
            return result_2;
        }

        public override object ConvertBack(object v, Type t, object p, CultureInfo c)
        {
            var result_1 = Second?.ConvertBack(v, t, p, c) ?? v;
            var result_2 = First?.ConvertBack(result_1, t, p, c) ?? result_1;
            return result_2;
        }
    }
}
