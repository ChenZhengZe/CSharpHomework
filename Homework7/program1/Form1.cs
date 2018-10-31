using System;
using program2;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace program1
{
    public partial class Form1 : Form
    {
        OrderService neworderService = OrderService.GetInstance();

        public Form1()
        {
            InitializeComponent();
            Order order1 = new Order("2018100601", "陈1", "陈志鹏", "月饼", 2100, 100);
            Order order2 = new Order("2018100602", "陈2", "陈志鹏", "方便面", 1000, 1000);
            Order order3 = new Order("2018100603", "陈3", "陈志鹏", "苹果", 1000, 2530);
            Order order4 = new Order("2018100604", "陈4", "陈志鹏", "草莓", 2045, 400);

            neworderService.AddOrder(order1);
            neworderService.AddOrder(order2);
            neworderService.AddOrder(order3);
            neworderService.AddOrder(order4);

            bindingSource1.DataSource = neworderService.orderList;
        }

        private void button6_Click(object sender, EventArgs e)
        {
            List<Order> orderListNull = new List<Order>();
            bindingSource1.DataSource = orderListNull;
            bindingSource1.DataSource = neworderService.orderList;
            label8.Text = " 订单总数为 " + neworderService.GetOrderCounts() + "      ";
        }

        //添加订单
        private void button1_Click(object sender, EventArgs e)
        {
            string s1 = textBox1.Text;
            string s2 = textBox2.Text;
            string s3 = textBox3.Text;
            string s4 = textBox4.Text;
            string s5 = textBox5.Text;
            string s6 = textBox6.Text;

            double itemPrice = double.Parse(s5);
            double itemCounts = double.Parse(s6);

            Order newOrder = new Order(s1, s2, s3, s4, itemPrice, itemCounts);
            neworderService.AddOrder(newOrder);
            label8.Text = " 订单总数为 " + neworderService.GetOrderCounts() + "      ";

            Form form1 = new Form
            {
                Text = "添加订单",
                Size = new Size(300, 300),
                Location = new Point(600, 350),
            };

            Label label1_1 = new Label
            {
                Text = "确定添加该订单吗?",
                Size = new Size(150, 75),
                Location = new Point(100, 100),
            };

            Button button1_1 = new Button
            {
                Text = "添加",
                Size = new Size(50, 20),
                Location = new Point(75, 200),
            };

            Button button1_2 = new Button
            {
                Text = "取消",
                Size = new Size(50, 20),
                Location = new Point(175, 200),
            };

            form1.Controls.Add(label1_1);
            form1.Controls.Add(button1_1);
            form1.Controls.Add(button1_2);
            form1.ShowDialog();
            
        }
        //删除订单
        private void button2_Click(object sender, EventArgs e)
        {
           neworderService.DeleteOrder(neworderService.orderList[dataGridView1.CurrentRow.Index]);
           label8.Text = " 订单总数为 " + neworderService.GetOrderCounts() + "      ";
            Form form2 = new Form
            {
                Text = "删除订单",
                Size = new Size(300, 300),
                Location = new Point(600, 350),
            };

            Label label2_1 = new Label
            {
                Text = "确定删除该订单吗?",
                Size = new Size(150, 75),
                Location = new Point(100, 100),
            };

            Button button2_1 = new Button
            {
                Text = "删除",
                Size = new Size(50, 20),
                Location = new Point(75, 200),
            };

            Button button2_2 = new Button
            {
                Text = "取消",
                Size = new Size(50, 20),
                Location = new Point(175, 200),
            };

            form2.Controls.Add(label2_1);
            form2.Controls.Add(button2_1);
            form2.Controls.Add(button2_2);
            form2.ShowDialog();   
        }
        //查询订单
        private void button3_Click(object sender, EventArgs e)
        {
            bindingSource1.DataSource = neworderService.SearchOrderByGoodsName(textBox7.Text);
            if (neworderService.SearchOrderByGoodsName(textBox7.Text).Count == 0)
            {
                textBox7.Text = "查询无果!!!!!   请重新输入参数";
            }
        }           //通过商品名称查询订单
        private void button4_Click(object sender, EventArgs e)
        {
            bindingSource1.DataSource = neworderService.SearchOrderByOrderNumber(textBox8.Text);
            if (neworderService.SearchOrderByOrderNumber(textBox8.Text).Count == 0)
            {
                textBox8.Text = "查询无果!!!!!   请重新输入参数";
            }
        }           //通过订单号查询订单
        private void button5_Click(object sender, EventArgs e)
        {
            bindingSource1.DataSource = neworderService.SearchOrderByOrderClient(textBox9.Text);
            if (neworderService.SearchOrderByOrderClient(textBox9.Text).Count == 0)
            {
                textBox9.Text = "查询无果!!!!!   请重新输入参数";
            }
        }           //通过客户名称查询订单
        private void button11_Click(object sender, EventArgs e)
        {
            string s11 = textBox15.Text;
            double money11 = double.Parse(s11);
            bindingSource1.DataSource = neworderService.SearchOrderByOrderTotalPriceABig(money11);
            if (neworderService.SearchOrderByOrderTotalPriceABig(money11).Count == 0)
            {
                textBox15.Text = "查询无果!!!!!   请重新输入参数";
            }
        }          //查询订单金额大于某一数额的订单，并按金额升序排列
        private void button12_Click(object sender, EventArgs e)
        {
            string s12 = textBox16.Text;
            double money12 = double.Parse(s12);
            bindingSource1.DataSource = neworderService.SearchOrderByOrderTotalPriceASmall(money12);
            if (neworderService.SearchOrderByOrderTotalPriceASmall(money12).Count == 0)
            {
                textBox16.Text = "查询无果!!!!!   请重新输入参数";
            }
        }          //查询订单金额小于某一数额的订单，并按金额升序排列
        //修改订单
        private void button7_Click(object sender, EventArgs e)
        {
            neworderService.AlterOrderNumber(neworderService.orderList[dataGridView1.CurrentRow.Index], textBox10.Text);
        }           //修改订单号
        private void button8_Click(object sender, EventArgs e)
        {
            neworderService.AlterOrderClient(neworderService.orderList[dataGridView1.CurrentRow.Index], textBox11.Text);
        }           //修改客户名称
        private void button9_Click(object sender, EventArgs e)
        {
            string s9 = textBox13.Text;
            double itemPrice9 = double.Parse(s9);
            neworderService.AlterOrderGoodsName(neworderService.orderList[dataGridView1.CurrentRow.Index], textBox12.Text, itemPrice9);
        }           //修改商品名称及单价
        private void button10_Click(object sender, EventArgs e)
        {
            string s10 = textBox14.Text;
            double itemCounts10 = double.Parse(s10);
            neworderService.AlterOrderGoodsCounts(neworderService.orderList[dataGridView1.CurrentRow.Index], itemCounts10);
        }          //修改商品数量




        private void Form1_Load(object sender, EventArgs e){}
        private void textBox1_TextChanged(object sender, EventArgs e){}
        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e){}
        private void label8_Click(object sender, EventArgs e) {}

       
    }
}
