using System;
using System.Drawing;
using UnityEngine;

[Serializable]
public class Coordinates
{
    [SerializeField] private int _z;
    [SerializeField] private int _x;

    // Z would be X in 2D
    public int Z => _z;

    // X would be Y in 2D
    public int X => _x;

    public Coordinates()
    {
        _z = 0;
        _x = 0;
    }
    
    public Coordinates(int xPoint, int zPoint)
    {
        _x = xPoint;
        _z = zPoint;
    }
    
    public Coordinates(float xPoint, float zPoint)
    {
        _x = (int)xPoint;
        _z = (int)zPoint;
    }

    public static bool operator ==(Coordinates lhs, Coordinates rhs)
    {
        if (lhs is null)
        {
            if (rhs is null)
            {
                return true;
            }

            return false;
        }

        return lhs.Equals(rhs);
    }

    public static bool operator !=(Coordinates lhs, Coordinates rhs) => !(lhs == rhs);

    public override bool Equals(object obj) => this.Equals(obj as Coordinates);
    
    public bool Equals(Coordinates c)
    {
        if (c is null)
        {
            return false;
        }

        if (ReferenceEquals(this, c))
        {
            return true;
        }

        if (this.GetType() != c.GetType())
        {
            return false;
        }

        return Z == c.Z && X == c.X;
    }

    public static Coordinates operator +(Coordinates lhs, Coordinates rhs)
    {
        if (lhs is null)
        {
            if (rhs is null)
            {
                return new Coordinates();
            }

            return rhs;
        }
        else
        {
            if (rhs is null)
                return lhs;
        }

        return new Coordinates(lhs.X + rhs.X, lhs.Z + rhs.Z);
    }
}
