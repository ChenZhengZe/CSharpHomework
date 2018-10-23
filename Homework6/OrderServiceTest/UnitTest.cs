using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using program1;
using System.Collections.Generic;
using System.IO;


namespace OrderServiceTest
{
    [TestClass]
    public class OrderServiceTest
    {
        [TestMethod]
        public void GetOrderCountsTest1()
        {
            OrderService orderService = new OrderService();
            Assert.AreEqual(orderService.GetOrderCounts(), 0);       //未添加订单，订单数应为0，测试成功
        }

        [TestMethod]
        public void GetOrderCountsTest2()
        {
            Order order1 = new Order("2018100601", "陈1", "陈志鹏", "月饼", 2100, 100);
            Order order2 = new Order("2018100602", "陈2", "陈志鹏", "方便面", 1000, 1000);
            OrderService orderService = new OrderService();
            orderService.AddOrder(order1);
            orderService.AddOrder(order2);

            Assert.AreEqual(orderService.GetOrderCounts(), 1);   //添加了2个订单，订单数应为2，测试失败
        }

        [TestMethod]
        public void AddOrderTest1()
        {
            Order order1 = new Order("2018100601", "陈1", "陈志鹏", "月饼", 2100, 100);
            OrderService orderService = new OrderService();

            Assert.IsTrue(orderService.AddOrder(order1));       //订单添加成功，测试成功
        }

        [TestMethod]
        public void AddOrderTest2()
        {
            Order order1 = new Order("2018100601", "陈1", "陈志鹏", "月饼", 2100, 100);
            OrderService orderService = new OrderService();

            Assert.IsTrue(orderService.AddOrder(order1));     //第一次添加订单1
            Assert.IsTrue(orderService.AddOrder(order1));     //重复添加订单1，测试失败
        }

        [TestMethod]
        public void DeleteOrderTest1()
        {
            Order order1 = new Order("2018100601", "陈1", "陈志鹏", "月饼", 2100, 100);
            Order order2 = new Order("2018100602", "陈2", "陈志鹏", "方便面", 1000, 1000);
            OrderService orderService = new OrderService();
            orderService.AddOrder(order1);
            orderService.AddOrder(order2);

            Assert.IsTrue(orderService.DeleteOrder(order1));       //订单1删除成功，测试成功
        }

        [TestMethod]
        public void DeleteOrderTest2()
        {
            Order order1 = new Order("2018100601", "陈1", "陈志鹏", "月饼", 2100, 100);
            Order order2 = new Order("2018100602", "陈2", "陈志鹏", "方便面", 1000, 1000);
            OrderService orderService = new OrderService();
            orderService.AddOrder(order1);
            orderService.AddOrder(order2);

            Assert.IsTrue(orderService.DeleteOrder(order1));     //第一次删除订单1
            Assert.IsTrue(orderService.DeleteOrder(order1));     //重复删除订单1，测试失败
        }

        [TestMethod]
        public void DeleteOrderTest3()
        {
            Order order1 = new Order("2018100601", "陈1", "陈志鹏", "月饼", 2100, 100);
            Order order2 = new Order("2018100602", "陈2", "陈志鹏", "方便面", 1000, 1000);
            OrderService orderService = new OrderService();
            orderService.AddOrder(order1);
            orderService.AddOrder(order2);

            Assert.IsTrue(orderService.DeleteOrder(order1));     //第一次删除订单1
            Assert.IsTrue(orderService.DeleteOrder(order1));     //第一次删除订单2，但订单2未添加，测试失败
        }

        [TestMethod]
        public void AlterOrderNumberTest1()
        {
            Order order1 = new Order("2018100601", "陈1", "陈志鹏", "月饼", 2100, 100);
            Order order2 = new Order("2018100602", "陈2", "陈志鹏", "方便面", 1000, 1000);
            OrderService orderService = new OrderService();
            orderService.AddOrder(order1);
            orderService.AddOrder(order2);

            Assert.IsTrue(orderService.AlterOrderNumber(order1, "2018100701"));     //修改订单1的订单号，测试成功
        }

        [TestMethod]
        public void AlterOrderNumberTest2()
        {
            Order order1 = new Order("2018100601", "陈1", "陈志鹏", "月饼", 2100, 100);
            Order order2 = new Order("2018100602", "陈2", "陈志鹏", "方便面", 1000, 1000);
            OrderService orderService = new OrderService();
            orderService.AddOrder(order1);


            Assert.IsTrue(orderService.AlterOrderNumber(order2, "2018100702"));     //修改订单2的订单号，但订单2未添加，测试失败
        }

        [TestMethod]
        public void AlterOrderClientTest1()
        {
            Order order1 = new Order("2018100601", "陈1", "陈志鹏", "月饼", 2100, 100);
            Order order2 = new Order("2018100602", "陈2", "陈志鹏", "方便面", 1000, 1000);
            OrderService orderService = new OrderService();
            orderService.AddOrder(order1);
            orderService.AddOrder(order2);

            Assert.IsTrue(orderService.AlterOrderClient(order1, "陈一"));     //修改订单1的客户名称，测试成功
        }

