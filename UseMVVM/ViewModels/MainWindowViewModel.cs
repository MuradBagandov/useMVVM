﻿using System;
using System.IO;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;
using System.Windows.Input;
using UseMVVM.Infrastuctures.Commands;
using UseMVVM.Models.HighShool;

namespace UseMVVM.ViewModels
{
    internal class MainWindowViewModel: Base.ViewModel
    {
        #region Properties
        public CountriesStatisticViewModel CountriesStatistic { get; }
        public ObservableCollection<Group> Groups { get; set; }

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
            set
            {
                if (Set(ref _selectedGroup, value))
                {
                    _selectedGroupStudent.Source = value?.Students;
                    OnPropertyChanged(nameof(SelectedGroupStudent));
                }
            }

        }
        #endregion

        #region свойство SelectedGroupStudent и метод фильтрации
        private CollectionViewSource _selectedGroupStudent = new CollectionViewSource();

        private void OnStudentFiltred(object sender, FilterEventArgs e)
        {
            if (!(e.Item is Student))
            {
                e.Accepted = false;
                return;
            }

            if (string.IsNullOrEmpty(_filterStudentText))
                return;

            Student student = (Student)e.Item;

            if (student.Name is null || student.Surname is null)
            {
                e.Accepted = false;
                return;
            }

            if (student.Name.Contains(_filterStudentText)) return;
            if (student.Surname.Contains(_filterStudentText)) return;
            if (student.Patronymic.Contains(_filterStudentText)) return;

            e.Accepted = false;

        }

        public ICollectionView SelectedGroupStudent => _selectedGroupStudent?.View; 
        #endregion

        #region FilterStudentText
        private string _filterStudentText;

        public string FilterStudentText
        {
            get => _filterStudentText;
            set 
            {
                if (Set(ref _filterStudentText, value))
                    _selectedGroupStudent.View.Refresh();
            }

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

        public MainWindowViewModel(CountriesStatisticViewModel statistic)
        {
            CountriesStatistic = statistic;
            CountriesStatistic.MainModel = this;

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

            _selectedGroupStudent.Filter += OnStudentFiltred;
            _selectedGroupStudent.SortDescriptions.Add(new SortDescription("Surname", ListSortDirection.Ascending));
        }  
    }
}
