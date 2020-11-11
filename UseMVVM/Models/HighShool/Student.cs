using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UseMVVM.Models.HighShool
{


    internal class Student
    {
        public string Name { get; set; }

        public string Surname { get; set; }

        public string Patronymic { get; set; }

        public int Age { get; set; }

        public DateTime Birthday { get; set; }

        public ICollection<int> LastRating { get; set; }

    }

    internal class Group
    {
        public string Name { get; set; }

        public ICollection<Student> Students { get; set; }
    }
}