        [TestMethod]
        public void AlterOrderClientTest2()
        {
            Order order1 = new Order("2018100601", "陈1", "陈志鹏", "月饼", 2100, 100);
            Order order2 = new Order("2018100602", "陈2", "陈志鹏", "方便面", 1000, 1000);
            OrderService orderService = new OrderService();
            orderService.AddOrder(order1);


            Assert.IsTrue(orderService.AlterOrderClient(order2, "陈二"));     //修改订单2的客户名称，但订单2未添加，测试失败
        }

        [TestMethod]
        public void AlterOrderGoodsNameTest1()
        {
            Order order1 = new Order("2018100601", "陈1", "陈志鹏", "月饼", 2100, 100);
            Order order2 = new Order("2018100602", "陈2", "陈志鹏", "方便面", 1000, 1000);
            OrderService orderService = new OrderService();
            orderService.AddOrder(order1);
            orderService.AddOrder(order2);

            Assert.IsTrue(orderService.AlterOrderGoodsName(order1, "iPhone", 7900));     //修改订单1的商品名称，测试成功
        }

        [TestMethod]
        public void AlterOrderGoodsNameTest2()
        {
            Order order1 = new Order("2018100601", "陈1", "陈志鹏", "月饼", 2100, 100);
            Order order2 = new Order("2018100602", "陈2", "陈志鹏", "方便面", 1000, 1000);
            OrderService orderService = new OrderService();
            orderService.AddOrder(order1);

            Assert.IsTrue(orderService.AlterOrderGoodsName(order2, "小米", 1988));     //修改订单2的商品名称，但订单2未添加，测试失败
        }

        [TestMethod]
        public void AlterOrderGoodsCountsTest1()
        {
            Order order1 = new Order("2018100601", "陈1", "陈志鹏", "月饼", 2100, 100);
            Order order2 = new Order("2018100602", "陈2", "陈志鹏", "方便面", 1000, 1000);
            OrderService orderService = new OrderService();
            orderService.AddOrder(order1);
            orderService.AddOrder(order2);

            Assert.IsTrue(orderService.AlterOrderGoodsCounts(order1, 790));     //修改订单1的商品数量，测试成功
        }

        [TestMethod]
        public void AlterOrderGoodsCountsTest2()
        {
            Order order1 = new Order("2018100601", "陈1", "陈志鹏", "月饼", 2100, 100);
            Order order2 = new Order("2018100602", "陈2", "陈志鹏", "方便面", 1000, 1000);
            OrderService orderService = new OrderService();
            orderService.AddOrder(order1);

            Assert.IsTrue(orderService.AlterOrderGoodsCounts(order2, 198));     //修改订单2的商品数量，但订单2未添加，测试失败
        }

        [TestMethod]
        public void SearchOrderByOrderNumberTest1()
        {
            Order order1 = new Order("2018100601", "陈1", "陈志鹏", "月饼", 2100, 100);
            Order order2 = new Order("2018100602", "陈2", "陈志鹏", "方便面", 1000, 1000);
            OrderService orderService = new OrderService();
            orderService.AddOrder(order1);
            orderService.AddOrder(order2);

            Assert.IsTrue(orderService.SearchOrderByOrderNumber("2018100601"));   //查询订单号是“2018100601”的订单，测试成功
        }

        [TestMethod]
        public void SearchOrderByOrderNumberTest2()
        {
            Order order1 = new Order("2018100601", "陈1", "陈志鹏", "月饼", 2100, 100);
            Order order2 = new Order("2018100602", "陈2", "陈志鹏", "方便面", 1000, 1000);
            OrderService orderService = new OrderService();
            orderService.AddOrder(order1);
            orderService.AddOrder(order2);

            Assert.IsTrue(orderService.SearchOrderByOrderNumber("2018100603"));   //查询订单号是“2018100603”的订单（该订单不在订单列表中），测试失败
        }

        [TestMethod]
        public void SearchOrderByGoodsNameTest1()
        {
            Order order1 = new Order("2018100601", "陈1", "陈志鹏", "月饼", 2100, 100);
            Order order2 = new Order("2018100602", "陈2", "陈志鹏", "方便面", 1000, 1000);
            OrderService orderService = new OrderService();
            orderService.AddOrder(order1);
            orderService.AddOrder(order2);

            Assert.IsTrue(orderService.SearchOrderByGoodsName("方便面"));     //查询商品名称是"方便面"的订单，测试成功
        }

        [TestMethod]
        public void SearchOrderByGoodsNameTest2()
        {
            Order order1 = new Order("2018100601", "陈1", "陈志鹏", "月饼", 2100, 100);
            Order order2 = new Order("2018100602", "陈2", "陈志鹏", "方便面", 1000, 1000);
            OrderService orderService = new OrderService();
            orderService.AddOrder(order1);
            orderService.AddOrder(order2);

            Assert.IsTrue(orderService.SearchOrderByGoodsName("小米"));   //查询商品名称是"小米"的订单（该订单不在订单列表中），测试失败
        }

