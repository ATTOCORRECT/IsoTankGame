using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    Vector3 targetMoveDirection = Vector3.zero;
    Vector3 moveDirection = Vector3.forward;
    float inputMagnitude = 0;


    float speed = 8;
    float angularSpeed = 192;

    SecondOrderDynamics dynamics;

    // Start is called before the first frame update
    void Start()
    {
        dynamics = new SecondOrderDynamics(3f, 1f, 0, Vector3.zero);
    }

    // Update is called once per frame  
    void Update()
    {
        UpdateInput();
    }

    private void FixedUpdate()
    {
        float turnAngle = Vector3.SignedAngle(moveDirection, targetMoveDirection, Vector3.up);


        turnAngle = Mathf.Clamp(turnAngle, -angularSpeed * Time.fixedDeltaTime, angularSpeed * Time.fixedDeltaTime);

        //turnAngle = dynamics.update(new Vector3(turnAngle, 0, 0), Time.fixedDeltaTime).x;

        //  Debug.Log(turnAngle);

        moveDirection = (Quaternion.AngleAxis(turnAngle, Vector3.up) * moveDirection).normalized;


        if (inputMagnitude > 0.5f)
        {
            speed = Mathf.Lerp(speed, 16, 0.4f * Time.deltaTime);
        }
        else
        {
            speed = Mathf.Lerp(speed, 8, 1.6f * Time.deltaTime);
        }


        Vector3 Velocity = moveDirection * inputMagnitude * speed;
        transform.position += Velocity * Time.fixedDeltaTime;
        transform.rotation = Quaternion.LookRotation(moveDirection, Vector3.up);
    }

    void UpdateInput()
    {
        Vector3 inputDirection = Vector3.zero;

        inputDirection.x = Input.GetAxis("Horizontal");
        inputDirection.z = Input.GetAxis("Vertical");

        inputDirection.Normalize();

        inputMagnitude = inputDirection.magnitude;

        if (inputMagnitude == 0)
        {
            return;
        }

        inputDirection = (Quaternion.AngleAxis(-45, Vector3.up) * inputDirection).normalized;

        targetMoveDirection = inputDirection;
    }

    float Throttle(float a, float b)
    {
        return a - b + (a * b);
    }
}
