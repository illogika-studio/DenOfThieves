using System.Drawing;
using UnityEngine;

public class Coordinates
{
    // Z would be X in 2D
    public int z { get; }
    
    // X would be Y in 2D
    public int x { get; }

    public Coordinates()
    { 
        z = 0;
        x = 0;
    }
    
    public Coordinates(int xPoint, int zPoint)
    {
        x = xPoint;
        z = zPoint;
    }
    
    public Coordinates(float xPoint, float zPoint)
    {
        x = (int)xPoint;
        z = (int)zPoint;
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

        return z == c.z && x == c.x;
    }
}
