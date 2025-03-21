using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimateTracks : MonoBehaviour
{
    [SerializeField]
    Material LeftTankTreadMaterial, RightTankTreadMaterial;

    [SerializeField]
    TrackVelocityDetector leftVelocitySensor, rightVelocitySensor;

    float rightTankTreadFrame = 0;
    float leftTankTreadFrame = 0;
    float oldRightTankTreadFrame = 0;
    float oldLeftTankTreadFrame = 0;

    float frameCount = 40;
    // Start is called before the first frame update
    void Start()
    {
        RightTankTreadMaterial.mainTextureOffset = (new Vector2(0, 0));
        LeftTankTreadMaterial.mainTextureOffset = (new Vector2(0, 0));
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        oldRightTankTreadFrame = rightTankTreadFrame;
        oldLeftTankTreadFrame = leftTankTreadFrame;

        rightTankTreadFrame = mod((rightTankTreadFrame - rightVelocitySensor.getForwardSpeed() / 4), frameCount);
        leftTankTreadFrame = mod((leftTankTreadFrame - leftVelocitySensor.getForwardSpeed() / 4), frameCount);


    }

    void Update()
    {
        float delta = (Time.time / Time.fixedDeltaTime) % 1;
        RightTankTreadMaterial.mainTextureOffset = (new Vector2(0, MathF.Floor(Mathf.Lerp(oldRightTankTreadFrame, rightTankTreadFrame, delta)) / frameCount));
        LeftTankTreadMaterial.mainTextureOffset = (new Vector2(0, MathF.Floor(Mathf.Lerp(oldLeftTankTreadFrame, leftTankTreadFrame, delta)) / frameCount));
    }

    float mod(float x, float m)
    {
        float r = x % m;
        return r < 0 ? r + m : r;
    }
}
