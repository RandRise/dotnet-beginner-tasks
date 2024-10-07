using System;

class Program
{
    static void Main(string[] args)
    {
        Console.Write("Enter the first number: ");
        string? inputA = Console.ReadLine();
        if (!int.TryParse(inputA, out int a))
        {
            Console.WriteLine("Invalid input. Please enter a valid integer.");
            return;
        }

        Console.Write("Enter the second number: ");
        string? inputB = Console.ReadLine();
        if (!int.TryParse(inputB, out int b))
        {
            Console.WriteLine("Invalid input. Please enter a valid integer.");
            return;
        }

        (int gcd, int x, int y) = FullGCD(a, b);

        Console.WriteLine($"GCD of {a} and {b} is: {gcd}");
        Console.WriteLine($"Coefficients x and y: {x} and {y}");
        Console.WriteLine($"{a} * {x} + {b} * {y} = {gcd}");
    }

    static (int, int, int) FullGCD(int a, int b)
    {
        if (b == 0) return (a, 1, 0);

        var (gcd, x1, y1) = FullGCD(b, a % b);

        int x = y1;
        int y = x1 - (a / b) * y1;

        return (gcd, x, y);
    }
}
