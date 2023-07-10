using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyController : MonoBehaviour
{
    [SerializeField] private float patrolSpeed = 3f;
    [SerializeField] private float detectionRadius = 5f;
    [SerializeField] private float walkRadius = 15f;
    [SerializeField] private float height = 1;
    [SerializeField] private LayerMask playerLayer;

    private Transform myTransform;
    private BotAttack botAttack;
    private BotHealth health;

    private Transform player;
    private bool playerInRange;

    private Animator animator;
    private NavMeshAgent agent;

    private void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
        botAttack = GetComponent<BotAttack>();
        health = GetComponent<BotHealth>();
        myTransform = transform;
    }

    private void Start()
    {
        playerInRange = false;

        agent.speed = patrolSpeed;

        SetNewDestination();

        botAttack.OnKillPlayer.AddListener(ResetTarget);
    }

    private void ResetTarget()
    {
        Debug.Log("ResetTarget");
        player = null;
        playerInRange = false;
        agent.isStopped = false;
        SetNewDestination();
    }

    private void Update()
    {
        if (health.isDead) return;

        CheckForPlayer();

        if (!playerInRange && !agent.pathPending && agent.remainingDistance < 0.15f)
        {
            SetNewDestination();
        }
        
        if (playerInRange)
        {
            agent.isStopped = true;
            FacePlayer();
        }

        bool isWalking = agent.velocity.magnitude > 0.1f;
        animator.SetBool("isWalking", isWalking);
    }

    private void FacePlayer()
    {
        Vector3 direction = player.position - myTransform.position;
        direction.y = 0f;

        if (direction != Vector3.zero)
        {
            Quaternion targetRotation = Quaternion.LookRotation(direction);
            myTransform.rotation = Quaternion.Slerp(myTransform.rotation, targetRotation, Time.deltaTime * 15f);
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, walkRadius);
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, detectionRadius);
    }

    private void CheckForPlayer()
    {
        var circle = Physics.OverlapSphere(myTransform.position, detectionRadius, playerLayer);

        if (circle.Length > 0)
        {
            player = circle[0].transform;

            Vector3 positionWithHeight = new Vector3(myTransform.position.x, myTransform.position.y + height, myTransform.position.z);
            Vector3 playerPositionWithHeight = new Vector3(player.position.x, player.position.y + height, player.position.z);

            Vector3 direction = playerPositionWithHeight - positionWithHeight;
            Ray ray = new Ray(positionWithHeight, direction.normalized);
            RaycastHit hit;

            Debug.DrawRay(positionWithHeight, direction * detectionRadius, Color.red, 0.1f);

            playerInRange = Physics.Raycast(ray, out hit, detectionRadius, playerLayer);

            if (playerInRange)
            {
                Shoot();
            }
        }
    }

    private void Shoot()
    {
        botAttack.HandleShoot();
    }

    private void SetNewDestination()
    {
        Vector3 randomPoint = Random.insideUnitSphere * walkRadius;
        NavMesh.SamplePosition(randomPoint, out NavMeshHit hit, walkRadius, NavMesh.AllAreas);
        Debug.Log(hit.position);
        agent.SetDestination(hit.position);
    }
}
