using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimateTracks : MonoBehaviour
{
    Material LeftTankTreadMaterial;
    Material RightTankTreadMaterial;
    TankMovement tankMovement;
    float rightTankTreadFrame = 0;
    float leftTankTreadFrame = 0;
    float oldRightTankTreadFrame = 0;
    float oldLeftTankTreadFrame = 0;
    // Start is called before the first frame update
    void Start()
    {
        GameObject Tank = gameObject;
        tankMovement = Tank.GetComponent<TankMovement>();

        RightTankTreadMaterial.mainTextureOffset = (new Vector2(0,0));
        LeftTankTreadMaterial.mainTextureOffset  = (new Vector2(0,0));
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        oldRightTankTreadFrame = rightTankTreadFrame;
        oldLeftTankTreadFrame  = leftTankTreadFrame;

        rightTankTreadFrame = mod((rightTankTreadFrame - tankMovement.RVelocity() / 10), 20);
        leftTankTreadFrame  = mod((leftTankTreadFrame  - tankMovement.LVelocity() / 10), 20);

      
    }

    void Update()
    {
        float delta = (Time.time / Time.fixedDeltaTime) % 1;
        RightTankTreadMaterial.mainTextureOffset = (new Vector2(0, MathF.Floor(Mathf.Lerp(oldRightTankTreadFrame, rightTankTreadFrame, delta)) / 20f));
        LeftTankTreadMaterial.mainTextureOffset  = (new Vector2(0, MathF.Floor(Mathf.Lerp(oldLeftTankTreadFrame, leftTankTreadFrame, delta)) / 20f));
    }

    float mod(float x, float m) {
    float r = x%m;
    return r<0 ? r+m : r;
}
}
