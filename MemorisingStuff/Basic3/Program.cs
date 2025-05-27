using System;
using System.Collections.Generic;
using System.Linq;

record Shape;
record Circle(double Radius) : Shape;
record Rectangle(double Width, double Height) : Shape;
record Triangle(double A, double B, double C) : Shape;

class Program
{
    static void Main()
    {
        var shapes = new List<Shape>
        {
            new Circle(5),
            new Rectangle(4, 6),
            new Triangle(3, 4, 5),
            new Circle(2.5),
            new Rectangle(2, 8),
        };

        var areas = shapes.Select(shape => shape switch
        {
            Circle(var r) => Math.PI * r * r,
            Rectangle(var w, var h) => w * h,
            Triangle(var a, var b, var c) when a + b > c && a + c > b && b + c > a =>
                TriangleArea(a, b, c),
            _ => 0
        });

        foreach (var area in areas)
        {
            Console.WriteLine($"{area:F2}");
        }
    }

    static double TriangleArea(double a, double b, double c)
    {
        // Law of Cosines to find angle C
        double cosC = (a * a + b * b - c * c) / (2 * a * b);
        cosC = Math.Max(-1, Math.Min(1, cosC));
        double angleC = Math.Acos(cosC);
        return 0.5 * a * b * Math.Sin(angleC);
    }
}