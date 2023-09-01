using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class TankMovement : MonoBehaviour
{
    Rigidbody Rigidbody;
    public float movementSpeed = 2;
    float lVelocity;
    float rVelocity;

    // Start is called before the first frame update
    void Start()
    {
        Rigidbody = gameObject.GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        lVelocity = Input.GetAxis("LeftThrottle") * movementSpeed;
        rVelocity = Input.GetAxis("RightThrottle") * movementSpeed;

        if (lVelocity != 0) {
            Rigidbody.AddRelativeTorque(new Vector3(0, lVelocity,0));
            Rigidbody.AddRelativeForce(new Vector3(lVelocity, 0 ,0));
        }

        if (rVelocity != 0) {
            Rigidbody.AddRelativeTorque(new Vector3(0, -rVelocity,0));
            Rigidbody.AddRelativeForce(new Vector3(rVelocity, 0 ,0));
        }
    }

    public float LVelocity()
    {
        return lVelocity;
    }

    public float RVelocity()
    {
        return rVelocity;
    }


}
