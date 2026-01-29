using System;
using System.Collections.Generic;

class Program
{
    static void Main()
    {
        List<double> grades = new List<double>();

        Console.Write("How many quiz grades do you want to enter: ");
        int n = int.Parse(Console.ReadLine());

        for (int i = 1; i <= n; i++)
        {
            Console.Write($"Enter quiz grade {i}: ");
            grades.Add(double.Parse(Console.ReadLine()));
        }

        double sum = 0;

        for (int i = 0; i < grades.Count; i++)
        {
            sum += grades[i];
        }

        double average = sum / grades.Count;
        Console.WriteLine($"Average: {average / 100:P}");
    }
}