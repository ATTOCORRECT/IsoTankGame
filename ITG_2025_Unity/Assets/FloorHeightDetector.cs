using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class FloorHeightDetector : MonoBehaviour
{


    public float GetHeight()
    {
        return (getDistanceToGroundFromPoint(transform.position + transform.forward / 2) +
                getDistanceToGroundFromPoint(transform.position - transform.forward / 2) +
                getDistanceToGroundFromPoint(transform.position + transform.right   / 2) +
                getDistanceToGroundFromPoint(transform.position - transform.right   / 2)) / 4;
    }

    float getDistanceToGroundFromPoint(Vector3 position)
    {
        float height;

        Debug.DrawRay(position, Vector3.down * 2, Color.red);

        RaycastHit hit;
        // Does the ray intersect any objects excluding the player layer
        if (Physics.Raycast(position, transform.TransformDirection(Vector3.down), out hit, 2))
        {
            Debug.DrawRay(position, transform.TransformDirection(Vector3.down) * hit.distance, Color.red);
            height = hit.distance - 1;
        }
        else
        {
            height = 1;
        }
        return height;
    }
}
