using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class FloorHeightDetector : MonoBehaviour
{


    public float GetHeight()
    {
        float height;

        Debug.DrawRay(transform.position, Vector3.down * 2, Color.red);

        RaycastHit hit;
        // Does the ray intersect any objects excluding the player layer
        if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.down), out hit, 2))
        {
            Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.down) * hit.distance, Color.red);
            height = hit.distance - 1;
        }
        else
        {
            height = 1;
        }
        return height;
    }
}
