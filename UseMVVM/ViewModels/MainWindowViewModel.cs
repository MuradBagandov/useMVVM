using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using UseMVVM.Infrastuctures.Commands;
using UseMVVM.Models.HighShool;
using UseMVVM.ViewModels.Base;

namespace UseMVVM.ViewModels
{
    internal class MainWindowViewModel:ViewModel
    {
        public ObservableCollection<Group> Groups { get; set; }


        #region TestList
        private List<string> _testList = new List<string> { "txt112", "txt12214" };

        public List<string> TestList
        {
            get => _testList;
            set => Set(ref _testList, value);
        } 
        #endregion

        #region Title
        private string _title = "MainTitle";

        public string Title
        {
            get => _title;
            set => Set(ref _title, value);
        }
        #endregion

        private Group _selectedGroup;

        public Group SelectedGroup
        {
            get => _selectedGroup;
            set => Set(ref _selectedGroup, value);
        }
       

        #region Commands

        

        #endregion

        public MainWindowViewModel()
        {
            int indexStudent = 1;
            var students = Enumerable.Range(1, 15).Select(s => new Student
            {
                Name = $"Name {indexStudent}",
                Surname = $"Surname {indexStudent}",
                Patronymic = $"Patronymic {++indexStudent}",
                Age = new Random(indexStudent).Next(10, 20),
                Birthday = DateTime.Today,
                LastRating = new ObservableCollection<int>(Enumerable.Range(1, 5).Select(x => new Random(x).Next(2, 5)))
            });
            var groups = Enumerable.Range(1, 30).Select(x => new Group 
            { 
                Name = $"Группа {x}", 
                Students = new ObservableCollection<Student>(students) 
            });
            Groups = new ObservableCollection<Group>(groups);
        }

    }
}
