#nullable disable

using System;

namespace GameServer.Models;

public readonly struct Angle : IEquatable<Angle>
{
    private readonly int _numerator;
    private readonly int _denominator;

    public Angle(int numerator, int denominator)
    {
        if (denominator == 0)
        {
            throw new ArgumentException("Denominator cannot be zero", nameof(denominator));
        }

        // Normalize to canonical form
        var gcd = GCD(Math.Abs(numerator), Math.Abs(denominator));
        _numerator = numerator / gcd;
        _denominator = denominator / gcd;

        // Ensure denominator is positive
        if (_denominator < 0)
        {
            _numerator = -_numerator;
            _denominator = -_denominator;
        }
    }

    public int Numerator => _numerator;
    public int Denominator => _denominator;

    public double Degrees => (double)_numerator / _denominator * 360;

    public static Angle operator +(Angle left, Angle right)
    {
        var newNumerator = left._numerator * right._denominator + right._numerator * left._denominator;
        var newDenominator = left._denominator * right._denominator;
        return new Angle(newNumerator, newDenominator);
    }

    public static Angle operator -(Angle left, Angle right)
    {
        var newNumerator = left._numerator * right._denominator - right._numerator * left._denominator;
        var newDenominator = left._denominator * right._denominator;
        return new Angle(newNumerator, newDenominator);
    }

    public static bool operator ==(Angle left, Angle right)
    {
        return left.Equals(right);
    }

    public static bool operator !=(Angle left, Angle right)
    {
        return !left.Equals(right);
    }

    public bool Equals(Angle other)
    {
        return _numerator == other._numerator && _denominator == other._denominator;
    }

    public override bool Equals(object obj)
    {
        return obj is Angle other && Equals(other);
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(_numerator, _denominator);
    }

    public double Sin()
    {
        return Math.Sin(Degrees * Math.PI / 180);
    }

    public double Cos()
    {
        return Math.Cos(Degrees * Math.PI / 180);
    }

    private static int GCD(int a, int b)
    {
        while (b != 0)
        {
            var temp = b;
            b = a % b;
            a = temp;
        }
        return a;
    }

    public override string ToString()
    {
        return $"{_numerator}/{_denominator}π";
    }
}
