using System;

class Program
{
    static void Main(string[] args)
    {
        Console.Write("Enter the first number: ");
        string? inputA = Console.ReadLine();
        if (!int.TryParse(inputA, out int a))
        {
            Console.WriteLine("Please enter a valid int.");
            return;
        }

        Console.Write("Enter the second number: ");
        string? inputB = Console.ReadLine();
        if (!int.TryParse(inputB, out int b))
        {
            Console.WriteLine("Please enter a valid int");
            return;
        }

        int gcd = IterativeGCD(a, b);

        Console.WriteLine($"GCD of {a} and {b} is: {gcd}");
    }

    static int IterativeGCD(int a, int b)
    {
        while (b != 0)
        {
            int remainder = a % b;
            a = b;
            b = remainder;

        }

        return a;
    }
}