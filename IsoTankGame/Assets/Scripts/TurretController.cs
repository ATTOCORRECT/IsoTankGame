using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretController : MonoBehaviour
{
    public GameObject Turret;
    public GameObject Gun;
    public GameObject Muzzle;
    public GameObject Target;

    public float projectileVelocity;
    public float rotationSpeed;

    LineRenderer lineRenderer;

    Vector3 muzzlePosition;

    float turretAngle = 0;
    float gunPitch = 5 * Mathf.Deg2Rad;
    float gravity = -9.8f;
    
    void Start()
    {
        lineRenderer = GetComponent<LineRenderer>();
    }

    void Update()
    {
        muzzlePosition = Muzzle.transform.position - transform.position;

        turretAngle = (turretAngle - Input.GetAxis("Horizontal") * rotationSpeed * Mathf.Deg2Rad) % (2 * Mathf.PI);
        gunPitch = Mathf.Clamp(gunPitch + Input.GetAxis("Vertical") * rotationSpeed * Mathf.Deg2Rad, -20 * Mathf.Deg2Rad, 30 * Mathf.Deg2Rad);;

        float finalTime = ProjectileTimeAtGround();
        float targetRadius = ProjectileTimetoDistance(finalTime);

        UpdateLine(finalTime);

        Turret.transform.eulerAngles = new Vector3 (0, -turretAngle * Mathf.Rad2Deg, 0);
        Gun.transform.eulerAngles = new Vector3 (0, Gun.transform.eulerAngles.y, gunPitch * Mathf.Rad2Deg);

        Target.transform.position = new Vector3 (targetRadius * Mathf.Cos(turretAngle), 0, targetRadius * Mathf.Sin(turretAngle)) + transform.position;
    }

    float ProjectileDistancetoTime(float distance)
    {
        float initialRadius = Mathf.Sqrt(muzzlePosition.x * muzzlePosition.x + muzzlePosition.z * muzzlePosition.z);
        float time = (distance - initialRadius) / (projectileVelocity * Mathf.Cos(gunPitch));
        return time;
    }

    float ProjectileTimetoDistance(float time)
    {
        float initialRadius = Mathf.Sqrt(muzzlePosition.x * muzzlePosition.x + muzzlePosition.z * muzzlePosition.z);
        float distance = time * projectileVelocity * Mathf.Cos(gunPitch) + initialRadius;
        return distance;
    }
    
    float ProjectileTimeAtGround()
    {
        float a = gravity / 2;
        float b = projectileVelocity * Mathf.Sin(gunPitch);
        float c = muzzlePosition.y;

        float time = (-b - Mathf.Sqrt((b * b) - (4 * a * c))) / (2 * a); // quadratic formula

        return time;
    }

    float ProjectileTimeToHeight(float time)
    {
        float a = gravity / 2;
        float b = projectileVelocity * Mathf.Sin(gunPitch);
        float c = muzzlePosition.y;
        float height = a * time * time + b * time + c;
        return height;
    }

    void UpdateLine(float finalTime)
    {
        float timeStep = finalTime / (lineRenderer.positionCount - 1);
        for (int i = 0; i < lineRenderer.positionCount; i++)
        {
            float radius = ProjectileTimetoDistance(timeStep * i);
            float height = ProjectileTimeToHeight(timeStep * i);
            Vector3 pointPosition = new Vector3(radius * Mathf.Cos(turretAngle), height, radius * Mathf.Sin(turretAngle));

            lineRenderer.SetPosition(i, pointPosition + transform.position);
        }
    }
}
