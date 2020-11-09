using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UseMVVM.ViewModels
{
    class CalculateViewModel: Base.ViewModel
    {
        #region Value
        private string _value;

        public string Value
        {
            get => _value;
            set => Set(ref _value, value);
        }
        #endregion

        #region Result
        private double _result = 0;

        public double Result
        {
            get => _result;
            set => Set(ref _result, value);
        } 
        #endregion
    }
}
