using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InformantionStudent
{
    class Program
    {
        static void Main(string[] args)
        {
            Student student = new Student("Kiet", 22, "TPM-01");
            student.Introduce();
        }
    }
}
