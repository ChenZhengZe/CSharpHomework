using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Xml;
using System.Xml.Serialization;

namespace program1
{
    //订单类
    [Serializable]
    public class Order
    {
        public string OrderNumber { set; get; }     //订单的订单号
        public DateTime OrderTime { set; get; }         //订单的订购时间
        public string Client { set; get; }          //订单的客户名称
        public string Creator { set; get; }         //订单的创建者
        public OrderDetails orderDetails;           //订单明细
        //其它字段

        public Order()
        {
            orderDetails = new OrderDetails();
        }        
        public Order(string orderNumber, string client, string creator, string goodsName, double goodsPrice, double goodsCounts)
        {
            OrderNumber = orderNumber;
            Client = client;
            Creator = creator;
            OrderTime = DateTime.Now;
            orderDetails = new OrderDetails(goodsName, goodsPrice, goodsCounts);
        }

        public override string ToString()
        {
            string result = OrderNumber + "\t" + OrderTime + "\t" + Client + "\t" + Creator + "\t" + orderDetails.GoodsName + "\t" + orderDetails.GoodsPrice + "\t" + orderDetails.GoodsCounts + "\t" + orderDetails.GetTotalPrice();
            return result;
        }

        public string GetOrderTime()
        {
            string result = "" + OrderTime;
            return result;
        }
    }

    //订单明细类
    public class OrderDetails
    {
        private double totalPrice;                  //订单的总价
        public string GoodsName { set; get; }       //订购的商品名称
        public double GoodsPrice { set; get; }      //订购的商品单价
        public double GoodsCounts { set; get; }     //订购的商品数量
        //其它字段

        public double GetTotalPrice()                //由商品的数量及单价计算出总价并返回值
        {
            totalPrice = GoodsPrice * GoodsCounts;
            return totalPrice;
        }

        public OrderDetails(string goodsName, double goodsPrice, double goodsCounts)
        {
            GoodsName = goodsName;
            GoodsPrice = goodsPrice;
            GoodsCounts = goodsCounts;
            totalPrice = GetTotalPrice();
        }

        public OrderDetails()
        {
        }

        public string GetGoodsPrice()
        {
            string result = "" + GoodsPrice;
            return result;
        }

        public string GetGoodsCounts()
        {
            string result = "" + GoodsCounts;
            return result;
        }
    }

    //自定义异常类
    public class MyAppException : ApplicationException
    {
        public MyAppException(string message) : base(message)
        {

        }
    }


    public class OrderService
    { 
        List<Order> orderList = new List<Order>();      //订单数据的列表

        public int GetOrderCounts()                      //获得订单的总数
        {
            return orderList.Count;
        }

        public bool AddOrder(Order order)              //添加订单
        {
            if (!orderList.Contains(order))
            {
                orderList.Add(order);
                Console.WriteLine("订单添加成功");
                return true;
            }
            else
            {
                throw new MyAppException("该订单已经被添加到订单表中，请勿重复添加");
            }
        }

        public bool DeleteOrder(Order order)           //删除订单
        {

            if (orderList.Contains(order))
            {
                orderList.Remove(order);
                Console.WriteLine("订单删除成功");
                return true;
            }
            else
            {
                throw new MyAppException("该订单已经被删除，或从未添加到订单表中，无法删除");
            }
        }

        public bool AlterOrderNumber(Order order, string orderNumber)        //修改订单号
        {
            if (orderList.Contains(order))
            {
                order.OrderNumber = orderNumber;
                order.OrderTime = DateTime.Now;
                Console.WriteLine("订单的订单号修改成功");
                return true;
            }
            else
            {
                throw new MyAppException("该订单已经被删除，或从未添加到订单表中，无法修改订单号");
            }
        }

        public bool AlterOrderClient(Order order, string client)              //修改客户名称
        {
            if (orderList.Contains(order))
            {
                order.Client = client;
                order.OrderTime = DateTime.Now;
                Console.WriteLine("订单的客户名称修改成功");
                return true;
            }
            else
            {
                throw new MyAppException("该订单已经被删除，或从未添加到订单表中，无法修改客户名称");
            }
        }

        public bool AlterOrderGoodsName(Order order, string goodsName, double goodsPrice)     //修改商品名称及商品单价
        {
            if (orderList.Contains(order))
            {
                order.orderDetails.GoodsName = goodsName;
                order.orderDetails.GoodsPrice = goodsPrice;
                order.OrderTime = DateTime.Now;
                Console.WriteLine("订单的商品名称及商品单价修改成功");
                return true;
            }
            else
            {
                throw new MyAppException("该订单已经被删除，或从未添加到订单表中，无法修改商品名称及商品单价");
            }
        }

