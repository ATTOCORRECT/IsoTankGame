using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PixelPerfect3D : MonoBehaviour
{
    [SerializeField]
    Transform CameraMount;

    [SerializeField]
    float pixelsPerUnit = 18;

    void LateUpdate()
    {
        Vector3 globalInLocalPosition = CameraMount.InverseTransformPoint(Vector3.zero);
        Vector3 offset = new Vector3(Utils.mod(globalInLocalPosition.x, 1 / pixelsPerUnit),
                                     Utils.mod(globalInLocalPosition.y, 1 / pixelsPerUnit),
                                     transform.localPosition.z);

        transform.localPosition = offset;                                                 // set position
    }
}
