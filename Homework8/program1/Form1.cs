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
            Order order1 = new Order("20181112001", "陈1", "13949496191", "陈志鹏", "月饼", 2100, 100);
            Order order2 = new Order("20181112002", "陈2", "13949496192", "陈志鹏", "方便面", 1000, 1000);
            Order order3 = new Order("20181112003", "陈3", "13949496193", "陈志鹏", "苹果", 1000, 2530);
            Order order4 = new Order("20181112004", "陈4", "13949496194", "陈志鹏", "草莓", 2045, 400);
            Order order5 = new Order("20181112005", "陈5", "13949496195", "陈志鹏", "香蕉", 205, 700);
            Order order6 = new Order("20181112006", "陈6", "13949496196", "陈志鹏", "橘子", 45, 800);
            Order order7 = new Order("20181112007", "陈7", "13949496197", "陈志鹏", "橙子", 205, 900);

            neworderService.AddOrder(order1);
            neworderService.AddOrder(order2);
            neworderService.AddOrder(order3);
            neworderService.AddOrder(order4);
            neworderService.AddOrder(order5);
            neworderService.AddOrder(order6);
            neworderService.AddOrder(order7);

            bindingSource1.DataSource = neworderService.orderList;
        }

        //刷新订单
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
                DialogResult = DialogResult.OK,
            };

            Button button1_2 = new Button
            {
                Text = "取消",
                Size = new Size(50, 20),
                Location = new Point(175, 200),
                DialogResult = DialogResult.Cancel,
            };

            Form form1 = new Form
            {
                Text = "添加订单",
                Size = new Size(300, 300),
                Location = new Point(600, 350),
                FormBorderStyle = FormBorderStyle.FixedDialog,
                StartPosition = FormStartPosition.CenterParent,
                AcceptButton = button1_1,
                CancelButton = button1_2,
            };

            form1.Controls.Add(label1_1);
            form1.Controls.Add(button1_1);
            form1.Controls.Add(button1_2);
            form1.ShowDialog();

            if (form1.DialogResult == DialogResult.OK)
            {
                string s1 = textBox1.Text;
                string s2 = textBox2.Text;
                string s3 = textBox3.Text;
                string s4 = textBox4.Text;
                string s5 = textBox5.Text;
                string s6 = textBox6.Text;
                string s7 = textBox17.Text;

                double itemPrice = double.Parse(s5);
                double itemCounts = double.Parse(s6);

                Order newOrder = new Order(s1, s2, s7, s3, s4, itemPrice, itemCounts);
                neworderService.AddOrder(newOrder);
                label8.Text = "     订单总数为 " + neworderService.GetOrderCounts() + "      ";

                MessageBox.Show("订单添加成功");
                form1.Dispose();
            }
            else
            {
                MessageBox.Show("已取消添加");
                form1.Dispose();
            }

        }
        //删除订单
        private void button2_Click(object sender, EventArgs e)
        {
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
                DialogResult = DialogResult.OK,
            };

            Button button2_2 = new Button
            {
                Text = "取消",
                Size = new Size(50, 20),
                Location = new Point(175, 200),
                DialogResult = DialogResult.Cancel,
            };

            Form form2 = new Form
            {
                Text = "删除订单",
                Size = new Size(300, 300),
                Location = new Point(600, 350),
                FormBorderStyle = FormBorderStyle.FixedDialog,
                StartPosition = FormStartPosition.CenterParent,
                AcceptButton = button2_1,
                CancelButton = button2_2,
            };

            form2.Controls.Add(label2_1);
            form2.Controls.Add(button2_1);
            form2.Controls.Add(button2_2);
            form2.ShowDialog();

            if (form2.DialogResult == DialogResult.OK)
            {
                neworderService.DeleteOrder(neworderService.orderList[dataGridView1.CurrentRow.Index]);
                label8.Text = " 订单总数为 " + neworderService.GetOrderCounts() + "      ";

                MessageBox.Show("订单删除成功");
                form2.Dispose();
            }
            else
            {
                MessageBox.Show("已取消删除");
                form2.Dispose();
            }
        }

        //查询订单
        private void btnSearch_Click(object sender, EventArgs e)
        {
            switch (comboBox1.SelectedIndex)
            {
                case 0:     //通过订单号查询订单
                    bindingSource1.DataSource = neworderService.SearchOrderByOrderNumber(textBox7.Text);
                    if (neworderService.SearchOrderByOrderNumber(textBox7.Text).Count == 0)
                    {
                        textBox7.Text = "查询无果!!!!!   请重新输入参数";
                    }
                    break;
                case 1:     //通过商品名称查询订单
                    bindingSource1.DataSource = neworderService.SearchOrderByGoodsName(textBox7.Text);
                    if (neworderService.SearchOrderByGoodsName(textBox7.Text).Count == 0)
                    {
                        textBox7.Text = "查询无果!!!!!   请重新输入参数";
                    }
                    break;
                case 2:      //通过客户名称查询订单
                    bindingSource1.DataSource = neworderService.SearchOrderByOrderClient(textBox7.Text);
                    if (neworderService.SearchOrderByOrderClient(textBox7.Text).Count == 0)
                    {
                        textBox7.Text = "查询无果!!!!!   请重新输入参数";
                    }
                    break;
                case 3:      //查询订单金额大于某一数额的订单，并按金额升序排列
                    string s7_1 = textBox7.Text;
                    double money7_1 = double.Parse(s7_1);
                    bindingSource1.DataSource = neworderService.SearchOrderByOrderTotalPriceABig(money7_1);
                    if (neworderService.SearchOrderByOrderTotalPriceABig(money7_1).Count == 0)
                    {
                        textBox7.Text = "查询无果!!!!!   请重新输入参数";
                    }
                    break;
                case 4:       //查询订单金额大于某一数额的订单，并按金额降序排列
                    string s7_2 = textBox7.Text;
                    double money7_2 = double.Parse(s7_2);
                    bindingSource1.DataSource = neworderService.SearchOrderByOrderTotalPriceDBig(money7_2);
                    if (neworderService.SearchOrderByOrderTotalPriceDBig(money7_2).Count == 0)
                    {
                        textBox7.Text = "查询无果!!!!!   请重新输入参数";
                    }
                    break;
                case 5:     //查询订单金额小于某一数额的订单，并按金额升序排列
                    string s7_3 = textBox7.Text;
                    double money7_3 = double.Parse(s7_3);
                    bindingSource1.DataSource = neworderService.SearchOrderByOrderTotalPriceASmall(money7_3);
                    if (neworderService.SearchOrderByOrderTotalPriceASmall(money7_3).Count == 0)
                    {
                        textBox7.Text = "查询无果!!!!!   请重新输入参数";
                    }
                    break;
                case 6:    //查询订单金额小于某一数额的订单，并按金额降序排列
                    string s7_4 = textBox7.Text;
                    double money7_4 = double.Parse(s7_4);
                    bindingSource1.DataSource = neworderService.SearchOrderByOrderTotalPriceDSmall(money7_4);
                    if (neworderService.SearchOrderByOrderTotalPriceDSmall(money7_4).Count == 0)
                    {
                        textBox7.Text = "查询无果!!!!!   请重新输入参数";
                    }
                    break;
            }
        }

        //修改订单
        //修改订单号
        private void button7_Click(object sender, EventArgs e)
        {
            Label label7_1 = new Label
            {
                Text = "确定修改该订单的订单号吗?",
                Size = new Size(200, 75),
                Location = new Point(70, 100),
            };

            Button button7_1 = new Button
            {
                Text = "修改",
                Size = new Size(50, 20),
                Location = new Point(75, 200),
                DialogResult = DialogResult.OK,
            };

            Button button7_2 = new Button
            {
                Text = "取消",
                Size = new Size(50, 20),
                Location = new Point(175, 200),
                DialogResult = DialogResult.Cancel,
            };

            Form form7 = new Form
            {
                Text = "修改订单号",
                Size = new Size(300, 300),
                Location = new Point(600, 350),
                FormBorderStyle = FormBorderStyle.FixedDialog,
                StartPosition = FormStartPosition.CenterParent,
                AcceptButton = button7_1,
                CancelButton = button7_2,
            };

            form7.Controls.Add(label7_1);
            form7.Controls.Add(button7_1);
            form7.Controls.Add(button7_2);
            form7.ShowDialog();

            if (form7.DialogResult == DialogResult.OK)
            {
                neworderService.AlterOrderNumber(neworderService.orderList[dataGridView1.CurrentRow.Index], textBox10.Text);
                label8.Text = " 订单总数为 " + neworderService.GetOrderCounts() + "      ";

                MessageBox.Show("该订单的订单号修改成功");
                form7.Dispose();
            }
            else
            {
                MessageBox.Show("已取消修改");
                form7.Dispose();
            }
        }
        //修改客户名称
        private void button8_Click(object sender, EventArgs e)
        {
            Label label8_1 = new Label
            {
                Text = "确定修改该订单的客户名称吗?",
                Size = new Size(200, 75),
                Location = new Point(65, 100),
            };

            Button button8_1 = new Button
            {
                Text = "修改",
                Size = new Size(50, 20),
                Location = new Point(75, 200),
                DialogResult = DialogResult.OK,
            };

            Button button8_2 = new Button
            {
                Text = "取消",
                Size = new Size(50, 20),
                Location = new Point(175, 200),
                DialogResult = DialogResult.Cancel,
            };

            Form form8 = new Form
            {
                Text = "修改客户名称",
                Size = new Size(300, 300),
                Location = new Point(600, 350),
                FormBorderStyle = FormBorderStyle.FixedDialog,
                StartPosition = FormStartPosition.CenterParent,
                AcceptButton = button8_1,
                CancelButton = button8_2,
            };

            form8.Controls.Add(label8_1);
            form8.Controls.Add(button8_1);
            form8.Controls.Add(button8_2);
            form8.ShowDialog();

            if (form8.DialogResult == DialogResult.OK)
            {
                neworderService.AlterOrderClient(neworderService.orderList[dataGridView1.CurrentRow.Index], textBox11.Text);
                label8.Text = " 订单总数为 " + neworderService.GetOrderCounts() + "      ";

                MessageBox.Show("该订单的客户名称修改成功");
                form8.Dispose();
            }
            else
            {
                MessageBox.Show("已取消修改");
                form8.Dispose();
            }
        }
        //修改商品名称及单价
        private void button9_Click(object sender, EventArgs e)
        {
            Label label9_1 = new Label
            {
                Text = "确定修改该订单的商品名称及单价吗?",
                Size = new Size(250, 75),
                Location = new Point(55, 100),
            };

            Button button9_1 = new Button
            {
                Text = "修改",
                Size = new Size(50, 20),
                Location = new Point(75, 200),
                DialogResult = DialogResult.OK,
            };

            Button button9_2 = new Button
            {
                Text = "取消",
                Size = new Size(50, 20),
                Location = new Point(175, 200),
                DialogResult = DialogResult.Cancel,
            };

            Form form9 = new Form
            {
                Text = "修改商品名称及单价",
                Size = new Size(300, 300),
                Location = new Point(600, 350),
                FormBorderStyle = FormBorderStyle.FixedDialog,
                StartPosition = FormStartPosition.CenterParent,
                AcceptButton = button9_1,
                CancelButton = button9_2,
            };

            form9.Controls.Add(label9_1);
            form9.Controls.Add(button9_1);
            form9.Controls.Add(button9_2);
            form9.ShowDialog();

            if (form9.DialogResult == DialogResult.OK)
            {
                string s9 = textBox13.Text;
                double itemPrice9 = double.Parse(s9);
                neworderService.AlterOrderGoodsName(neworderService.orderList[dataGridView1.CurrentRow.Index], textBox12.Text, itemPrice9);
                label8.Text = " 订单总数为 " + neworderService.GetOrderCounts() + "      ";

                MessageBox.Show("该订单的商品名称及单价修改成功");
                form9.Dispose();
            }
            else
            {
                MessageBox.Show("已取消修改");
                form9.Dispose();
            }
        }
        //修改商品数量
        private void button10_Click(object sender, EventArgs e)
        {
            Label label10_1 = new Label
            {
                Text = "确定修改该订单的商品数量吗?",
                Size = new Size(250, 75),
                Location = new Point(65, 100),
            };

            Button button10_1 = new Button
            {
                Text = "修改",
                Size = new Size(50, 20),
                Location = new Point(75, 200),
                DialogResult = DialogResult.OK,
            };

            Button button10_2 = new Button
            {
                Text = "取消",
                Size = new Size(50, 20),
                Location = new Point(175, 200),
                DialogResult = DialogResult.Cancel,
            };

            Form form10 = new Form
            {
                Text = "修改商品数量",
                Size = new Size(300, 300),
                Location = new Point(600, 350),
                FormBorderStyle = FormBorderStyle.FixedDialog,
                StartPosition = FormStartPosition.CenterParent,
                AcceptButton = button10_1,
                CancelButton = button10_2,
            };

            form10.Controls.Add(label10_1);
            form10.Controls.Add(button10_1);
            form10.Controls.Add(button10_2);
            form10.ShowDialog();

            if (form10.DialogResult == DialogResult.OK)
            {
                string s10 = textBox14.Text;
                double itemCounts10 = double.Parse(s10);
                neworderService.AlterOrderGoodsCounts(neworderService.orderList[dataGridView1.CurrentRow.Index], itemCounts10);
                label8.Text = " 订单总数为 " + neworderService.GetOrderCounts() + "      ";

                MessageBox.Show("该订单的商品数量修改成功");
                form10.Dispose();
            }
            else
            {
                MessageBox.Show("已取消修改");
                form10.Dispose();
            }
        }

        //导出订单为XML文件
        private void btnExport_Click(object sender, EventArgs e)
        {
            DialogResult result = saveFileDialog1.ShowDialog();
            if (result.Equals(DialogResult.OK))
            {
                string fileName = saveFileDialog1.FileName;
                neworderService.Export(fileName);   
            }
        }

        //导入订单
        private void btnImport_Click(object sender, EventArgs e)
        {
            DialogResult result = openFileDialog1.ShowDialog();
            if (result.Equals(DialogResult.OK))
            {
                String fileName = openFileDialog1.FileName;
                neworderService.Import(fileName);
                List<Order> orderListNull = new List<Order>();
                bindingSource1.DataSource = orderListNull;
                bindingSource1.DataSource = neworderService.orderList;
                label8.Text = " 订单总数为 " + neworderService.GetOrderCounts() + "      ";
            }

        }

            private void Form1_Load(object sender, EventArgs e) { }
            private void textBox1_TextChanged(object sender, EventArgs e) { }
            private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e) { }
            private void label8_Click(object sender, EventArgs e) { }
            private void panel1_Paint(object sender, PaintEventArgs e) { }
            private void dataGridView1_CellContentClick_1(object sender, DataGridViewCellEventArgs e) { }
            private void comboBox1_SelectedIndexChanged(object sender, EventArgs e) { }
            private void button3_Click(object sender, EventArgs e) { }
    }
}
