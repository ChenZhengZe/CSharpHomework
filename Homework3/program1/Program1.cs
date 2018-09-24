using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

//图形抽象父类
public abstract class Shape
{
    public abstract double ShapeAreaShow();
    public abstract void Draw();
   
}

//具体图形
//三角形类
class Triangle : Shape
{
    private double myBottomMargin;
    private double myHeight;

    public Triangle(double[] length)
    {
        myBottomMargin = length[0];
        myHeight = length[1];
    }

    public override double ShapeAreaShow()
    {
        return 0.5 * myBottomMargin * myHeight;
    }

    public override void Draw()
    {
        Console.WriteLine("Draw Triangle " + "(" + myBottomMargin + "," + myHeight + ")");
    }
}
//圆形类
class Circle: Shape     
{
    private double myRadius;     

    public Circle(double[] length)
    {
        myRadius = length[0];
    }

    public override double ShapeAreaShow()
    {
            return myRadius * myRadius * System.Math.PI;
    }

    public override void Draw()
    {
        Console.WriteLine("Draw Circle: " + myRadius);
    }
}
//正方形类
class Square : Shape
{
    private double mySide;        

    public Square(double[] length) 
    {
        mySide = length[0];
    }

    public override double ShapeAreaShow()
    {
         return mySide * mySide;
    }

    public override void Draw()
    {
        Console.WriteLine("Draw Square: " + mySide);
    }
}
//长方形类
class Rectangle : Shape
{
    private double myWidth;
    private double myHeight;

    public Rectangle(double[] length) 
    {
        myWidth = length[0];
        myHeight = length[1];
    }

    public override double ShapeAreaShow()
    {
            return myWidth * myHeight;
    }

    public override void Draw()
    {
        Console.WriteLine("Draw Rectangle " + "(" + myWidth + "," + myHeight + ")");
    }
}

//图形工厂类
public class ShapeFactory
{
    public static Shape createShape(string type, double[] length)
    {
        Shape sha = null;
        switch (type)
        {
            case "Triangle":
                sha = new Triangle(length);
                break;
            case "Circle":
                sha = new Circle(length);
                break;
            case "Square":
                sha = new Square(length);
                break;
            case "Rectangle":
                sha = new Rectangle(length);
                break;
        }
        return sha;
    }
}

namespace Homework3
{
    class Program1
    {
        static void Main(string[] args)
        {
            try
            {
                //实例化各种图形
                double[] T = new double[2];
                Console.Write("Please enter the bottom margin of the triangle: ");
                T[0] = Convert.ToDouble(Console.ReadLine());
                Console.Write("Please enter the height of the triangle: ");
                T[1] = Convert.ToDouble(Console.ReadLine());
                Shape triangle = ShapeFactory.createShape("Triangle",T);
                Console.WriteLine();

                double[] C = new double[1];
                Console.Write("Please enter the radius of the circle: ");
                C[0] = Convert.ToDouble(Console.ReadLine());
                Shape circle = ShapeFactory.createShape("Circle", C);
                Console.WriteLine();

                double[] S = new double[1];
                Console.Write("Please enter the length of a side of the square: ");
                S[0] = Convert.ToDouble(Console.ReadLine());
                Shape square = ShapeFactory.createShape("Square", S);
                Console.WriteLine();

                double[] R = new double[1];
                Console.Write("Please enter the width of the rectangle: ");
                T[0] = Convert.ToDouble(Console.ReadLine());
                Console.Write("Please enter the height of the rectangle: ");
                T[1] = Convert.ToDouble(Console.ReadLine());
                Shape rectangle = ShapeFactory.createShape("Rectangle", T);
                Console.WriteLine();

                //获取图形
                if (triangle != null)
                {
                    triangle.Draw();
                    Console.WriteLine("The area of the triangle is " + triangle.ShapeAreaShow());
                    Console.WriteLine();
                }
                if (circle != null)
                {
                    circle.Draw();
                    Console.WriteLine("The area of the circle is " + circle.ShapeAreaShow());
                    Console.WriteLine();
                }
                if (square != null)
                {
                    square.Draw();
                    Console.WriteLine("The area of the square is " + square.ShapeAreaShow());
                    Console.WriteLine();
                }
                if (rectangle != null)
                {
                    rectangle.Draw();
                    Console.WriteLine("The area of the rectangle is " + rectangle.ShapeAreaShow());
                    Console.WriteLine();
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("input error." + e.Message);
            }
        }
    }
}
