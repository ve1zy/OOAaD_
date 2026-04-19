namespace GameServer.Models;

public struct Angle
{
    public static int CommonDenominator { get; set; } = 360;

    private readonly int _numerator;

    public Angle(int numerator)
    {
        _numerator = numerator;
    }

    public int Numerator => _numerator;

    public static Angle operator +(Angle a, Angle b)
    {
        var sum = a._numerator + b._numerator;
        var normalized = Normalize(sum);
        return new Angle(normalized);
    }

    public static bool operator ==(Angle a, Angle b)
    {
        return Normalize(a._numerator) == Normalize(b._numerator);
    }

    public static bool operator !=(Angle a, Angle b)
    {
        return !(a == b);
    }

    public override bool Equals(object obj)
    {
        if (obj is Angle other)
        {
            return this == other;
        }
        return false;
    }

    public override int GetHashCode()
    {
        return Normalize(_numerator).GetHashCode();
    }

    private static int Normalize(int numerator)
    {
        var result = numerator % CommonDenominator;
        if (result < 0)
        {
            result += CommonDenominator;
        }
        return result;
    }

    public double ToRadians()
    {
        return (Normalize(_numerator) * Math.PI * 2) / CommonDenominator;
    }
}

public static class AngleMath
{
    public static double Sin(Angle angle)
    {
        return Math.Sin(angle.ToRadians());
    }

    public static double Cos(Angle angle)
    {
        return Math.Cos(angle.ToRadians());
    }
}
