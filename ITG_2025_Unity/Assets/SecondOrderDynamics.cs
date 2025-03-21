using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SecondOrderDynamics
{
    Vector3 xp;
    Vector3 y, yd;
    float k1, k2, k3;

    public float f, z, r;

    public SecondOrderDynamics(float frequency, float damping, float reaction, Vector3 target)
    {
        f = frequency;// speed of the system
        z = damping;  // 'springy-ness' of the system, < 1 underdamped, > 1 over damped
        r = reaction; // initial response of the system, < 0 negative response, > 0 positive response

        Vector3 x0 = target;
        //compute constants

        // initialize variables
        xp = x0;
        y = x0;
        yd = Vector3.zero;
    }

    public Vector3 update(Vector3 target)
    {
        //black magic
        k1 = z / (Mathf.PI * f);
        k2 = 1 / ((2 * Mathf.PI * f) * (2 * Mathf.PI * f));
        k3 = r * z / (2 * Mathf.PI * f);

        float T = Time.fixedDeltaTime;
        Vector3 x = target;
        Vector3 xd = (x - xp) / T;
        xp = x;

        float k2Stable = Mathf.Max(k2, T * T / 2 + T * k1 / 2, T * k1);
        y = y + T * yd;
        yd = yd + T * (x + k3 * xd - y - k1 * yd) / k2;

        if (Vector3.Magnitude(y - x) < 0.01)
        {
            y = x;
        }

        return y;
    }
}
