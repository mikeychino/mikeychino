using System;

class Program
{
    static void Main()
    {
        char[] lower = { 'a','b','c','d','e','f','g','h','i','j','k','l','m','n','o','p','q','r','s','t','u','v','w','x','y','z' };
        char[] upper = { 'A','B','C','D','E','F','G','H','I','J','K','L','M','N','O','P','Q','R','S','T','U','V','W','X','Y','Z' };

        string first =
            $"{upper[12]}{lower[8]}{lower[2]}{lower[7]}{lower[0]}{lower[4]}{lower[11]}";

        string last =
            $"{upper[2]}{lower[20]}{lower[0]}{lower[3]}{lower[17]}{lower[0]}{lower[3]}{lower[14]}";

        Console.WriteLine($"{first} {last}");
    }
}
