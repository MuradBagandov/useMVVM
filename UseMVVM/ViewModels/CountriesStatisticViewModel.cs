using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using UseMVVM.Services;
using UseMVVM.Models;
using UseMVVM.Infrastuctures;
using UseMVVM.Infrastuctures.Commands;

namespace UseMVVM.ViewModels
{
    internal class CountriesStatisticViewModel : Base.ViewModel
    {
        
        private DataService _DataService;

        #region Properties
        public MainWindowViewModel MainModel { get; }

        #region Countries : IEnumerable<CountryInfo>
        private IEnumerable<CountryInfo> _Countries;

        public IEnumerable<CountryInfo> Countries
        {
            get => _Countries;
            set => Set(ref _Countries, value);
        } 
        #endregion

        #endregion


        #region Команды

        #region RefreshDataCommand
        public ICommand RefreshDataCommand { get; set; }

        private void OnRefreshDataCommandExecuted(object p)
        {
            Countries = _DataService.GetData(RecievedData.Confirmed);
        } 
        #endregion

        #endregion

        public CountriesStatisticViewModel(MainWindowViewModel main)
        {
            this.MainModel = main;
            _DataService = new DataService();

            #region Commands
            RefreshDataCommand = new LambdaCommand(OnRefreshDataCommandExecuted);
            #endregion
        }
    }
}
