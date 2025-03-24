using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class FloorHeightDetector : MonoBehaviour
{
    LayerMask layerMask;

    private void Start()
    {
        layerMask = LayerMask.GetMask("Ground");
    }


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
        if (Physics.Raycast(position, transform.TransformDirection(Vector3.down), out hit, 4, layerMask))
        {
            Debug.DrawRay(position, transform.TransformDirection(Vector3.down) * hit.distance, Color.red);
            height = hit.distance - 2;
        }
        else
        {
            height = 2;
            Debug.Log("NO GROUND!");
        }
        return height;
    }
}
