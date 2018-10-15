using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace program2
{
    public partial class Form1 : Form
    {
        private Graphics graphics;
        double th1 = 30 * Math.PI / 180;
        double th2 = 20 * Math.PI / 180;
        double length = 300;
        double per1 = 0.6;
        double per2 = 0.7;
        double k = 1;

        public Form1()
        {
            InitializeComponent();
            this.AutoScaleBaseSize = new Size(6, 14);
            this.ClientSize = new Size(1200, 900);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (graphics == null)
            {
                graphics = this.CreateGraphics();
            }
            DrawCayleyTree(15, 600, 910, length, k, -Math.PI / 2);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if(graphics == null)
            {
                graphics = this.CreateGraphics();
            }
            string s1 = textBox1.Text;
            string s2 = textBox2.Text;
            string s3 = textBox3.Text;
            string s4 = textBox4.Text;
            int angle1 = int.Parse(s1);
            int angle2 = int.Parse(s2);
            th1 = angle1 * Math.PI / 180;
            th2 = angle2 * Math.PI / 180;
            length = double.Parse(s3);
            k = double.Parse(s4);
            DrawCayleyTree(15, 600, 910, length, k, -Math.PI / 2);
        }

        private void DrawCayleyTree(int n, double x0, double y0, double leng,double k, double th)
        {
            if (n == 0) return;

            double x1 = x0 + leng * Math.Cos(th);
            double y1 = y0 + leng * Math.Sin(th);

            double x2 = x0 + k * leng * Math.Cos(th);
            double y2 = y0 + k * leng * Math.Sin(th);

            DrawLine(x0, y0, x1, y1);

            DrawCayleyTree(n - 1, x1, y1, per1 * leng, k, th + th1);
            DrawCayleyTree(n - 1, x2, y2, per2 * leng, k, th - th2);
        }

        private void DrawLine(double x0, double y0, double x1, double y1)
        {
            Pen[] pens = new Pen[] { Pens.Blue, Pens.Red, Pens.Purple, Pens.PowderBlue, Pens.Brown, Pens.DarkOrange ,Pens.DarkSeaGreen,Pens.DeepPink,Pens.DarkSlateGray};
            Random random = new Random();
            int num = random.Next(0,8);
            graphics.DrawLine(pens[num],(int)x0, (int)y0, (int)x1, (int)y1);        
        }

        private void label1_Click(object sender, EventArgs e)
        {
           
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox4_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox5_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox6_TextChanged(object sender, EventArgs e)
        {

        }

        private void label5_Click(object sender, EventArgs e)
        {

        }

        private void label6_Click(object sender, EventArgs e)
        {

        }
    }
}
