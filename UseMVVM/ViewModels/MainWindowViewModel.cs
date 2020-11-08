using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using UseMVVM.Infrastuctures.Commands;
using UseMVVM.ViewModels.Base;

namespace UseMVVM.ViewModels
{
    internal class MainWindowViewModel:ViewModel
    {

        #region Title
        private string _title = "MainTitle";

        public string Title
        {
            get => _title;
            set => Set(ref _title, value);
        }
        #endregion

        #region Commands

        public ICommand CloseApplicationCommand { get;  }

        public bool CanCloseApplicationCommandExecute(object p) => true;

        public void OnCloseApplicationCommandExecute(object p)
        {
            Application.Current.Shutdown();
        }

        #endregion

        public MainWindowViewModel()
        {
            CloseApplicationCommand = new LambdaCommand(OnCloseApplicationCommandExecute, CanCloseApplicationCommandExecute);
        }

    }
}
