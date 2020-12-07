using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Markup;

namespace UseMVVM.Infrastuctures.Common
{
    [MarkupExtensionReturnType(typeof(int[]))]
    internal class StringToIntArray : MarkupExtension
    {
        [ConstructorArgument(nameof(Value))]
        public string Value { get; set; }

        public StringToIntArray() { }

        public StringToIntArray(string Value)=> this.Value = Value;

        public override object ProvideValue(IServiceProvider serviceProvider)
        {
            return Value.Split(';').Select(i => int.Parse(i)).ToArray();
        }

    }
}
