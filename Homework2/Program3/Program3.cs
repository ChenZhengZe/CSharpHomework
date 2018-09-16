using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Program3
{
    class Program3
    {
        static void Main(string[] args)
        {
            //“埃氏算法”求2到100以内的素数
            Console.Write(" 2到100以内的素数有 ");

            int[] anArray = new int[99];
            
            for(int j = 0; j  <anArray.Length; j++)
            {
                anArray[j] = j + 2;
                for (int m = 2; m <= 100; m++)
                {
                    if (anArray[j] >= m &&(anArray[j] % m) == 0 && (anArray[j] / m) != 1)
                    {
                        anArray[j] = 0;
                    }
                   
                }
                if (anArray[j] != 0)
                {
                    Console.Write(anArray[j] + "  ");
                }
            }
            Console.WriteLine();
        }
    }
}
