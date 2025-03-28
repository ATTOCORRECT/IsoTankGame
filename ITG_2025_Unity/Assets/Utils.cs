using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Utils
{
    public static float mod(float x, float m)
    {
        float r = x % m;
        return r < 0 ? r + m : r;
    }

    public static void drawDebugPoint(Vector3 position, Color color)
    {
        Debug.DrawRay(position - Vector3.right      , 2 * Vector3.right     );
        Debug.DrawRay(position - Vector3.up         , 2 * Vector3.up        );
        Debug.DrawRay(position - Vector3.forward    , 2 * Vector3.forward   );
    }
}
