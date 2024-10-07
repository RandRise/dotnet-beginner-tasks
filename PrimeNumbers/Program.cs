using System;

class Program {
    static void Main(string[] args)
    {
        Console.Write("Enter a number: ");
        if (int.TryParse(Console.ReadLine(),out int number) && number > 2)
        {
            Console.WriteLine($"Prime numbers less than {number}: ");
            for (int i=2; i < number; i++)
            {
                if (IsPrime(i))
                {
                    Console.Write(i+ " ");
                }
            }
        }
        else 
        {
            Console.WriteLine("Please enter a number greater than 2");
        }
    }

    static bool IsPrime(int num)
    {
        for (int i = 2; i<= Math.Sqrt(num); i++)
        {
            if (num % i == 0) {
                return false;
            }
        }
        return true;
    }
}