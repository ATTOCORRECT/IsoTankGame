using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DollyFollowTank : MonoBehaviour
{
    public GameObject Tank;
    public float snap = 0.5f;

    void Start()
    {
        
    }

    void FixedUpdate()
    {
        Vector2 position = new Vector2 (transform.position.x, transform.position.z);
        Vector2 tankPosition = new Vector2 (Tank.transform.position.x, Tank.transform.position.z);

        Vector2 newPosition = Vector2.Lerp(position, tankPosition, snap);

        transform.position = new Vector3(newPosition.x, transform.position.y, newPosition.y);
    }
}
