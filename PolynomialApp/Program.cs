using System;
using System.Collections.Generic;

class Program
{
    static void Main(string[] args)
    {
        Console.Write("Enter the degree of the first polynomial: ");
        if (!int.TryParse(Console.ReadLine(), out int degree1) || degree1 < 0)
        {
            Console.WriteLine("Incorrect degree, Please Enter an integer");
            return;
        }

        List<double> coefficients1 = InputCoefficients(degree1);
        Polynomial poly1 = new Polynomial(coefficients1);
        Console.WriteLine($"First Polynomial: {poly1}");

        Console.WriteLine("Enter the degree of the second polynomial: ");
        if (!int.TryParse(Console.ReadLine(), out int degree2) || degree2 < 0)
        {
            Console.WriteLine("Invalid degree");
            return;
        }

        List<double> coefficients2 = InputCoefficients(degree2);
        Polynomial poly2 = new Polynomial(coefficients2);
        Console.WriteLine($"Second Polynomial: {poly2}");

        Polynomial sum = poly1.Add(poly2);
        Console.WriteLine($"Sum: {sum}");

        Polynomial product = poly1.Multiply(poly2);
        Console.WriteLine($"Product: {product}");

        (Polynomial quotient, Polynomial remainder) = poly1.Divide(poly2);
        Console.WriteLine($"Quotient: {quotient}");
        Console.WriteLine($"Remainder: {remainder}");

        Console.WriteLine("\nRoots for the first polynomial: ");
        List<double> roots1 = poly1.FindRoots();
        if (roots1.Count > 0)
        {
            foreach (double root in roots1)
            {
                Console.WriteLine(root);
            }
        }

        Console.WriteLine("\nRoots for the second polynomial: ");
        List<double> roots2 = poly2.FindRoots();
        if (roots2.Count > 0)
        {
            foreach (double root in roots2)
            {
                Console.WriteLine(root);
            }
        }
    }

    static List<double> InputCoefficients(int degree)
    {
        List<double> coefficients = new List<double>();
        Console.WriteLine("Enter the coefficients: ");

        for (int i = degree; i >= 0; i--)
        {
            Console.Write($"Coefficient for x^{i}: ");
            string? input = Console.ReadLine();
            if (!double.TryParse(input, out double coeff))
            {
                Console.WriteLine("Invalid Coefficient");
                return null;
            }
            coefficients.Add(coeff);
        }

        coefficients.Reverse();
        return coefficients;
    }
}

public class Polynomial
{
    public List<double> coefficients;

    public Polynomial(List<double> coefficients)
    {
        this.coefficients = RemoveTrailingZeros(coefficients);
    }

    public override string ToString()
    {
        string polynomial = "";
        int degree = coefficients.Count - 1;

        for (int i = degree; i >= 0; i--)
        {
            double coeff = coefficients[i];

            if (coeff == 0)
                continue;

            string sign = "";
            if (polynomial.Length > 0)
                sign = coeff > 0 ? " + " : " - ";
            else if (coeff < 0)
                sign = "-";

            double absCoeff = Math.Abs(coeff);

            string term = "";
            if (i == 0) term = $"{absCoeff}";
            else if (i == 1) term = absCoeff == 1 ? "x" : $"{absCoeff}x";
            else term = absCoeff == 1 ? $"x^{i}" : $"{absCoeff}x^{i}";

            polynomial += sign + term;
        }

        return polynomial == "" ? "0" : polynomial;
    }

    public List<double> RemoveTrailingZeros(List<double> coefficients)
    {
        int i = coefficients.Count - 1;
        while (i >= 0 && coefficients[i] == 0)
        {
            coefficients.RemoveAt(i);
            i--;
        }
        return coefficients;
    }

    public int Degree()
    {
        return coefficients.Count - 1;
    }

    public Polynomial Add(Polynomial otherPoly)
    {
        int maxDegree = Math.Max(this.Degree(), otherPoly.Degree());
        List<double> resultCoefficients = new List<double>(new double[maxDegree + 1]);

        for (int i = 0; i <= maxDegree; i++)
        {
            double coeff1 = (i <= this.Degree()) ? this.coefficients[i] : 0;
            double coeff2 = (i <= otherPoly.Degree()) ? otherPoly.coefficients[i] : 0;
            resultCoefficients[i] = coeff1 + coeff2;
        }

        return new Polynomial(resultCoefficients);
    }

    public Polynomial Multiply(Polynomial OtherPoly)
    {
        int resultDegree = this.Degree() + OtherPoly.Degree();
        List<double> resultCoefficients = new List<double>(new double[resultDegree + 1]);

        for (int i = 0; i <= this.Degree(); i++)
        {
            for (int j = 0; j <= OtherPoly.Degree(); j++)
            {
                resultCoefficients[i + j] += this.coefficients[i] * OtherPoly.coefficients[j];
            }
        }

        return new Polynomial(resultCoefficients);
    }

    public (Polynomial quotient, Polynomial remainder) Divide(Polynomial divisor)
    {
        List<double> dividendCoefficients = new List<double>(this.coefficients);
        int dividendDegree = this.Degree();
        int divisorDegree = divisor.Degree();

        List<double> quotientCoefficients = new List<double>(new double[dividendDegree - divisorDegree + 1]);

        while (dividendDegree >= divisorDegree)
        {
            double leadingCoefficient = dividendCoefficients[dividendDegree] / divisor.coefficients[divisorDegree];
            List<double> termCoefficients = new List<double>(new double[dividendDegree - divisorDegree + 1]);
            termCoefficients[dividendDegree - divisorDegree] = leadingCoefficient;

            Polynomial term = new Polynomial(termCoefficients);
            quotientCoefficients[dividendDegree - divisorDegree] = leadingCoefficient;

            Polynomial productTerm = term.Multiply(divisor);
            dividendCoefficients = new Polynomial(dividendCoefficients).Subtract(productTerm).coefficients;
            dividendDegree = new Polynomial(dividendCoefficients).Degree();
        }

        Polynomial remainder = new Polynomial(dividendCoefficients);
        Polynomial quotient = new Polynomial(quotientCoefficients);

        return (quotient, remainder);
    }

    public Polynomial Subtract(Polynomial OtherPoly)
    {
        int maxDegree = Math.Max(this.Degree(), OtherPoly.Degree());
        List<double> resultCoefficients = new List<double>(new double[maxDegree + 1]);

        for (int i = 0; i <= maxDegree; i++)
        {
            double coeff1 = i <= this.Degree() ? this.coefficients[i] : 0;
            double coeff2 = i <= OtherPoly.Degree() ? OtherPoly.coefficients[i] : 0;
            resultCoefficients[i] = coeff1 - coeff2;
        }

        return new Polynomial(resultCoefficients);
    }

    public List<double> FindRoots()
    {
        int degree = this.Degree();
        List<double> roots = new List<double>();

        if (degree == 1)
        {
            double a = coefficients[1];
            double b = coefficients[0];
            roots.Add(-b / a);
        }
        else if (degree == 2)
        {
            double a = coefficients[2];
            double b = coefficients[1];
            double c = coefficients[0];
            double discriminant = b * b - 4 * a * c;

            if (discriminant > 0)
            {
                roots.Add((-b + Math.Sqrt(discriminant)) / (2 * a));
                roots.Add((-b - Math.Sqrt(discriminant)) / (2 * a));
            }
            else if (discriminant == 0)
            {
                roots.Add(-b / (2 * a));
            }
        }
        else
        {
            Console.WriteLine("Degree higher than 2");
        }

        return roots;
    }
}
