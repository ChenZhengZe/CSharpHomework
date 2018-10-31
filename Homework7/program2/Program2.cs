using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace program2
{
    //订单类
    public class Order
    {
        public string OrderNumber { set; get; }     //订单的订单号
        //public DateTime OrderTime { set; get; }     //订单的订购时间
        public string Client { set; get; }          //订单的客户名称
        public string Creator { set; get; }         //订单的创建者
        public List<OrderDetails> OrderDetails { set; get; }    //订单明细


        public Order(string orderNumber, string client, string creator, string goodsName, double goodsPrice, double goodsCounts)
        {
            OrderDetails = new List<OrderDetails>();
            OrderNumber = orderNumber;
            Client = client;
            Creator = creator;
            //OrderTime = DateTime.Now;
            OrderDetails orderDetails0 = new OrderDetails(goodsName, goodsPrice, goodsCounts);
            OrderDetails.Add(orderDetails0);
        }

        public override string ToString()
        {
            string result = OrderNumber + "    " /*+ OrderTime + "    "*/ + Client + "    " + Creator + "    " + OrderDetails[0].GoodsName + "    " + OrderDetails[0].GoodsPrice + "    " + OrderDetails[0].GoodsCounts + "    " + OrderDetails[0].TotalPrice;
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
                else
                {
                    throw new MyAppException("该订单已经被添加到订单表中，请勿重复添加");
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
                //order.OrderTime = DateTime.Now;
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
                //order.OrderTime = DateTime.Now;
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
                //order.OrderTime = DateTime.Now;
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
                order.OrderDetails[0].TotalPrice = goodsCounts * (order.OrderDetails[0].GoodsPrice);
                //order.OrderTime = DateTime.Now;
                Console.WriteLine("订单的商品数量修改成功");
            }
            else
            {
                throw new MyAppException("该订单已经被删除，或从未添加到订单表中，无法修改商品数量");
            }
        }

        public string SearchOrderByOrderNumber(string orderNumber)          //通过订单号查询订单
        {
            var query = orderList
                       .Where(s => s.OrderNumber == orderNumber);
            foreach (var s in query)
            {
                Console.WriteLine("订单查询成功");
                return ($"{s.ToString()}");
            }
            return "查询失败";
        }

        public string SearchOrderByGoodsName(string goodsName)        //通过商品名称查询订单
        {
            var query = orderList
                       .Where(s => s.OrderDetails[0].GoodsName == goodsName);
            foreach (var s in query)
            {
                Console.WriteLine("订单查询成功");
                return ($"{s.ToString()}");
            }
            return "查询失败";
        }

        public string SearchOrderByOrderClient(string client)        //通过客户名称查询订单
        {
            var query = orderList
                         .Where(s => s.Client == client);
            foreach (var s in query)
            {
                Console.WriteLine("订单查询成功");
                return  ($"{s.ToString()}");
            }
            return "查询失败";
        }

        public void SearchOrderByOrderTotalPriceA(double money)        //查询订单金额大于某一数额的订单，并按金额升序排列
        {
            var query = orderList
                         .Where(s => s.OrderDetails[0].TotalPrice > money)
                         .OrderBy(s => s.OrderDetails[0].TotalPrice);
            foreach (var s in query)
            {
                Console.WriteLine($"{s.ToString()}");
            }
        }

        public void SearchOrderByOrderTotalPriceD(double money)        //查询订单金额大于某一数额的订单，并按金额降序排列
        {
            var query = orderList
                         .Where(s => s.OrderDetails[0].TotalPrice > money)
                         .OrderByDescending(s => s.OrderDetails[0].TotalPrice);
            foreach (var s in query)
            {
                Console.WriteLine($"{s.ToString()}");
            }
        }
    }

    class Program2
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

            OrderService orderService = OrderService.GetInstance();             //定义一个订单服务类的实例,该实例是唯一的

            orderService.AddOrder(order1);            //添加订单
            orderService.AddOrder(order2);
            orderService.AddOrder(order3);
            orderService.AddOrder(order4);

            Console.WriteLine(orderService.GetOrderCounts());             //输出现在订单表中订单的个数
            Console.WriteLine();

            orderService.AlterOrderNumber(order1, "2018100701");    //修改订单 1 的订单号 
            orderService.AlterOrderGoodsName(order1, "香肠", 10);   //修改订单 1 的商品名称及商品单价
            orderService.AlterOrderClient(order1, "陈一");      //修改订单 1 的客户名称
            orderService.AlterOrderGoodsCounts(order1, 20);      //修改订单 1 的订购的商品数量
            Console.WriteLine();

            //在控制台输出现在订单 1 的所有信息
            Console.WriteLine(order1.ToString());
            Console.WriteLine();

            //通过订单号查询订单信息,并在控制台输出所查询订单的所有信息
            Console.WriteLine(orderService.SearchOrderByOrderNumber("2018100603"));
            Console.WriteLine();

            //通过商品名称查询订单信息,并在控制台输出所查询订单的所有信息
            Console.WriteLine(orderService.SearchOrderByGoodsName("草莓"));
            Console.WriteLine();

            //通过客户名称查询订单信息,并在控制台输出所查询订单的所有信息
            Console.WriteLine(orderService.SearchOrderByOrderClient("陈2"));
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
        }
    }
}
