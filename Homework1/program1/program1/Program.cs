using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace program1
{
    class Program
    {
        static void Main(string[] args)
        {
            string s = "";
            int n1 = 0;
            int n2 = 0;
            Console.Write("Please input the first number : ");
            s = Console.ReadLine();
            n1 = Int32.Parse(s);
            Console.Write("Please input the second number : ");
            s = Console.ReadLine();
            n2 = Int32.Parse(s);
            Console.WriteLine("You have entered : " + n1 + " and " + n2 + " , and " + n1 + " * " + n2 + " = " + (n1 * n2));
        }
    }
}
