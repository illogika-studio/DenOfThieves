using UnityEngine;

public static class UtilsClass
{
    public static Vector3 RoundVector3(Vector3 v)
    {
        var x = Mathf.Round(v.x);
        var z = Mathf.Round(v.z);

        return new Vector3(x, v.y, z);
    }
    
    public static Vector3 GetVectorFromAngle(float angle)
    {
        float angleRad = angle * (Mathf.PI / 180f);
        var a = new Vector3(Mathf.Cos(angleRad),0,Mathf.Sin(angleRad));
        return a;
    }

    public static float GetAngleFromVectorFloat(Vector3 dir)
    {
        dir = dir.normalized;
        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        
        if (angle < 0)
        {
            angle += 360;
        }

        return angle;
    }
}
