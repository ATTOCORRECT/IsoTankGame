using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrackVelocityDetector : MonoBehaviour
{
    float forwardSpeed;
    Vector3 lastPosition;
    Vector3 velocity;
    // Start is called before the first frame update
    void Start()
    {
        lastPosition = transform.position;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        velocity = transform.position - lastPosition;

        forwardSpeed = Vector3.Dot(velocity, transform.forward) / Time.fixedDeltaTime;

        lastPosition = transform.position;
    }

    public float getForwardSpeed()
    {
        return forwardSpeed; // in units per second
    }
}
