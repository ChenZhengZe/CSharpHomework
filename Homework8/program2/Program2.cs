using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Xml.Serialization;
using System.IO;
using System.Xml;
using System.Xml.Xsl;
using System.Xml.XPath;

namespace program2
{
    //订单类
    public class Order
    {
        public string OrderNumber { set; get; }     //订单的订单号
        public DateTime OrderTime { set; get; }     //订单的订购时间
        public string Client { set; get; }          //订单的客户名称
        public string ClientPhone { set; get; }     //客户的电话
        public string Creator { set; get; }         //订单的创建者
        public List<OrderDetails> OrderDetails { set; get; }    //订单明细

        public Order()
        {
            OrderDetails = new List<OrderDetails>();
        }

        public Order(string orderNumber, string client, string clientPhone, string creator, string goodsName,double goodsPrice, double goodsCounts)
        {
            string pattern1 = "" + DateTime.Now.Year + DateTime.Now.Month + DateTime.Now.Day + "[0-9]{3}";
            string pattern2 = "1[0-9]{10}";
            if (orderNumber != null && Regex.IsMatch(orderNumber, pattern1) && Regex.IsMatch(clientPhone, pattern2))
            {
                OrderDetails = new List<OrderDetails>();
                OrderNumber = orderNumber;
                Client = client;
                ClientPhone = clientPhone;
                Creator = creator;
                OrderTime = DateTime.Now;
                OrderDetails orderDetails0 = new OrderDetails(goodsName, goodsPrice, goodsCounts);
                OrderDetails.Add(orderDetails0);
            }
            else
            {
                if(!Regex.IsMatch(orderNumber, pattern1))
                {
                    throw new MyAppException("订单号格式不正确,请重新输入");
                }
                if (!Regex.IsMatch(clientPhone, pattern2))
                {
                    throw new MyAppException("客户电话格式不正确,请重新输入");
                }
            }
        }

        public override string ToString()
        {
            string result = OrderNumber + "\t" + OrderTime + "\t" + Client + "\t" + ClientPhone + "\t" + Creator + "\n" + "订单明细如下：";
            foreach (OrderDetails theOrderDetails in OrderDetails)                                                  
            {
                result += theOrderDetails.GoodsName + "\t" + theOrderDetails.GoodsPrice + "\t" + theOrderDetails.GoodsCounts + "\t" + theOrderDetails.TotalPrice + "              ";
            }
            return result;
        }
    }

    //订单明细类
    public class OrderDetails
    {
        
        public string GoodsName { set; get; }       //订购的商品名称
        public double GoodsPrice { set; get; }      //订购的商品单价
        public double GoodsCounts { set; get; }     //订购的商品数量
        public double TotalPrice { set; get; }
        //其它字段


        public OrderDetails()
        {
        }

        public OrderDetails(string goodsName, double goodsPrice, double goodsCounts)
        {
            GoodsName = goodsName;
            GoodsPrice = goodsPrice;
            GoodsCounts = goodsCounts;
            TotalPrice = goodsPrice * goodsCounts;
        }
    }

    //自定义异常类
    public class MyAppException : ApplicationException
    {
        public MyAppException(string message) : base(message)
        {

        }
    }

    //订单服务类   !!!!!!!!（采用单件模式）
    public class OrderService
    {
        private static OrderService uniqueInstance;     //订单服务类唯一的实例
        private OrderService()                          //构造函数定义为私有的
        {

        }
        public static OrderService GetInstance()       //获得实例的静态公有方法
        {
            if (uniqueInstance == null)
            {
                uniqueInstance = new OrderService();
            }
            return uniqueInstance;
        }

        public List<Order> orderList = new List<Order>();      //订单数据的列表

        public int GetOrderCounts()                      //获得订单的总数
        {
            return orderList.Count;
        }

        public void AddOrder(Order order)              //添加订单
        {
            bool isExist = false;
            if(GetOrderCounts() == 0)
            {
                orderList.Add(order);
            }
            else
            {
                foreach (Order one in orderList)
                {
                    if (one.ToString() == order.ToString())
                    {
                        isExist = true;
                    }
                }
                if (!isExist)
                {
                    orderList.Add(order);
                    Console.WriteLine("订单添加成功");
                }
            }    
        }

        public void DeleteOrder(Order order)           //删除订单
        {
            if (orderList.Contains(order))
            {
                orderList.Remove(order);
                Console.WriteLine("订单删除成功");
            }
            else
            {
                throw new MyAppException("该订单已经被删除，或从未添加到订单表中，无法删除");
            }
        }

