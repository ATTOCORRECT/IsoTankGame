using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReticleMovement : MonoBehaviour
{
    Vector3 reticlePosition;
    Vector3 reticlePositionOld;
    float angle = 0;
    float radius = 1;

    public float speed = 0.001f;
    void Start()
    {
        reticlePosition = transform.localPosition;
    }

    void FixedUpdate()
    {
        reticlePositionOld = transform.localPosition;

        angle = (angle - Input.GetAxis("Horizontal") * speed / 3) % (2 * Mathf.PI);
        radius = Mathf.Clamp(radius + Input.GetAxis("Vertical") * speed, 2.5f, 8);

        reticlePosition = new Vector3(Mathf.Cos(angle) * radius, reticlePosition.y, Mathf.Sin(angle) * radius);
    }

    void Update()
    {
        SmoothPosition();
    }

    void SmoothPosition(){
        float delta = (Time.time / Time.fixedDeltaTime) % 1;
        Vector3 positionNew = Vector3.Lerp(reticlePositionOld,reticlePosition,delta);
        transform.localPosition = new Vector3(positionNew.x, transform.localPosition.y, positionNew.z);
    }
}
