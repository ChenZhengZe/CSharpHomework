using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Program2
{
    class Program2
    {
        static void Main(string[] args)
        {
            Console.Write(" 请输入数组的元素个数 : ");

            try
            { 
                int n = Int32.Parse(Console.ReadLine());

                int[] anArray = new int[n];
                double sum = 0;
  
                for (int i = 0; i < n; i++)
                {
                    Console.Write(" 请输入第{0}个元素: ", i + 1);
                    anArray[i] = int.Parse(Console.ReadLine());
                    sum += anArray[i];
                }

                int Max = anArray[0];
                int Min = anArray[0];
                for (int m = 1; m < anArray.Length; m++)
                {
                    Max = (Max <= anArray[m]) ? anArray[m] : Max;
                    Min = (Min >= anArray[m]) ? anArray[m] : Min;
                }
                Console.WriteLine(" 数组的最大值是 " + Max);
                Console.WriteLine(" 数组的最小值是 " + Min);

                int average = (int)(sum/n + 0.5);
                Console.WriteLine(" 数组的平均值四舍五入后为 " + average);
                Console.WriteLine(" 数组元素的和为 "+ sum);
            }
            catch (Exception e)
            {
                Console.WriteLine("input error." + e.Message);
            }
        }
    }
}