        public bool AlterOrderGoodsCounts(Order order, double goodsCounts)     //修改订购的商品数量
        {
            if (orderList.Contains(order))
            {
                order.orderDetails.GoodsCounts = goodsCounts;
                order.OrderTime = DateTime.Now;
                Console.WriteLine("订单的商品数量修改成功");
                return true;
            }
            else
            {
                throw new MyAppException("该订单已经被删除，或从未添加到订单表中，无法修改商品数量");
            }
        }

        public bool SearchOrderByOrderNumber(string orderNumber)          //通过订单号查询订单
        {
            var query = orderList
                       .Where(s => s.OrderNumber == orderNumber);
            foreach (var s in query)
            {
                Console.WriteLine("订单查询成功");
                Console.WriteLine($"{s.ToString()}");
                return true;
            }
            Console.WriteLine("订单查询失败");
            return false;
        }

        public bool SearchOrderByGoodsName(string goodsName)        //通过商品名称查询订单
        {
            var query = orderList
                       .Where(s => s.orderDetails.GoodsName == goodsName);
            foreach (var s in query)
            {
                Console.WriteLine("订单查询成功");
                Console.WriteLine($"{s.ToString()}");
                return true;
            }
            Console.WriteLine("订单查询失败");
            return false;
        }

        public bool SearchOrderByOrderClient(string client)        //通过客户名称查询订单
        {
            var query = orderList
                         .Where(s => s.Client == client);
            foreach (var s in query)
            {
                Console.WriteLine("订单查询成功");
                Console.WriteLine($"{s.ToString()}");
                return true;
            }
            Console.WriteLine("订单查询失败");
            return false;
        }

        public bool SearchOrderByOrderTotalPriceA(double sumPrice)        //查询订单金额大于某一数额的订单，并按金额升序排列
        {
            int counts = 0;
            var query = orderList
                         .Where(s => s.orderDetails.GetTotalPrice() > sumPrice)
                         .OrderBy(s => s.orderDetails.GetTotalPrice());
            foreach (var s in query)
            {
                Console.WriteLine($"{s.ToString()}");
                ++counts;
            }
            if(counts == 0)
            {
                Console.WriteLine($"不存在订单总金额大于{sumPrice}的订单");
                return false;
            }
            else
            {
                return true;
            }
        }

        public bool SearchOrderByOrderTotalPriceD(double sumPrice)        //查询订单金额大于某一数额的订单，并按金额降序排列
        {
            int counts = 0;
            var query = orderList
                         .Where(s => s.orderDetails.GetTotalPrice() > sumPrice)
                         .OrderByDescending(s => s.orderDetails.GetTotalPrice());
            foreach (var s in query)
            {
                Console.WriteLine($"{s.ToString()}");
                ++counts;
            }
            if (counts == 0)
            {
                Console.WriteLine($"不存在订单总金额大于{sumPrice}的订单");
                return false;
            }
            else
            {
                return true;
            }
        }

        public bool Export()          //将所有订单序列化为XML文件，并在控制台输出
        {
                XmlSerializer xmlSerializer = new XmlSerializer(typeof(List<Order>));
                using (FileStream fs = new FileStream("Orders.xml", FileMode.Create))
                {
                    xmlSerializer.Serialize(fs, orderList);
                }

                Console.WriteLine(File.ReadAllText("Orders.xml"));        //在控制台输出XML文件
                return true;   
        }

        public bool Import(Order order)                 //从XML文件中载入订单
        {
            XmlDocument doc = new XmlDocument();
            doc.Load("Orders.xml");

            XmlNode orderNList = doc.SelectSingleNode("ArrayOfOrder");   //首先找到根节点
            XmlElement orderN = doc.CreateElement("Order");
            XmlElement orderNDetails = doc.CreateElement("orderDetails");

            XmlElement goodsNName = doc.CreateElement("GoodsName");
            goodsNName.InnerText = order.orderDetails.GoodsName;
            
            XmlElement goodsNPrice = doc.CreateElement("GoodsPrice");
            goodsNPrice.InnerText = order.orderDetails.GetGoodsPrice();
          
            XmlElement goodsNCounts = doc.CreateElement("GoodsCounts");
            goodsNCounts.InnerText = order.orderDetails.GetGoodsCounts();
      
            XmlElement orderNNumber = doc.CreateElement("OrderNumber");
            orderNNumber.InnerText = order.OrderNumber;
  
            XmlElement orderNTime = doc.CreateElement("OrderTime");
            orderNTime.InnerText = order.GetOrderTime();

            XmlElement orderNClient = doc.CreateElement("Client");
            orderNClient.InnerText = order.Client;

            XmlElement orderNCreator = doc.CreateElement("Creator");
            orderNCreator.InnerText = order.Creator;

            //添加订单明细（商品名称、商品单价、商品数量）订单明细节点
            orderNDetails.AppendChild(goodsNName);
            orderNDetails.AppendChild(goodsNPrice);
            orderNDetails.AppendChild(goodsNCounts);

            //添加订单信息（订单明细、订单号，客户名称、创建者）到订单节点
            orderN.AppendChild(orderNDetails);
            orderN.AppendChild(orderNNumber);
            orderN.AppendChild(orderNTime);
            orderN.AppendChild(orderNClient);
            orderN.AppendChild(orderNCreator);

            //将订单节点添加到根节点
            orderNList.AppendChild(orderN);

            //保存修改后的XML文件
            doc.Save("Orders.xml");
            Console.WriteLine("从XML文件中添加订单成功");

            //直接添加订单（不通过XML文件）到订单表中，保持数据同步(!!!!!!!!!!!!!!!!!!!!1)
            AddOrder(order);

            Console.WriteLine(File.ReadAllText("Orders.xml"));          //在控制台输出修改后的XML文件
            return true;
        }
    }

