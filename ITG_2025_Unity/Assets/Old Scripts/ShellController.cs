using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShellController : MonoBehaviour
{
    Vector3 oldPosition;
    Vector3 position;
    // Start is called before the first frame update
    void Start()
    {
        position = transform.position;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        oldPosition = position;
        position = transform.position;

        Vector3 direction = position - oldPosition;
        if (direction.magnitude > 0)
        {
            transform.eulerAngles = new Vector3(0, transform.eulerAngles.y, Mathf.Atan2(direction.y,Mathf.Sqrt(direction.x * direction.x + direction.z * direction.z)) * Mathf.Rad2Deg);
        }

        if (position.y < 0) {
            Destroy(gameObject, 0.1f);
        }
    }
}
