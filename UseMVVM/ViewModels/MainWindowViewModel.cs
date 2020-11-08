using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UseMVVM.ViewModels.Base;

namespace UseMVVM.ViewModels
{
    internal class MainWindowViewModel:ViewModel
    {
        private string _title = "MainTitle";

        public string Title
        {
            get => _title;
            set => Set(ref _title, value);
        }

    }
}