    class Program1
    {
        static void Main(string[] args)
        {
            Order order1 = new Order("2018100601", "陈1", "陈志鹏", "月饼", 2100, 100);
            //在控制台输出订单 1 的所有信息
            Console.WriteLine(order1.ToString());
            Console.WriteLine();

            Order order2 = new Order("2018100602", "陈2", "陈志鹏", "方便面", 1000, 1000);
            Order order3 = new Order("2018100603", "陈3", "陈志鹏", "苹果", 1000, 2530);
            Order order4 = new Order("2018100604", "陈4", "陈志鹏", "草莓", 2045, 400);

            OrderService orderService = new OrderService();

            orderService.Export();       //将所有订单（此时还未添加订单）序列化为XML文件，并在控制台输出
            Console.WriteLine();

            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine();

            orderService.Import(order1);            //从XML文件中载入订单1
            Console.WriteLine();

            orderService.AddOrder(order2);          //添加订单
            orderService.AddOrder(order3);
            orderService.AddOrder(order4);

            Console.WriteLine(orderService.GetOrderCounts());             //输出现在订单表中订单的个数
            Console.WriteLine();

            orderService.Export();       //将所有订单序列化为XML文件，并在控制台输出
            Console.WriteLine();

            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine();

            Order order5 = new Order("2018102205", "陈5", "陈志鹏", "iPhone", 3999, 400);    
            orderService.Import(order5);                 //从XML文件中载入订单5
            Console.WriteLine();
           
            orderService.AlterOrderNumber(order1, "2018100701");    //修改订单 1 的订单号 
            orderService.AlterOrderGoodsName(order1, "香肠", 10);   //修改订单 1 的商品名称及商品单价
            orderService.AlterOrderClient(order1, "陈一");      //修改订单 1 的客户名称
            orderService.AlterOrderGoodsCounts(order1, 20);      //修改订单 1 的订购的商品数量
            Console.WriteLine();

            //在控制台输出现在订单 1 的所有信息
            Console.WriteLine(order1.ToString());
            Console.WriteLine();

            orderService.SearchOrderByOrderNumber("2018100603");  //通过商品名称查询订单信息,并在控制台输出所查询订单的所有信息
            Console.WriteLine();

            orderService.SearchOrderByGoodsName("草莓");  //通过商品名称查询订单信息,并在控制台输出所查询订单的所有信息
            Console.WriteLine();

            orderService.SearchOrderByOrderClient("陈2"); //通过商品名称查询订单信息,并在控制台输出所查询订单的所有信息
            Console.WriteLine();

            Console.WriteLine("查询订单金额大于一万的订单,并按金额升序排列");
            orderService.SearchOrderByOrderTotalPriceA(10000);
            Console.WriteLine();

            Console.WriteLine("查询订单金额大于一万的订单,并按金额降序排列");
            orderService.SearchOrderByOrderTotalPriceD(10000);
            Console.WriteLine();

            orderService.DeleteOrder(order3);     //删除订单 3 
            Console.WriteLine(orderService.GetOrderCounts());        //输出现在订单表中订单的个数
            Console.WriteLine();

            orderService.DeleteOrder(order1);     //删除订单 1 
            Console.WriteLine(orderService.GetOrderCounts());        //输出现在订单表中订单的个数
            Console.WriteLine();

            Console.WriteLine(orderService.SearchOrderByOrderNumber("2018100603"));
            Console.WriteLine();
        }
    }
}

