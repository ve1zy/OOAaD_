namespace GameServer.Models;

public class Vector
{
    private readonly int[] _components;

    public Vector(params int[] components)
    {
        if (components == null || components.Length == 0)
        {
            throw new ArgumentException("Vector must have at least one component.");
        }

        _components = components;
    }

    public int Dimensions => _components.Length;

    public int this[int index]
    {
        get
        {
            if (index < 0 || index >= _components.Length)
            {
                throw new IndexOutOfRangeException();
            }
            return _components[index];
        }
    }

    public static Vector operator +(Vector a, Vector b)
    {
        if (a.Dimensions != b.Dimensions)
        {
            throw new ArgumentException("Vectors must have the same number of dimensions.");
        }

        var result = new int[a.Dimensions];
        for (int i = 0; i < a.Dimensions; i++)
        {
            result[i] = a[i] + b[i];
        }

        return new Vector(result);
    }

    public override bool Equals(object obj)
    {
        if (obj is not Vector other)
        {
            return false;
        }

        if (Dimensions != other.Dimensions)
        {
            return false;
        }

        for (int i = 0; i < Dimensions; i++)
        {
            if (this[i] != other[i])
            {
                return false;
            }
        }

        return true;
    }

    public static bool operator ==(Vector a, Vector b)
    {
        if (ReferenceEquals(a, null))
        {
            return ReferenceEquals(b, null);
        }

        return a.Equals(b);
    }

    public static bool operator !=(Vector a, Vector b)
    {
        return !(a == b);
    }

    public override int GetHashCode()
    {
        int hash = 17;
        foreach (var component in _components)
        {
            hash = hash * 31 + component;
        }
        return hash;
    }

    public override string ToString()
    {
        return $"({string.Join(", ", _components)})";
    }
}
