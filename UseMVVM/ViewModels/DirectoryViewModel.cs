using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UseMVVM.ViewModels
{
    internal class DriverViewModel : Base.ViewModel
    {
        private readonly DriveInfo _driverInfo;

        public DriverViewModel(string path)
        {
            _driverInfo = new DriveInfo(path);
            
        }
    }

    internal class DirectoryViewModel : Base.ViewModel
    {
        private readonly DirectoryInfo _directoryInfo;

        public DirectoryViewModel(string path)
        {
            _directoryInfo = new DirectoryInfo(path);
            
        }

        public IEnumerable<DirectoryViewModel> Directories 
        {
            get
            {
                try
                {
                    return _directoryInfo.EnumerateDirectories()
                            .Select(d => new DirectoryViewModel(d.FullName));
                }
                catch
                {
                    return Enumerable.Empty<DirectoryViewModel>();
                }
                
            }
        } 

        

        public IEnumerable<FileViewModel> Files
        {
            get
            {
                try
                {
                    return _directoryInfo.EnumerateFiles().
                        Select(f => new FileViewModel(f.FullName));      
                }
                catch
                {
                    return Enumerable.Empty<FileViewModel>();
                }
            }
        }

        public IEnumerable<object> DirectoryItems => Directories.Cast<object>().Concat(Files);

        public string Name => _directoryInfo.Name;

        public string FullName => _directoryInfo.FullName;

        public DateTime CreationTime => _directoryInfo.CreationTime;

    }

    internal class FileViewModel: Base.ViewModel
    {
        private FileInfo _fileInfo;

        public FileViewModel(string path)
        {
            _fileInfo = new FileInfo(path);
        }

        public string Name => _fileInfo.Name;

        public string FullName => _fileInfo.FullName;

        public DateTime CreationTime => _fileInfo.CreationTime;

    }

}
