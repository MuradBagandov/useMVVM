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
using UseMVVM.Services.Interfaces;

namespace UseMVVM.ViewModels
{
    internal class CountriesStatisticViewModel : Base.ViewModel
    {
        private IDataService _DataService;

        #region Properties
        public MainWindowViewModel MainModel { get; internal set; }

        #region Countries : IEnumerable<CountryInfo>
        private IEnumerable<CountryInfo> _Countries;

        public IEnumerable<CountryInfo> Countries
        {
            get => _Countries;
            set => Set(ref _Countries, value);
        }
        #endregion

        #region SelectedCountry : CountryInfo
        private CountryInfo _SelectedCountry;

        public CountryInfo SelectedCountry
        {
            get => _SelectedCountry;
            set => Set(ref _SelectedCountry, value);
        } 
        #endregion

        #endregion

        #region Commands

        #region RefreshDataCommand
        public ICommand RefreshDataCommand { get; set; }

        private void OnRefreshDataCommandExecuted(object p)
        {
            Countries = _DataService.GetData(RecievedData.Confirmed);
        }
        #endregion

        #endregion

        public CountriesStatisticViewModel(IDataService data_service)
        {
            _DataService = data_service;

            #region Commands
            RefreshDataCommand = new LambdaCommand(OnRefreshDataCommandExecuted);
            #endregion
        }
    }
}
