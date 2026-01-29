using System;

class Program
{
    static void Main()
    {
        string[] items = { "apples", "oranges", "bananas", "grapes", "blueberries" };
        double[] prices = { 0.99, 0.50, 0.50, 2.99, 1.99 };

        Console.Write("Enter the item you want: ");
        string input = Console.ReadLine().Trim().ToLower();

        int index = -1;

        for (int i = 0; i < items.Length; i++)
        {
            if (items[i] == input)
            {
                index = i;
                break;
            }
        }

        if (index == -1)
        {
            Console.WriteLine("Error: item not found. Check spelling.");
        }
        else
        {
            Console.WriteLine($"Price: {prices[index]:C}");
        }
    }
}