        public void AlterOrderNumber(Order order, string orderNumber)        //修改订单号
        {
            if (orderList.Contains(order))
            {
                order.OrderNumber = orderNumber;
                order.OrderTime = DateTime.Now;
                Console.WriteLine("订单的订单号修改成功");
            }
            else
            {
                throw new MyAppException("该订单已经被删除，或从未添加到订单表中，无法修改订单号");
            }
        }

        public void AlterOrderClient(Order order, string client)              //修改客户名称
        {
            if (orderList.Contains(order))
            {
                order.Client = client;
                order.OrderTime = DateTime.Now;
                Console.WriteLine("订单的客户名称修改成功");
            }
            else
            {
                throw new MyAppException("该订单已经被删除，或从未添加到订单表中，无法修改客户名称");
            }
        }

        public void AlterOrderGoodsName(Order order, string goodsName, double goodsPrice)     //修改商品名称及商品单价
        {
            if (orderList.Contains(order))
            {
                order.OrderDetails[0].GoodsName = goodsName;
                order.OrderDetails[0].GoodsPrice = goodsPrice;
                order.OrderDetails[0].TotalPrice = goodsPrice * (order.OrderDetails[0].GoodsCounts);
                order.OrderTime = DateTime.Now;
                Console.WriteLine("订单的商品名称及商品单价修改成功");
            }
            else
            {
                throw new MyAppException("该订单已经被删除，或从未添加到订单表中，无法修改商品名称及商品单价");
            }
        }

        public void AlterOrderGoodsCounts(Order order, double goodsCounts)     //修改订购的商品数量
        {
            if (orderList.Contains(order))
            {
                order.OrderDetails[0].GoodsCounts = goodsCounts;
                order.OrderDetails[0].TotalPrice = goodsCounts * order.OrderDetails[0].GoodsPrice;
                order.OrderTime = DateTime.Now;
                Console.WriteLine("订单的商品数量修改成功");
            }
            else
            {
                throw new MyAppException("该订单已经被删除，或从未添加到订单表中，无法修改商品数量");
            }
        }

        public List<Order> SearchOrderByOrderNumber(string orderNumber)          //通过订单号查询订单
        {
            var query = orderList
                       .Where(s => s.OrderNumber == orderNumber);
            List<Order> theOrderList = query.ToList();
            if(theOrderList.Count != 0)
            {
                Console.WriteLine("查询成功");
            }
            return theOrderList;
        }

        public List<Order> SearchOrderByGoodsName(string goodsName)        //通过商品名称查询订单
        {
            var query = orderList
                       .Where(s => s.OrderDetails[0].GoodsName == goodsName);
            List<Order> theOrderList = query.ToList();
            if (theOrderList.Count != 0)
            {
                Console.WriteLine("查询成功");
            }
            return theOrderList;
        }

        public List<Order> SearchOrderByOrderClient(string client)        //通过客户名称查询订单
        {
            var query = orderList
                         .Where(s => s.Client == client);
            List<Order> theOrderList = query.ToList();
            if (theOrderList.Count != 0)
            {
                Console.WriteLine("查询成功");
            }
            return theOrderList;
        }

        public List<Order> SearchOrderByOrderTotalPriceABig(double money)        //查询订单金额大于某一数额的订单，并按金额升序排列
        {
            var query = orderList
                         .Where(s => s.OrderDetails[0].TotalPrice > money)
                         .OrderBy(s => s.OrderDetails[0].TotalPrice);
            List<Order> theOrderList = query.ToList();
            if (theOrderList.Count != 0)
            {
                Console.WriteLine("查询成功");
            }
            return theOrderList;
        }
        public List<Order> SearchOrderByOrderTotalPriceASmall(double money)        //查询订单金额小于某一数额的订单，并按金额升序排列
        {
            var query = orderList
                         .Where(s => s.OrderDetails[0].TotalPrice < money)
                         .OrderBy(s => s.OrderDetails[0].TotalPrice);
            List<Order> theOrderList = query.ToList();
            if (theOrderList.Count != 0)
            {
                Console.WriteLine("查询成功");
            }
            return theOrderList;
        }

        public List<Order> SearchOrderByOrderTotalPriceDBig(double money)        //查询订单金额大于某一数额的订单，并按金额降序排列
        {
            var query = orderList
                         .Where(s => s.OrderDetails[0].TotalPrice > money)
                         .OrderByDescending(s => s.OrderDetails[0].TotalPrice);
            List<Order> theOrderList = query.ToList();
            if (theOrderList.Count != 0)
            {
                Console.WriteLine("查询成功");
            }
            return theOrderList;
            
        }

