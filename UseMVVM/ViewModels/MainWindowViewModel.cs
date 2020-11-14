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

        #region Property

        #region TestList
        private List<object> _testList = new List<object> { "txt112", "txt12214" };

        public List<object> TestList
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

        #region SelectedGroup
        private Group _selectedGroup;

        public Group SelectedGroup
        {
            get => _selectedGroup;
            set => Set(ref _selectedGroup, value);
        }
        #endregion

        #endregion

        #region Commands

        #region AddGroupCommand
        public ICommand AddGroupCommand { get; set; }

        private bool CanAddGroupCommandExecute(object p) => true;

        private void OnAddGroupCommandExecuted(object p)
        {
            Groups.Add(new Group
            {
                Name = $"Group {Groups.Count + 1}",
                Students = new ObservableCollection<Student>()
            });
        } 
        #endregion

        #region RemoveGroupCommand
        public ICommand RemoveGroupCommand { get; set; }

        private bool CanRemoveGroupCommandExecute(object p) => p is Group group && Groups.Contains(group);

        private void OnRemoveGroupCommandExecuted(object p)
        {
            if (p is Group group)
            {
                var index = Groups.IndexOf(group);
                Groups.Remove(group);
                if (Groups.Count > 0)
                    SelectedGroup = index < Groups.Count ? Groups[index] : Groups[index - 1];
            }
                
        } 
        #endregion

        #endregion

        public MainWindowViewModel()
        {
            #region Commands
            RemoveGroupCommand = new LambdaCommand(OnRemoveGroupCommandExecuted, CanRemoveGroupCommandExecute);
            AddGroupCommand = new LambdaCommand(OnAddGroupCommandExecuted, CanAddGroupCommandExecute); 
            #endregion

            int indexStudent = 1;
            var students = Enumerable.Range(1, 10).Select(s => new Student
            {
                Name = $"Name {indexStudent}",
                Surname = $"Surname {indexStudent}",
                Patronymic = $"Patronymic {indexStudent}",
                Age = new Random(++indexStudent).Next(10, 20),
                Birthday = DateTime.Today,
                LastRating = new ObservableCollection<int>(Enumerable.Range(1, 5).Select(x => new Random(x).Next(2, 5)))
            });
            var groups = Enumerable.Range(1, 10).Select(x => new Group 
            { 
                Name = $"Группа {x}", 
                Students = new ObservableCollection<Student>(students) 
            });
            Groups = new ObservableCollection<Group>(groups);
            _testList.Add(Groups[0]);
            _testList.Add(new ObservableCollection<Student>(students)[0]);
        }

    }
}
