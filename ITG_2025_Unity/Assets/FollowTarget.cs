using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class FollowTarget : MonoBehaviour
{
    [SerializeField]
    private Transform targetTransform;
    private NavMeshAgent agent;

    private Vector3 lastDestination = Vector3.zero;

    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
    }

    // Update is called once per frame
    void Update()
    {
        if (targetTransform.position == lastDestination)
        {
            return;
        }

        agent.SetDestination(targetTransform.position);

        lastDestination = targetTransform.position;
    }
}