        public List<Order> SearchOrderByOrderTotalPriceDSmall(double money)        //查询订单金额小于某一数额的订单，并按金额降序排列
        {
            var query = orderList
                         .Where(s => s.OrderDetails[0].TotalPrice < money)
                         .OrderByDescending(s => s.OrderDetails[0].TotalPrice);
            List<Order> theOrderList = query.ToList();
            if (theOrderList.Count != 0)
            {
                Console.WriteLine("查询成功");
            }
            return theOrderList;
        }

        public string Export()         
        {
            DateTime time = System.DateTime.Now;
            string fileName = "orders_" + time.Year + "_" + time.Month
                + "_" + time.Day + "_" + time.Hour + "_" + time.Minute
                + "_" + time.Second + ".xml";
            Export(fileName);
            return fileName;
        }
        //将所有订单序列化为XML文件
        public void Export(String fileName)          
        {
            XmlSerializer xs = new XmlSerializer(typeof(List<Order>));
            using (FileStream fs = new FileStream(fileName, FileMode.Create))
            {
                xs.Serialize(fs, orderList);
            }
        }
        //从XML文件中导入订单
        public void Import(string path)
        {
            if (Path.GetExtension(path) != ".xml")
            {
                throw new ArgumentException("It isn't a xml file!");
            }

            XmlSerializer xs = new XmlSerializer(typeof(List<Order>));
            List<Order> result = new List<Order>(orderList);
            using (FileStream fs = new FileStream(path, FileMode.Open))
            {
                List<Order> temp = (List<Order>)xs.Deserialize(fs);
                 foreach (Order one in temp)
                {
                    foreach (Order two in result)
                    {
                        if(one.ToString() != two.ToString())
                        {
                            AddOrder(one);
                        }
                    }
                } 
            }
        }
        //通过XSLT将XML文件导出为HTML文件
        public void ExportToHTML(string path, string fileName)           
        {
            try
            {
                XmlDocument doc = new XmlDocument();
                doc.Load(path);

                XPathNavigator nav = doc.CreateNavigator();
                nav.MoveToRoot();

                XslCompiledTransform xt = new XslCompiledTransform();
                xt.Load(@"..\..\Orders.xslt");

                FileStream outFileStream = File.OpenWrite(fileName);
                XmlTextWriter writer = new XmlTextWriter(outFileStream, System.Text.Encoding.UTF8);
                xt.Transform(nav, null, writer);
            }
            catch (XmlException e)
            {
                Console.WriteLine("XML Exception:" + e.ToString());
            }
            catch (XsltException e)
            {
                Console.WriteLine("XSLT Exception:" + e.ToString());
            }
        }
       
        public string MyToString(List<Order> someOrderList)
        {
            if (someOrderList.Count == 0)
            {
                return "查询无果";
            }
            string result = "";
            foreach (Order s in someOrderList)
            {
                result +=$"{s.ToString()}\n";
            }
            return result;
        }
    }

    class Program2
    {
        static void Main(string[] args)
        {
            OrderService orderService = OrderService.GetInstance();             //定义一个订单服务类的实例,该实例是唯一的
            try
            {
                Order order1 = new Order("20181113001", "陈1", "13949496191", "陈志鹏", "月饼", 2100, 100);
                orderService.AddOrder(order1);            //添加订单
                //在控制台输出订单 1 的所有信息
                Console.WriteLine(order1.ToString());   
                Console.WriteLine();

                Order order2 = new Order("20181113002", "陈2", "13949496192", "陈志鹏", "方便面", 1000, 1000);
                orderService.AddOrder(order2);
                Console.WriteLine("现在订单表中订单的个数为" + orderService.GetOrderCounts());
                Console.WriteLine();

                orderService.Export();     //将所有订单序列化为XML文件       
                orderService.ExportToHTML(orderService.Export(), @"..\..\Orders.html");     //通过XSLT将XML文件导出为HTML文件

                //Order order3 = new Order("20181113003", "陈3", "03949496193", "陈志鹏", "苹果", 1000, 2530);           //客户号码出现异常
                //orderService.AddOrder(order3);
                //Console.WriteLine("现在订单表中订单的个数为" + orderService.GetOrderCounts());
                //Console.WriteLine();

                //Order order4 = new Order("20181006004", "陈4", "13949496194", "陈志鹏", "草莓", 2045, 400);            //订单号出现出现异常
                //orderService.AddOrder(order4);
                //Console.WriteLine("现在订单表中订单的个数为" + orderService.GetOrderCounts());
                //Console.WriteLine();
            }
            catch (Exception e)
            {
                Console.WriteLine("出现了异常：{0}", e.Message);
            }
        }
    }
}
