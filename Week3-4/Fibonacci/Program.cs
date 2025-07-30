using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fibonacci
{
    class Program
    {
        static int Fibonacci(int n)
        {
            int f1=1, f2=1, f3;
            for (int i = 3; i <= n; i++)
            {
                f3 = f1 + f2;
                f1 = f2;
                f2 = f3;
            }
            return n > 2 ? f2 : 1;
        }
        static void Main(string[] args)
        {
            int n;
            do
            {
                Console.WriteLine("Enter a number greater than 0: ");
                n = int.Parse(Console.ReadLine());
            } while (n < 1);
            Console.WriteLine(Fibonacci(n));
        }
    }
}
