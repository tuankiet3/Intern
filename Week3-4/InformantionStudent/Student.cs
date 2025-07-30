using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InformantionStudent
{
    class Student
    {
        public string Name { get; set; }
        public int Age { get; set; }
        public string Class { get; set; }

        public Student()
        {
        }

        public Student(string name, int age, string @class)
        {
            Name = name;
            Age = age;
            Class = @class;
        }

        public void Introduce()
        {
            Console.WriteLine($"Name: {Name}\nAge: {Age}\nClass: {Class}");
        }
    }
}
