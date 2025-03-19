using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimateTank : MonoBehaviour
{

    [SerializeField]
    FloorHeightDetector frontLeft, frontRight, backLeft, BackRight;

    [SerializeField]
    Transform Hull, leftTrack, rightTrack;

    float bodyWidth = 2.5f;
    float trackLength = 3.5f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        float heightFrontLeft, heightFrontRight, heightBackLeft, heightBackRight;

        heightFrontLeft  = frontLeft.GetHeight();
        heightFrontRight = frontRight.GetHeight();
        heightBackLeft   = backLeft.GetHeight();
        heightBackRight  = BackRight.GetHeight();

        float heightLeft, heightRight;

        heightLeft = (heightFrontLeft + heightBackLeft) / 2;
        heightRight = (heightFrontRight + heightBackRight) / 2;

        float roll = Mathf.Atan2(heightLeft - heightRight, bodyWidth) * Mathf.Rad2Deg;

        Hull.localRotation = Quaternion.Euler(0, 0, roll);

        Hull.localPosition = Vector3.down * (heightFrontLeft + heightFrontRight + heightBackLeft + heightBackRight) / 4;
    }
}
