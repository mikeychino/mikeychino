using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;

namespace SalesReceipts
{
    internal class Receipt
    {
        public int CustomerID { get; set; }
        public int CogQuantity { get; set; }
        public int GearQuantity { get; set; }
        public DateTime SaleDate { get; set; }
        public double SalesTaxPercent { get; set; }

        private readonly double CogPrice;
        private readonly double GearPrice;

        public Receipt()
        {
            CustomerID = 0;
            CogQuantity = 0;
            GearQuantity = 0;

            SaleDate = DateTime.Now;
            SalesTaxPercent = 0.089;

            CogPrice = 79.99;
            GearPrice = 250.00;
        }

        public Receipt(int id, int cog, int gear) : this()
        {
            CustomerID = id;
            CogQuantity = cog;
            GearQuantity = gear;
        }

        public double CalculateTotal()
        {
            double netAmount = CalculateNetAmount();
            double taxAmount = CalculateTaxAmount();
            return netAmount + taxAmount;
        }

        public void PrintReceipt()
        {
            CultureInfo usd = CultureInfo.GetCultureInfo("en-US");

            int totalItems = CogQuantity + GearQuantity;

            double markupPercent = 0.15;
            if (CogQuantity > 10 || GearQuantity > 10 || totalItems >= 16)
            {
                markupPercent = 0.125;
            }

            double cogUnitWithMarkup = CogPrice * (1 + markupPercent);
            double gearUnitWithMarkup = GearPrice * (1 + markupPercent);

            double net = CalculateNetAmount();
            double tax = CalculateTaxAmount();
            double total = CalculateTotal();

            Console.WriteLine("========================================");
            Console.WriteLine("RECEIPT");
            Console.WriteLine("========================================");
            Console.WriteLine($"Sale Date and Time : {SaleDate}");
            Console.WriteLine($"Customer ID        : {CustomerID}");
            Console.WriteLine();
            Console.WriteLine($"Number of Cogs     : {CogQuantity}");
            Console.WriteLine($"Number of Gears    : {GearQuantity}");
            Console.WriteLine($"Total Items        : {totalItems}");
            Console.WriteLine();
            Console.WriteLine($"Base Cog Price     : {CogPrice.ToString("C2", usd)}");
            Console.WriteLine($"Base Gear Price    : {GearPrice.ToString("C2", usd)}");
            Console.WriteLine($"Markup Percent     : {(markupPercent * 100):0.0}%");
            Console.WriteLine($"Cog Unit With Mark : {cogUnitWithMarkup.ToString("C2", usd)}");
            Console.WriteLine($"Gear Unit With Mark: {gearUnitWithMarkup.ToString("C2", usd)}");
            Console.WriteLine();
            Console.WriteLine($"Net Amount         : {net.ToString("C2", usd)}");
            Console.WriteLine($"Sales Tax Percent  : {(SalesTaxPercent * 100):0.0}%");
            Console.WriteLine($"Tax Amount         : {tax.ToString("C2", usd)}");
            Console.WriteLine("----------------------------------------");
            Console.WriteLine($"Total              : {total.ToString("C2", usd)}");
            Console.WriteLine("========================================");
            Console.WriteLine();
        }

        private double CalculateTaxAmount()
        {
            double net = CalculateNetAmount();
            return net * SalesTaxPercent;
        }

        private double CalculateNetAmount()
        {
            int totalItems = CogQuantity + GearQuantity;

            double markupPercent = 0.15;
            if (CogQuantity > 10 || GearQuantity > 10 || totalItems >= 16)
            {
                markupPercent = 0.125;
            }

            double cogUnitWithMarkup = CogPrice * (1 + markupPercent);
            double gearUnitWithMarkup = GearPrice * (1 + markupPercent);

            double netAmount = (CogQuantity * cogUnitWithMarkup) + (GearQuantity * gearUnitWithMarkup);
            return netAmount;
        }
    }

    internal class Program
    {
        static void Main(string[] args)
        {
            List<Receipt> receipts = new List<Receipt>();

            Console.WriteLine("Sales Receipts");
            Console.WriteLine();

            while (true)
            {
                int customerId = ReadInt("Enter Customer ID: ", 0);
                int cogs = ReadInt("Enter number of Cogs: ", 0);
                int gears = ReadInt("Enter number of Gears: ", 0);

                Receipt r = new Receipt(customerId, cogs, gears);

                Console.WriteLine();
                Console.WriteLine("Receipt for this sale:");
                Console.WriteLine();

                r.PrintReceipt();
                receipts.Add(r);

                if (!ReadYesNo("Is there another order to enter? (y/n): "))
                {
                    break;
                }

                Console.WriteLine();
            }

            if (receipts.Count == 0)
            {
                Console.WriteLine("No orders entered.");
                return;
            }

            while (true)
            {
                Console.WriteLine("Choose an option:");
                Console.WriteLine("1 Print all receipts for a Customer ID");
                Console.WriteLine("2 Print all receipts for the day");
                Console.WriteLine("3 Print the receipt with the highest total");
                Console.WriteLine("4 Exit");
                Console.Write("Option: ");

                string choice = (Console.ReadLine() ?? "").Trim();
                Console.WriteLine();

                if (choice == "1")
                {
                    int id = ReadInt("Enter Customer ID: ", 0);

                    var matches = receipts
                        .Where(x => x.CustomerID == id)
                        .OrderBy(x => x.SaleDate)
                        .ToList();

                    if (matches.Count == 0)
                    {
                        Console.WriteLine($"No receipts found for Customer ID {id}.");
                        Console.WriteLine();
                    }
                    else
                    {
                        foreach (var rec in matches)
                        {
                            rec.PrintReceipt();
                        }
                    }
                }
                else if (choice == "2")
                {
                    DateTime today = DateTime.Today;

                    var matches = receipts
                        .Where(x => x.SaleDate.Date == today)
                        .OrderBy(x => x.SaleDate)
                        .ToList();

                    if (matches.Count == 0)
                    {
                        Console.WriteLine("No receipts found for today.");
                        Console.WriteLine();
                    }
                    else
                    {
                        foreach (var rec in matches)
                        {
                            rec.PrintReceipt();
                        }
                    }
                }
                else if (choice == "3")
                {
                    Receipt best = receipts
                        .OrderByDescending(x => x.CalculateTotal())
                        .First();

                    best.PrintReceipt();
                }
                else if (choice == "4")
                {
                    break;
                }
                else
                {
                    Console.WriteLine("Invalid option.");
                    Console.WriteLine();
                }

                if (!ReadYesNo("Would you like to perform another function? (y/n): "))
                {
                    break;
                }

                Console.WriteLine();
            }
        }

        private static int ReadInt(string prompt, int min)
        {
            while (true)
            {
                Console.Write(prompt);
                string input = (Console.ReadLine() ?? "").Trim();

                if (int.TryParse(input, out int value) && value >= min)
                {
                    return value;
                }

                Console.WriteLine($"Enter a valid integer that is at least {min}.");
            }
        }

        private static bool ReadYesNo(string prompt)
        {
            while (true)
            {
                Console.Write(prompt);
                string input = (Console.ReadLine() ?? "").Trim().ToLowerInvariant();

                if (input == "y" || input == "yes") return true;
                if (input == "n" || input == "no") return false;

                Console.WriteLine("Type y or n.");
            }
        }
    }
}
