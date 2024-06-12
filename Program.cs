using System;

public class ProperFraction
{
    private int numerator;
    private int denominator;
    private int wholePart;

    // Властивості
    public int Numerator
    {
        get { return numerator; }
        set
        {
            if (value < 0)
                throw new ArgumentException("Numerator cannot be negative");
            numerator = value;
        }
    }

    public int Denominator
    {
        get { return denominator; }
        set
        {
            if (value <= 0)
                throw new ArgumentException("Denominator must be greater than zero");
            denominator = value;
        }
    }

    public int WholePart
    {
        get { return wholePart; }
        set { wholePart = value; }
    }

    // Конструктори
    public ProperFraction()
    {
        Numerator = 0;
        Denominator = 1;
        WholePart = 0;
    }

    public ProperFraction(int numerator, int denominator)
    {
        Numerator = numerator;
        Denominator = denominator;
        WholePart = 0;
        Simplify();
    }

    public ProperFraction(int numerator, int denominator, int wholePart)
    {
        Numerator = numerator;
        Denominator = denominator;
        WholePart = wholePart;
        Simplify();
    }

    // Функція введення
    public void Input()
    {
        Console.Write("Enter numerator: ");
        Numerator = int.Parse(Console.ReadLine());
        Console.Write("Enter denominator: ");
        Denominator = int.Parse(Console.ReadLine());
        Console.Write("Enter whole part: ");
        WholePart = int.Parse(Console.ReadLine());
        Simplify();
    }

    // Перевизначення функції ToString
    public override string ToString()
    {
        return $"{WholePart} {Numerator}/{Denominator}";
    }

    // Метод спрощення дробу
    private void Simplify()
    {
        if (Numerator >= Denominator)
        {
            WholePart += Numerator / Denominator;
            Numerator %= Denominator;
        }

        int gcd = GCD(Numerator, Denominator);
        Numerator /= gcd;
        Denominator /= gcd;
    }

    private int GCD(int a, int b)
    {
        while (b != 0)
        {
            int temp = b;
            b = a % b;
            a = temp;
        }
        return a;
    }

    // Операції додавання, віднімання, множення, ділення
    public static ProperFraction operator +(ProperFraction a, ProperFraction b)
    {
        int commonDenominator = a.Denominator * b.Denominator;
        int newNumerator = a.Numerator * b.Denominator + b.Numerator * a.Denominator;
        int newWholePart = a.WholePart + b.WholePart;

        return new ProperFraction(newNumerator, commonDenominator, newWholePart);
    }

    public static ProperFraction operator -(ProperFraction a, ProperFraction b)
    {
        int commonDenominator = a.Denominator * b.Denominator;
        int newNumerator = a.Numerator * b.Denominator - b.Numerator * a.Denominator;
        int newWholePart = a.WholePart - b.WholePart;

        return new ProperFraction(newNumerator, commonDenominator, newWholePart);
    }

    public static ProperFraction operator *(ProperFraction a, ProperFraction b)
    {
        int newNumerator = (a.WholePart * a.Denominator + a.Numerator) * (b.WholePart * b.Denominator + b.Numerator);
        int newDenominator = a.Denominator * b.Denominator;

        return new ProperFraction(newNumerator, newDenominator);
    }

    public static ProperFraction operator /(ProperFraction a, ProperFraction b)
    {
        int newNumerator = (a.WholePart * a.Denominator + a.Numerator) * b.Denominator;
        int newDenominator = a.Denominator * (b.WholePart * b.Denominator + b.Numerator);

        return new ProperFraction(newNumerator, newDenominator);
    }

    public static ProperFraction operator +(ProperFraction a, int b)
    {
        return new ProperFraction(a.Numerator, a.Denominator, a.WholePart + b);
    }

    public static ProperFraction operator *(ProperFraction a, int b)
    {
        return new ProperFraction(a.Numerator * b, a.Denominator, a.WholePart * b);
    }

    // Операції порівняння
    public static bool operator ==(ProperFraction a, ProperFraction b)
    {
        return (a.WholePart == b.WholePart) &&
               (a.Numerator * b.Denominator == b.Numerator * a.Denominator);
    }

    public static bool operator !=(ProperFraction a, ProperFraction b)
    {
        return !(a == b);
    }

    public static bool operator >(ProperFraction a, ProperFraction b)
    {
        return (a.WholePart > b.WholePart) ||
               (a.WholePart == b.WholePart && a.Numerator * b.Denominator > b.Numerator * a.Denominator);
    }

    public static bool operator <(ProperFraction a, ProperFraction b)
    {
        return (a.WholePart < b.WholePart) ||
               (a.WholePart == b.WholePart && a.Numerator * b.Denominator < b.Numerator * a.Denominator);
    }

    public static bool operator >=(ProperFraction a, ProperFraction b)
    {
        return a == b || a > b;
    }

    public static bool operator <=(ProperFraction a, ProperFraction b)
    {
        return a == b || a < b;
    }

    // Перевантаження операцій true і false
    public static bool operator true(ProperFraction a)
    {
        return a.Numerator != 0 || a.WholePart != 0;
    }

    public static bool operator false(ProperFraction a)
    {
        return a.Numerator == 0 && a.WholePart == 0;
    }

    // Явне та неявне приведення типів
    public static explicit operator double(ProperFraction a)
    {
        return a.WholePart + (double)a.Numerator / a.Denominator;
    }

    public static explicit operator int(ProperFraction a)
    {
        return a.WholePart;
    }
}

// Головна функція
public class Program
{
    public static void Main(string[] args)
    {
        ProperFraction fraction1 = new ProperFraction(3, 4, 1);
        ProperFraction fraction2 = new ProperFraction(2, 5);

        Console.WriteLine("Fraction 1: " + fraction1);
        Console.WriteLine("Fraction 2: " + fraction2);

        ProperFraction sum = fraction1 + fraction2;
        ProperFraction difference = fraction1 - fraction2;
        ProperFraction product = fraction1 * fraction2;
        ProperFraction quotient = fraction1 / fraction2;

        Console.WriteLine("Sum: " + sum);
        Console.WriteLine("Difference: " + difference);
        Console.WriteLine("Product: " + product);
        Console.WriteLine("Quotient: " + quotient);

        ProperFraction addedInteger = fraction1 + 2;
        ProperFraction multipliedInteger = fraction1 * 3;

        Console.WriteLine("Fraction 1 + 2: " + addedInteger);
        Console.WriteLine("Fraction 1 * 3: " + multipliedInteger);

        Console.WriteLine("Fraction 1 > Fraction 2: " + (fraction1 > fraction2));
        Console.WriteLine("Fraction 1 < Fraction 2: " + (fraction1 < fraction2));
        Console.WriteLine("Fraction 1 == Fraction 2: " + (fraction1 == fraction2));
        Console.WriteLine("Fraction 1 != Fraction 2: " + (fraction1 != fraction2));

        double fraction1AsDouble = (double)fraction1;
        int fraction1AsInt = (int)fraction1;

        Console.WriteLine("Fraction 1 as double: " + fraction1AsDouble);
        Console.WriteLine("Fraction 1 as int: " + fraction1AsInt);
    }
}
