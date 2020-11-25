using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UseMVVM.ViewModels
{
    internal class FileManagerViewModel:Base.ViewModel
    {
        #region Properties

        #region Drivers
        public IEnumerable<DriverViewModel> Drivers => DriveInfo.GetDrives()
            .Where(d => d.DriveType == DriveType.Fixed)
            .Select(d => new DriverViewModel(d));
        #endregion

        #region DirectoryRoot
        public DirectoryViewModel DirectoryRoot => new DirectoryViewModel("c:\\");
        #endregion

        #region SelectedDirectory
        private DirectoryViewModel _selectedDirectory;

        public DirectoryViewModel SelectedDirectory
        {
            get => _selectedDirectory;
            set => Set(ref _selectedDirectory, value);
        }
        #endregion
        
        #endregion

        public FileManagerViewModel()
        {

        }

    }
}
