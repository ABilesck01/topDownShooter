using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UIElements;

public class BotMovement : MonoBehaviour
{
    [SerializeField] private float walkPointRange;
    [SerializeField] private LayerMask groundLayer;

    private Vector3 walkPoint;
    private bool walkPointSet;

    private NavMeshAgent agent;
    private BotController controller;
    private Transform myTransform;

    public Transform target;

    private void Awake()
    {
        myTransform = transform;
        controller = GetComponent<BotController>();
        agent = GetComponent<NavMeshAgent>();
    }

    private void Update()
    {
        Patrol();
        ChaseTransform();
    }

    public void StopAgent()
    {
        agent.SetDestination(myTransform.position);
    }

    private void ChaseTransform()
    {
        if(controller.botState != BotState.follow || target == null) return;

        agent.SetDestination(transform.position);
    }

    private void Patrol()
    {
        if (controller.botState != BotState.patrol)
            return;

        if (!walkPointSet)
        {
            SetWalkPoint();
            return;
        }

        agent.SetDestination(walkPoint);

        float distanceToPoint = (myTransform.position - walkPoint).magnitude;

        if(distanceToPoint < 1f) 
        {
            walkPointSet = false;
        }
    }

    private void SetWalkPoint()
    {
        float x = Random.Range(-walkPointRange, walkPointRange);
        float z = Random.Range(-walkPointRange, walkPointRange);

        walkPoint = new Vector3(myTransform.position.x + x, myTransform.position.y, myTransform.position.z + z);

        if(Physics.Raycast(walkPoint, -myTransform.up, 2f, groundLayer))
        {
            walkPointSet = true;
        }
    }
}
