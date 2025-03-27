using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DollyFollow : MonoBehaviour
{
    [SerializeField]
    Transform target;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 newPosition = new Vector3(target.position.x, 0, target.position.z);
        transform.position = newPosition;
    }
}
