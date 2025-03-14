using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PixelSnapping : MonoBehaviour
{
    Transform cameraTransform;
    public float pixelsPerUnit;

    void Start()
    {
        cameraTransform = GameObject.Find("Main Camera").transform;
    }

    void Update()
    {
        Vector3 cameraTransformedposition = cameraTransform.InverseTransformVector(cameraTransform.position);

        Vector3 TransformedPosition = new Vector3(mod(cameraTransformedposition.x, 1 / pixelsPerUnit),
                                                  mod(cameraTransformedposition.y, 1 / pixelsPerUnit),
                                                  mod(cameraTransformedposition.z, 1 / pixelsPerUnit));

        Vector3 Position = cameraTransform.TransformVector(TransformedPosition);
        //Debug.Log(transformedposition);
        transform.position = Position;
    } 
    float Round(float n, float m)
    {
        return Mathf.Floor(n / m + 0.5f) * m;
    }

    float mod(float x, float m) 
    {
        float r = x%m;
        return r<0 ? r+m : r;
    }
}



