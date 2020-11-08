using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using UseMVVM.Infrastuctures.Commands.Base;

namespace UseMVVM.Infrastuctures.Commands
{
    internal class CloseApplicationCommand:Command
    {
        public override bool CanExecute(object p) => true;

        public override void Execute(object p)
        {
            Application.Current.Shutdown();
        }

    }
}
