using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Program1
{
    class Program1
    {
        static void Main(string[] args)
        {
            string s = "";
;           int n = 0;
            Console.Write(" Please input a positive int : ");

            try
            {
                s = Console.ReadLine();
                n = Int32.Parse(s);
                while (n <= 0)
                {
                    Console.Write(" Your input is not qualified, please enter again : ");
                    s = Console.ReadLine();
                    n = Int32.Parse(s);
                }
                if (n == 1)
                {
                    Console.WriteLine(" 1 is neither a prime number nor a composite number, and there are no prime factors. ");
                }
                else
                {
                    Console.Write(" All prime factors of " + n + " are ");
                    for (int i = 2; i <= n; i++)
                    {
                        if (0 == (n % i))
                        {
                            int counts = 0;
                            for (int j = 1; j <= i; j++)
                            {
                                if (i % j == 0)
                                {
                                    counts += 1;
                                }
                            }
                            if (counts == 2)
                            {
                                Console.Write("  " + i);
                            }
                        }
                    }
                    Console.WriteLine();
                }
            }
            catch(Exception e)
            {
                Console.WriteLine("input error." + e.Message);
            }
        }
    }
}
