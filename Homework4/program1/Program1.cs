using System;
using System.Threading;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace program1
{
    //1. 声明参数类型
    public class AlarmEventArgs : EventArgs
    {
        public TimeSpan AlarmTimeSpan { set; get; }     //闹铃时间间隔
        public DateTime AlarmTime { set; get; }         //闹铃时间
    }

    //2. 声明委托类型
    public delegate void AlarmEventHandler(object sender, AlarmEventArgs e);

    //定义事件源(闹钟)
    public class AlarmClock
    {
        //3. 声明事件
        public event AlarmEventHandler Alarm;

        public void DoAlarm()
        {
            AlarmEventArgs args = new AlarmEventArgs();
            Console.WriteLine("如果您想设定闹铃时间间隔，请按 A");
            Console.WriteLine("如果您想设定闹铃时间，请按 B");
            try
            {
                  ConsoleKeyInfo keyinfo = Console.ReadKey();
                  if (keyinfo.Key == ConsoleKey.A)
                  {
                       Console.WriteLine();
                       Console.WriteLine("请按照如下格式输入闹铃的时间间隔");
                       Console.WriteLine("XX.XX:XX:XX");
                       Console.WriteLine("第1，2个 X 代表天数，第3，4个 X 代表小时数，第5，6个 X 代表分钟数，第7，8个 X 代表秒数");
                       args.AlarmTimeSpan = TimeSpan.Parse(Console.ReadLine());
                       Console.WriteLine("敲回车开始计时");
                       Console.ReadKey();
                       Console.WriteLine("计时开始");
                       Thread.Sleep(args.AlarmTimeSpan);
                       Alarm(this, args);
                  }
                  if (keyinfo.Key == ConsoleKey.B)
                  {
                       Console.WriteLine();
                       Console.WriteLine("请按照如下格式输入闹铃的时间");
                       Console.WriteLine("X/X/X  X:X:X");
                       Console.WriteLine("第 1 个 X 代表年，第 2 个 X 代表月，第 3 个 X 代表日，第 4 个 X 代表时，第 5 个 X 代表分，第 6 个 X 代表秒");
                       args.AlarmTime = DateTime.Parse(Console.ReadLine());
                       Console.WriteLine("敲回车开始计时");
                       Console.ReadKey();
                       Console.WriteLine("计时开始");
                       args.AlarmTimeSpan = args.AlarmTime - DateTime.Now;
                       Thread.Sleep(args.AlarmTimeSpan);
                       Alarm(this, args);
                  }       
            }
            catch (Exception e)
            {
                Console.WriteLine("input error." + e.Message);
            }
           
        }
    }
    class Program1
    {
        static void Main(string[] args)
        {
            var alarmClock = new AlarmClock();

            //5. 注册事件
            alarmClock.Alarm += ShowTimeArrival;
            alarmClock.DoAlarm();
        }

        //6. 事件处理方法
        static void ShowTimeArrival(object sender, AlarmEventArgs e)
        {
            Console.WriteLine("Time is up, come on, you should do what you should do!");
        }
       
    }
}
