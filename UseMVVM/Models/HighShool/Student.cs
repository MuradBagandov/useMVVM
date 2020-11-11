using System;
using System.Collections.Generic;

namespace UseMVVM.Models.HighShool
{
    internal class Student
    {
        public string Name { get; set; }

        public string Surname { get; set; }

        public string Patronymic { get; set; }

        public ICollection<int> LastRating { get; set; }

        public int Age { get; set; }

        public DateTime Birthday { get; set; }
    }

    internal class Group
    {
        public string Name { get; set; }

        public ICollection<Student> Students { get; set; }
    }
}