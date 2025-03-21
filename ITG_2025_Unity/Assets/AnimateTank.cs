using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimateTank : MonoBehaviour
{

    [SerializeField]
    FloorHeightDetector frontLeft, frontRight, midLeft, midRight ,backLeft, backRight;

    [SerializeField]
    Transform Hull, leftTrack, rightTrack;

    float bodyWidth = 2.5f;
    float trackLength = 3.5f;

    SecondOrderDynamics height, aPiRo, aPiLPiR;

    // Start is called before the first frame update
    void Start()
    {
        height  = new SecondOrderDynamics(0.25f, 0.5f, 2, Vector3.zero);
        aPiRo   = new SecondOrderDynamics(0.25f, 0.5f, 2, Vector3.zero);
        aPiLPiR = new SecondOrderDynamics(0.25f, 0.5f, 2, Vector3.zero);
    }

    // Update is called once per frame
    void Update()
    {
        float heightFrontLeft, heightFrontRight, heightMidLeft, heightMidRight, heightBackLeft, heightBackRight;

        heightFrontLeft  = frontLeft.GetHeight();
        heightFrontRight = frontRight.GetHeight();
        heightMidLeft    = midLeft.GetHeight();
        heightMidRight   = midRight.GetHeight();
        heightBackLeft   = backLeft.GetHeight();
        heightBackRight  = backRight.GetHeight();

        float pitchLeft, pitchRight;

        pitchLeft = Mathf.Atan2(heightFrontLeft - heightBackLeft, bodyWidth) * Mathf.Rad2Deg;
        pitchRight = Mathf.Atan2(heightFrontRight - heightBackRight, bodyWidth) * Mathf.Rad2Deg;

        float heightLeft, heightRight;

        heightLeft = (heightFrontLeft + heightBackLeft) / 2;
        heightRight = (heightFrontRight + heightBackRight) / 2;

        if (heightMidLeft < heightLeft)
        {
            if (heightFrontLeft < heightBackLeft)
            {
                pitchLeft = Mathf.Atan2(heightFrontLeft - heightMidLeft, bodyWidth / 2) * Mathf.Rad2Deg;
            }
            else if (heightFrontLeft > heightBackLeft)
            {
                pitchLeft = Mathf.Atan2(heightMidLeft - heightBackLeft, bodyWidth / 2) * Mathf.Rad2Deg;
            }

            heightLeft = heightMidLeft;
        }

        if (heightMidRight < heightRight)
        {
            if (heightFrontRight < heightBackRight)
            {
                pitchRight = Mathf.Atan2(heightFrontRight - heightMidRight, bodyWidth / 2) * Mathf.Rad2Deg;
            }
            else if (heightFrontRight > heightBackRight)
            {
                pitchRight = Mathf.Atan2(heightMidRight - heightBackRight, bodyWidth / 2) * Mathf.Rad2Deg;
            }

            heightRight = heightMidRight;
        }

        float pitch = Mathf.LerpAngle(pitchLeft, pitchRight, 0.5f);

        float roll = Mathf.Atan2(heightLeft - heightRight, bodyWidth) * Mathf.Rad2Deg;

        Vector3 heightT = Vector3.down * (heightLeft + heightRight) / 2;

        Vector3 aPiRoOut = aPiRo.update(new Vector3(pitch, 0, roll));
        Hull.localRotation = Quaternion.Euler(aPiRoOut.x, 0, aPiRoOut.z);

        Vector3 aPiLPiROut = aPiLPiR.update(new Vector3(pitchLeft, pitchRight, 0));
        leftTrack.localRotation = Quaternion.Euler(aPiLPiROut.x - aPiRoOut.x, 0, 0);
        rightTrack.localRotation = Quaternion.Euler(aPiLPiROut.y - aPiRoOut.x, 0, 0);

        Hull.localPosition = height.update(heightT);
    }
}
