using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    Vector3 inputMoveDirection = Vector3.forward;
    float inputMoveSpeed;

    float speed = 5;
    float angularSpeed = 64;

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
        float inputLeft = Input.GetAxis("Left Track");
        float inputRight = Input.GetAxis("Right Track");

        float inputSpeedLeft  = Throttle(inputLeft, inputRight);
        float inputSpeedRight = Throttle(inputRight, inputLeft);

        inputMoveSpeed = (inputSpeedLeft + inputSpeedRight) / 2;
        float inputTorque = inputSpeedLeft - inputSpeedRight;

        inputMoveDirection = (Quaternion.AngleAxis(inputTorque * angularSpeed * Time.deltaTime, Vector3.up) * inputMoveDirection).normalized;
    }

    float Throttle(float a, float b)
    {
        return a - b + (a * b);
    }
}
