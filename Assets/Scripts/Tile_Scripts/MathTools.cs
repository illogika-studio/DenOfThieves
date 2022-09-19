using UnityEngine;

public static class MathTools
{
    public static Vector3 RoundVector3(Vector3 v)
    {
        var x = Mathf.Round(v.x);
        var z = Mathf.Round(v.z);

        return new Vector3(x, v.y, z);
    }
}
