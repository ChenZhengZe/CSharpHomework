using System;
using System.Collections;
using System.IO;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;

namespace program1
{
    
    class Crawler
    {
        private Hashtable urls = new Hashtable();
        private int count = 0;
  
        static void Main(string[] args)
        {
            Crawler myCrawler = new Crawler();

            string startUrl = "http://www.cnblogs.com/dstang2000/";
            if(args.Length >= 1)
            {
                startUrl = args[0];
            }

            myCrawler.urls.Add(startUrl, false);           //加入初始页面

            new Thread(myCrawler.Crawl).Start();           //开始爬行
        }

        private void Crawl()
        {
            Console.WriteLine("开始爬行了....");
            while(true)
            {
                string[] currents =new string[2] { null, null };
                int m = 0;
                foreach (string url in urls.Keys)
                {
                    m++;
                    if((bool)urls[url])
                    {
                        m--;
                        continue;                         //已经下载过的，不再下载
                    }
                    currents[(m-1)%2] = url;
                }
                if((currents[0] == null && currents[1] == null) || count > 10)
                {
                    break;
                }

                //string html = DownLoad(current);               //下载
                Task<string>[] tasks =
                {
                    Task.Run(()=>DownLoad(currents[0])),
                    Task.Run(()=>DownLoad(currents[1])),
                };
             
                for(int i = 0; i < tasks.Length; i++)
                {
                    Parse(tasks[i].Result);                                  //解析，并加入新的链接
                }
                Task.WaitAll(tasks);          //等待结束
            }
        }

        public string DownLoad(string url)
        {
            if (url == null)
            {
                return "";
            }
            Console.WriteLine("爬行" + url + "页面！");
            try
            {
                WebClient webClient = new WebClient();
                webClient.Encoding = Encoding.UTF8;
                string html = webClient.DownloadString(url);

                string fileName = count.ToString();
                File.WriteAllText(fileName, html, Encoding.UTF8);
                return html;
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
                return "";
            }
            finally
            {
                urls[url] = true;
                count++;
            }
        }

        public void Parse(string html)
        {
            string strRef = @"(href|HREF)[]*=[]*[""'][^""'#>]+[""']";
            MatchCollection matches = new Regex(strRef).Matches(html);
            foreach (Match match in matches)
            {
                strRef = match.Value.Substring(match.Value.IndexOf('=')+1).Trim('"','\"','#','>');
       
                if (strRef.Length == 0)
                {
                    continue;
                }

                if(urls[strRef] == null)
                {
                    urls[strRef] = false;
                }
            }
        }
    }
}
 