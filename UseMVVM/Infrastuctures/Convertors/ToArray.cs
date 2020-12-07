using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Windows.Markup;

namespace UseMVVM.Infrastuctures.Convertors
{
    class ToArray : Base.MultiConvertor
    {
        public override object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            return values.ToList();
        }
    }
}