        [TestMethod]
        public void SearchOrderByOrderClientTest1()
        {
            Order order1 = new Order("2018100601", "陈1", "陈志鹏", "月饼", 2100, 100);
            Order order2 = new Order("2018100602", "陈2", "陈志鹏", "方便面", 1000, 1000);
            OrderService orderService = new OrderService();
            orderService.AddOrder(order1);
            orderService.AddOrder(order2);

            Assert.IsTrue(orderService.SearchOrderByOrderClient("陈2"));   //查询客户名称是“陈2”的订单，测试成功
        }

        [TestMethod]
        public void SearchOrderByOrderClientTest2()
        {
            Order order1 = new Order("2018100601", "陈1", "陈志鹏", "月饼", 2100, 100);
            Order order2 = new Order("2018100602", "陈2", "陈志鹏", "方便面", 1000, 1000);
            OrderService orderService = new OrderService();
            orderService.AddOrder(order1);
            orderService.AddOrder(order2);

            Assert.IsTrue(orderService.SearchOrderByOrderClient("陈一"));   //查询客户名称是“陈一”的订单（该订单不在订单列表中），测试失败
        }

        [TestMethod]
        public void SearchOrderByOrderTotalPriceATest1()
        {
            Order order1 = new Order("2018100601", "陈1", "陈志鹏", "月饼", 2100, 100);
            Order order2 = new Order("2018100602", "陈2", "陈志鹏", "方便面", 1000, 1000);
            OrderService orderService = new OrderService();
            orderService.AddOrder(order1);
            orderService.AddOrder(order2);

            Assert.IsTrue(orderService.SearchOrderByOrderTotalPriceA(1000));   //查询订单总金额大于10000的订单，测试成功
        }

        [TestMethod]
        public void SearchOrderByOrderTotalPriceATest2()
        {
            Order order1 = new Order("2018100601", "陈1", "陈志鹏", "月饼", 2100, 100);
            Order order2 = new Order("2018100602", "陈2", "陈志鹏", "方便面", 1000, 1000);
            OrderService orderService = new OrderService();
            orderService.AddOrder(order1);
            orderService.AddOrder(order2);

            Assert.IsTrue(orderService.SearchOrderByOrderTotalPriceA(1000000));   //查询订单总金额大于1000000的订单，（该种订单不存在）测试失败
        }

        [TestMethod]
        public void SearchOrderByOrderTotalPriceDTest1()
        {
            Order order1 = new Order("2018100601", "陈1", "陈志鹏", "月饼", 2100, 100);
            Order order2 = new Order("2018100602", "陈2", "陈志鹏", "方便面", 1000, 1000);
            OrderService orderService = new OrderService();
            orderService.AddOrder(order1);
            orderService.AddOrder(order2);

            Assert.IsTrue(orderService.SearchOrderByOrderTotalPriceD(1000));   //查询订单总金额大于10000的订单，测试成功
        }

        [TestMethod]
        public void SearchOrderByOrderTotalPriceDTest2()
        {
            Order order1 = new Order("2018100601", "陈1", "陈志鹏", "月饼", 2100, 100);
            Order order2 = new Order("2018100602", "陈2", "陈志鹏", "方便面", 1000, 1000);
            OrderService orderService = new OrderService();
            orderService.AddOrder(order1);
            orderService.AddOrder(order2);

            Assert.IsTrue(orderService.SearchOrderByOrderTotalPriceD(1000000));   //查询订单总金额大于1000000的订单，（该种订单不存在）测试失败
        }

        [TestMethod]
        public void ExportTest1()
        {
            Order order1 = new Order("2018100601", "陈1", "陈志鹏", "月饼", 2100, 100);
            Order order2 = new Order("2018100602", "陈2", "陈志鹏", "方便面", 1000, 1000);
            OrderService orderService = new OrderService();
            orderService.AddOrder(order1);
            orderService.AddOrder(order2);

            Assert.IsTrue(orderService.Export());
        }

        [TestMethod]
        public void ExportTest2()
        {
            OrderService orderService = new OrderService();
            Assert.IsTrue(orderService.Export());                  //订单列表为空（还未添加订单），测试成功
        }

        [TestMethod]
        public void ImportTest()
        {
            Order order1 = new Order("2018100601", "陈1", "陈志鹏", "月饼", 2100, 100);
            Order order2 = new Order("2018100602", "陈2", "陈志鹏", "方便面", 1000, 1000);
            OrderService orderService = new OrderService();
            orderService.AddOrder(order1);

            orderService.Export();                          //先生成一个XML文件
            Assert.IsTrue(orderService.Import(order2));     //再向该XML文件中添加订单2,测试成功
        }
    }
}
