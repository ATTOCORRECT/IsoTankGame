using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TiltDynamics : MonoBehaviour
{
    public Transform TiltDynamicsTargetTransform;
    void FixedUpdate()
    {
        
        Vector3 targetVector = TiltDynamicsTargetTransform.position - transform.position;

        float x = -(Vector3.Angle(transform.forward, targetVector) - 90);
        float z = Vector3.Angle(transform.right, targetVector) - 90;

        transform.localEulerAngles = new Vector3(Mathf.LerpAngle(transform.localEulerAngles.x, x, 0.8f),
                                                 transform.localEulerAngles.y, 
                                                 Mathf.LerpAngle(transform.localEulerAngles.z, z, 0.8f));
    }
}
