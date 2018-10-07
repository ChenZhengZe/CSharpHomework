using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace program2
{
    //订单类
    public class Order
    {
        public string OrderNumber { set; get; }     //订单的订单号
        public DateTime OrderTime{set;get;}         //订单的订购时间
        public string Client { set; get; }          //订单的客户名称
        public string Creator { set; get; }         //订单的创建者
        public OrderDetails orderDetails;           //订单明细
        //其它字段

        public Order()                              //空白订单
        {
            orderDetails = new OrderDetails();
        }

        public Order(string orderNumber,string client,string creator, string goodsName, double goodsPrice, double goodsCounts)
        {
            OrderNumber = orderNumber;
            Client = client;
            Creator = creator;
            OrderTime = DateTime.Now;
            orderDetails = new OrderDetails(goodsName, goodsPrice, goodsCounts);
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
            totalPrice =  GoodsPrice * GoodsCounts;
            return totalPrice;
        }

        public OrderDetails()                        //空白的订单明细       
        {

        }

        public OrderDetails(string goodsName, double goodsPrice, double goodsCounts)
        {
            GoodsName = goodsName;
            GoodsPrice = goodsPrice;
            GoodsCounts = goodsCounts;
            totalPrice = GetTotalPrice();
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

        List<Order> orderList = new List<Order>();      //订单数据的列表

        public int GetOrderCounts()                      //获得订单的总数
        {
            return orderList.Count;
        }

        public void AddOrder(Order order)              //添加订单
        {
            if(!orderList.Contains(order))
            {
                orderList.Add(order);
                Console.WriteLine("订单添加成功");
            }
            else
            {
                throw new MyAppException("该订单已经被添加到订单表中，请勿重复添加");
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
                order.orderDetails.GoodsName = goodsName;
                order.orderDetails.GoodsPrice = goodsPrice;
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
                order.orderDetails.GoodsCounts = goodsCounts;
                order.OrderTime = DateTime.Now;
                Console.WriteLine("订单的商品数量修改成功");
            }
            else
            {
                throw new MyAppException("该订单已经被删除，或从未添加到订单表中，无法修改商品数量");
            }
        }

        public void SearchOrderByOrderNumber(string orderNumber,Order noneOrder)          //通过订单号查询订单
        {
            foreach(Order order in orderList)
            {
                if(order.OrderNumber == orderNumber)
                {
                    noneOrder.OrderNumber = order.OrderNumber;
                    noneOrder.OrderTime = order.OrderTime;
                    noneOrder.Client = order.Client;
                    noneOrder.Creator = order.Creator;
                    noneOrder.orderDetails.GoodsName = order.orderDetails.GoodsName;
                    noneOrder.orderDetails.GoodsPrice = order.orderDetails.GoodsPrice;
                    noneOrder.orderDetails.GoodsCounts = order.orderDetails.GoodsCounts;
                    Console.WriteLine("订单查询成功");
                    return;
                }
            }
            Console.WriteLine("订单查询失败");
            throw new MyAppException("该订单已经被删除，或从未添加到订单表中");
        }

        public void SearchOrderByGoodsName(string goodsName, Order noneOrder)        //通过商品名称查询订单
        {
            foreach (Order order in orderList)
            {
                if (order.orderDetails.GoodsName == goodsName)
                {
                    noneOrder.OrderNumber = order.OrderNumber;
                    noneOrder.OrderTime = order.OrderTime;
                    noneOrder.Client = order.Client;
                    noneOrder.Creator = order.Creator;
                    noneOrder.orderDetails.GoodsName = order.orderDetails.GoodsName;
                    noneOrder.orderDetails.GoodsPrice = order.orderDetails.GoodsPrice;
                    noneOrder.orderDetails.GoodsCounts = order.orderDetails.GoodsCounts;
                    Console.WriteLine("订单查询成功");
                    return;
                }  
            }
            Console.WriteLine("订单查询失败");
            throw new MyAppException("该订单已经被删除，或从未添加到订单表中");
        }

        public void SearchOrderByOrderClient(string client, Order noneOrder)        //通过客户名称查询订单
        {
            foreach (Order order in orderList)
            {
                if (order.Client.Equals(client))
                {
                    noneOrder.OrderNumber = order.OrderNumber;
                    noneOrder.OrderTime = order.OrderTime;
                    noneOrder.Client = order.Client;
                    noneOrder.Creator = order.Creator;
                    noneOrder.orderDetails.GoodsName = order.orderDetails.GoodsName;
                    noneOrder.orderDetails.GoodsPrice = order.orderDetails.GoodsPrice;
                    noneOrder.orderDetails.GoodsCounts = order.orderDetails.GoodsCounts;
                    Console.WriteLine("订单查询成功");
                    return;
                }
            }
            Console.WriteLine("订单查询失败");
            throw new MyAppException("该订单已经被删除，或从未添加到订单表中");
        }
    }

    class Program2
    {
        static void Main(string[] args)
        {
            Order order1 = new Order("2018100601", "陈1", "陈志鹏","月饼",21,10);
            //在控制台输出订单 1 的所有信息
            Console.WriteLine(order1.OrderNumber);
            Console.WriteLine(order1.OrderTime);
            Console.WriteLine(order1.Client);
            Console.WriteLine(order1.Creator);
            Console.WriteLine(order1.orderDetails.GoodsName);
            Console.WriteLine(order1.orderDetails.GoodsPrice);
            Console.WriteLine(order1.orderDetails.GoodsCounts);
            Console.WriteLine(order1.orderDetails.GetTotalPrice());
            Console.WriteLine();

            Order order2 = new Order("2018100602", "陈2", "陈志鹏", "方便面", 10, 10);
            Order order3 = new Order("2018100603", "陈3", "陈志鹏", "苹果", 10, 5.5);
            Order order4 = new Order("2018100604", "陈4", "陈志鹏", "草莓", 20.3, 3.2);
            Order noneOrder1 = new Order();
            Order noneOrder2 = new Order();
            Order noneOrder3 = new Order();
            Order noneOrder4 = new Order();

            OrderService orderService = OrderService.GetInstance();             //定义一个订单服务类的实例,该实例是唯一的

            orderService.AddOrder(order1);            //添加订单
            orderService.AddOrder(order2);
            orderService.AddOrder(order3);
            orderService.AddOrder(order4);

            Console.WriteLine(orderService.GetOrderCounts());             //输出现在订单表中订单的个数
            Console.WriteLine();

            orderService.AlterOrderNumber(order1, "2018100701");    //修改订单 1 的订单号 
            orderService.AlterOrderGoodsName(order1, "香肠",10);   //修改订单 1 的商品名称及商品单价
            orderService.AlterOrderClient(order1, "陈一");      //修改订单 1 的客户名称
            orderService.AlterOrderGoodsCounts(order1, 20);      //修改订单 1 的订购的商品数量
            Console.WriteLine();
      
            //在控制台输出现在订单 1 的所有信息
            Console.WriteLine(order1.OrderNumber);
            Console.WriteLine(order1.OrderTime);
            Console.WriteLine(order1.Client);
            Console.WriteLine(order1.Creator);
            Console.WriteLine(order1.orderDetails.GoodsName);
            Console.WriteLine(order1.orderDetails.GoodsPrice);
            Console.WriteLine(order1.orderDetails.GoodsCounts);
            Console.WriteLine(order1.orderDetails.GetTotalPrice());
            Console.WriteLine();

            orderService.SearchOrderByOrderNumber("2018100603", noneOrder1);  //通过订单号查询订单信息，并保存在一个空的订单中
            //在控制台输出所查询订单的所有信息
            Console.WriteLine(noneOrder1.OrderNumber);
            Console.WriteLine(noneOrder1.OrderTime);
            Console.WriteLine(noneOrder1.Client);
            Console.WriteLine(noneOrder1.Creator);
            Console.WriteLine(noneOrder1.orderDetails.GoodsName);
            Console.WriteLine(noneOrder1.orderDetails.GoodsPrice);
            Console.WriteLine(noneOrder1.orderDetails.GoodsCounts);
            Console.WriteLine(noneOrder1.orderDetails.GetTotalPrice());
            Console.WriteLine();

            orderService.SearchOrderByGoodsName("草莓", noneOrder2);  //通过商品名称查询订单信息，并保存在一个空的订单中
            //在控制台输出所查询订单的所有信息
            Console.WriteLine(noneOrder2.OrderNumber);
            Console.WriteLine(noneOrder2.OrderTime);
            Console.WriteLine(noneOrder2.Client);
            Console.WriteLine(noneOrder2.Creator);
            Console.WriteLine(noneOrder2.orderDetails.GoodsName);
            Console.WriteLine(noneOrder2.orderDetails.GoodsPrice);
            Console.WriteLine(noneOrder2.orderDetails.GoodsCounts);
            Console.WriteLine(noneOrder2.orderDetails.GetTotalPrice());
            Console.WriteLine();

            orderService.SearchOrderByOrderClient("陈2", noneOrder3);  //通过客户名称查询订单信息，并保存在一个空的订单中
            //在控制台输出所查询订单的所有信息
            Console.WriteLine(noneOrder3.OrderNumber);
            Console.WriteLine(noneOrder3.OrderTime);
            Console.WriteLine(noneOrder3.Client);
            Console.WriteLine(noneOrder3.Creator);
            Console.WriteLine(noneOrder3.orderDetails.GoodsName);
            Console.WriteLine(noneOrder3.orderDetails.GoodsPrice);
            Console.WriteLine(noneOrder3.orderDetails.GoodsCounts);
            Console.WriteLine(noneOrder3.orderDetails.GetTotalPrice());
            Console.WriteLine();


            orderService.DeleteOrder(order3);     //删除订单 3 
            Console.WriteLine(orderService.GetOrderCounts());        //输出现在订单表中订单的个数
            Console.WriteLine();

            orderService.DeleteOrder(order1);     //删除订单 1 
            Console.WriteLine(orderService.GetOrderCounts());        //输出现在订单表中订单的个数
            Console.WriteLine();

            //订单操作的异常检测
            //再次删除订单 3
            try
            {
                orderService.DeleteOrder(order3);     
            }
            catch(Exception e)
            {
                Console.WriteLine("出现了异常：{0}", e.Message);
            }
            Console.WriteLine();

            //修改订单 3 的订单号
            try
            {
                orderService.AlterOrderNumber(order3, "2018100703");    
            }
            catch (Exception e)
            {
                Console.WriteLine("出现了异常：{0}", e.Message);
            }
            Console.WriteLine();

            //修改订单 3 的商品数量
            try
            {
                orderService.AlterOrderGoodsCounts(order3, 1234567890);
            }
            catch (Exception e)
            {
                Console.WriteLine("出现了异常：{0}", e.Message);
            }
            Console.WriteLine();

            //通过订单号查询订单信息，并保存在一个空的订单中（所查询订单已经被删除）
            try
            {
                orderService.SearchOrderByOrderNumber("2018100603", noneOrder4);    
            }
            catch (Exception e)
            {
                Console.WriteLine("出现了异常：{0}", e.Message);
            }
            Console.WriteLine();

            //通过客户名称查询订单信息，并保存在一个空的订单中（所查询订单已经被删除）
            try
            {
                orderService.SearchOrderByOrderClient("陈3", noneOrder4);
            }
            catch (Exception e)
            {
                Console.WriteLine("出现了异常：{0}", e.Message);
            }
            Console.WriteLine();

            //再次添加订单 2 
            try
            {
                orderService.AddOrder(order2);         
            }
            catch (Exception e)
            {
                Console.WriteLine("出现了异常：{0}", e.Message);
            }
            Console.WriteLine();

            //添加订单 5，由于该订单不在订单表中，所以不会抛出异常
            try
            {
                Order order5 = new Order("2018100605", "陈5", "陈志鹏", "芒果", 10.6, 2.2);
                orderService.AddOrder(noneOrder2);                                           
            }
            catch (Exception e)
            {
                Console.WriteLine("出现了异常：{0}", e.Message);
            }
            Console.WriteLine(orderService.GetOrderCounts());        //输出现在订单表中订单的个数
            Console.WriteLine();

        }
    }
}
    

