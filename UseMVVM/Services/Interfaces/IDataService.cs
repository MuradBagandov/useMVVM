using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UseMVVM.Models;

namespace UseMVVM.Services.Interfaces
{
    internal interface IDataService
    {
        IEnumerable<CountryInfo> GetData(RecievedData recieved);

    }
}
