using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    Vector3 inputMoveDirection = Vector3.forward;
    float inputMoveSpeed;

    float speed = 8;
    float angularSpeed = 64;

    SecondOrderDynamics trackSpeed;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        UpdateInput();
    }

    private void FixedUpdate()
    {
        Vector3 Velocity = inputMoveDirection * speed * inputMoveSpeed;
        transform.position += Velocity * Time.fixedDeltaTime;
        transform.rotation = Quaternion.LookRotation(inputMoveDirection, Vector3.up);
    }

    void UpdateInput()
    {
        float inputLeft = 0;
        float inputRight = 0;

        inputLeft = Input.GetAxis("Left Track"); // For controller triggers
        inputRight = Input.GetAxis("Right Track");

        //if (Input.GetKey(KeyCode.W))
        //{
        //    //inputLeft = 1;
        //    //inputRight = 1;
        //}
        //if (Input.GetKey(KeyCode.S))
        //{

        //}
        //if (Input.GetKey(KeyCode.A))
        //{
        //    //inputLeft -= 1;
        //    inputLeft += 1;
        //}
        //if (Input.GetKey(KeyCode.D))
        //{
        //    inputRight += 1;
        //    //inputRight -= 1;
        //}

        Vector3 inputSpeed = new Vector3(Throttle(inputLeft, inputRight), Throttle(inputRight, inputLeft), 0);
        float inputSpeedLeft  = inputSpeed.x;
        float inputSpeedRight = inputSpeed.y;

        inputMoveSpeed = (inputSpeedLeft + inputSpeedRight) / 2;

        if (inputMoveSpeed > 0.5f)
        {
            speed = Mathf.Lerp(speed, 16, 0.4f * Time.deltaTime);
        }
        else
        {
            speed = Mathf.Lerp(speed, 8, 1.6f * Time.deltaTime);
        }

        //Debug.Log(speed);

        float inputTorque = inputSpeedLeft - inputSpeedRight;

        inputMoveDirection = (Quaternion.AngleAxis(inputTorque * angularSpeed * Time.deltaTime, Vector3.up) * inputMoveDirection).normalized;
    }

    float Throttle(float a, float b)
    {
        return a - b + (a * b);
    }
}
