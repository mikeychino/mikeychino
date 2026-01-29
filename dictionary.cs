using System;
using System.Collections.Generic;

class Program
{
    static void Main()
    {
        Dictionary<string, double> fruitPrices = new Dictionary<string, double>()
        {
            { "apples", 0.99 },
            { "oranges", 0.50 },
            { "bananas", 0.50 },
            { "grapes", 2.99 },
            { "blueberries", 1.99 }
        };

        Console.Write("Enter the item you want: ");
        string input = Console.ReadLine().Trim().ToLower();

        if (fruitPrices.ContainsKey(input))
        {
            Console.WriteLine($"Price: {fruitPrices[input]:C}");
        }
        else
        {
            Console.WriteLine("Error: item not found. Check spelling.");
        }
    }
}
