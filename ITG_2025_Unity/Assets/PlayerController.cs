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
        

        //turnAngle = dynamics.update(new Vector3(turnAngle, 0, 0), Time.fixedDeltaTime).x;

        //  Debug.Log(turnAngle);

        


        if (inputMagnitude > 0.5f)
        {
            speed = Mathf.Lerp(speed, 16, 0.4f * Time.deltaTime);
        }
        else
        {
            speed = Mathf.Lerp(speed, 8, 1.6f * Time.deltaTime);
        }


        

        if (inputMagnitude > 0)
        {
            float turnAngle = Vector3.SignedAngle(moveDirection, targetMoveDirection, Vector3.up);

            turnAngle = Mathf.Clamp(turnAngle, -angularSpeed * Time.fixedDeltaTime, angularSpeed * Time.fixedDeltaTime);

            moveDirection = (Quaternion.AngleAxis(turnAngle, Vector3.up) * moveDirection).normalized;


            transform.rotation = Quaternion.LookRotation(moveDirection, Vector3.up);
        }

        Vector3 Velocity = moveDirection * inputMagnitude * speed;
        transform.position += Velocity * Time.fixedDeltaTime;

        Utils.drawDebugPoint(transform.position + targetMoveDirection * 5, Color.cyan);
    }

    void UpdateInput()
    {
        Vector3 inputDirection = Vector3.zero;

        inputDirection.x = Input.GetAxis("Horizontal");
        inputDirection.z = Input.GetAxis("Vertical");

        inputDirection = Vector3.ClampMagnitude(inputDirection, 1);

        inputMagnitude = inputDirection.magnitude;

        inputDirection = (Quaternion.AngleAxis(-45, Vector3.up) * inputDirection);

        targetMoveDirection = inputDirection;
    }

    float Throttle(float a, float b)
    {
        return a - b + (a * b);
    }
}
