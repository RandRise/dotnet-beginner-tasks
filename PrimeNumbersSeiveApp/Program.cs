using System;

class Program
{
    static void Main(string[] args)
    {
        Console.Write("Enter a number: ");
        string? input = Console.ReadLine();
        if (int.TryParse(input, out int n))
        {
            bool[] primes = Sieve(n);
            Console.WriteLine($"Prime numbers: ");

            for (int i = 2; i <= n; i++)
            {
                if (primes[i])
                {
                    Console.Write(i + " ");
                }
            }
            Console.WriteLine();
        }
        else
        {
            Console.WriteLine("Please enter a valid integer");
        }

    }

    static bool[] Sieve(int n)
    {
        bool[] isPrime = new bool[n + 1];
        for (int i = 2; i <= n; i++)
        {
            isPrime[i] = true;
        }
        for (int i = 2; i * i <= n; i++)
        {
            if (isPrime[i])
            {
                for (int j = i * i; j <= n; j += i)
                {
                    isPrime[j] = false;
                }
            }
        }
        return isPrime;
    }
}