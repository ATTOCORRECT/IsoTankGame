using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PixelPerfect3D : MonoBehaviour
{
    [SerializeField]
    Transform CameraMount;

    float pixelsPerUnit = 9;

    Matrix4x4 rotationMatrix = Matrix4x4.identity;
    Matrix4x4 scaleMatrix = Matrix4x4.identity;

    Matrix4x4 transformationMatrix = Matrix4x4.identity;

    // Start is called before the first frame update
    void Start()
    {
        Vector3 scale = new Vector3(1, 1 / Mathf.Cos(30 * Mathf.Deg2Rad) * 2, 2) * pixelsPerUnit;
        scaleMatrix.SetTRS(Vector3.zero, Quaternion.identity, scale);

        rotationMatrix.SetTRS(Vector3.zero, Quaternion.Euler(0, -45, 0), Vector3.one);
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 transformedCameraPosition = CameraMount.position;                                       // get position

        transformedCameraPosition = rotationMatrix.MultiplyPoint(transformedCameraPosition);            // rotate
        transformedCameraPosition = scaleMatrix.MultiplyPoint(transformedCameraPosition);               // scale
        transformedCameraPosition = roundVector(transformedCameraPosition);                             // round to nearest pixel
        transformedCameraPosition = scaleMatrix.inverse.MultiplyPoint(transformedCameraPosition);       // inverse scale
        transformedCameraPosition = rotationMatrix.inverse.MultiplyPoint(transformedCameraPosition);    // inverse rotate

        //transformedCameraPosition.y = 

        transform.position = transformedCameraPosition;                                                 // set position
    }

    Vector3 roundVector(Vector3 v)
    {
        return new Vector3(Mathf.Round(v.x), Mathf.Round(v.y), Mathf.Round(v.z));
    }
}
