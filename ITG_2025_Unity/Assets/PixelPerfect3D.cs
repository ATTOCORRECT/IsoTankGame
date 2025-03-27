using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PixelPerfect3D : MonoBehaviour
{
    [SerializeField]
    Transform CameraMount;

    float pixelsPerUnit = 18;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 targetPosition = CameraMount.position;
        Vector3 snappedCameraPosition = roundVector(Quaternion.AngleAxis(45, Vector3.up) * targetPosition * pixelsPerUnit);
        snappedCameraPosition = Quaternion.AngleAxis(-45, Vector3.up) * snappedCameraPosition / pixelsPerUnit;
        transform.position = snappedCameraPosition;
    }

    Vector3 roundVector(Vector3 v)
    {
        return new Vector3(Mathf.Round(v.x), Mathf.Round(v.y), Mathf.Round(v.z));
    }
}
