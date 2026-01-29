using System;

class Program
{
    static void Main()
    {
        Console.Write("Enter slope (m): ");
        double m = double.Parse(Console.ReadLine());

        Console.Write("Enter x value: ");
        double x = double.Parse(Console.ReadLine());

        Console.Write("Enter y-intercept (b): ");
        double b = double.Parse(Console.ReadLine());

        double y = LineValueForY(m, x, b);
        Console.WriteLine($"Y value: {y}");

        Console.Write("\nEnter a number for factorial: ");
        int number = int.Parse(Console.ReadLine());

        int result = Factorial(number);
        Console.WriteLine($"Factorial: {result}");
    }

    static double LineValueForY(double m, double x, double b)
    {
        return (m * x) + b;
    }

    static int Factorial(int n)
    {
        int result = 1;

        while (n > 1)
        {
            result *= n;
            n--;
        }

        return result;
    }
}