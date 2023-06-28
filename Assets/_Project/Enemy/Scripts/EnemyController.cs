using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyController : MonoBehaviour
{
    [SerializeField] private float patrolSpeed = 3f;
    [SerializeField] private float detectionRadius = 5f;
    [SerializeField] private float walkRadius = 15f;
    [SerializeField] private LayerMask playerLayer;

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
        player = null;
        playerInRange = false;
        SetNewDestination();
    }

    private void Update()
    {
        if (health.isDead) return;

        CheckForPlayer();

        if (!playerInRange && !agent.pathPending && agent.remainingDistance < 0.5f)
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
        Vector3 direction = player.position - transform.position;
        direction.y = 0f;

        if (direction != Vector3.zero)
        {
            Quaternion targetRotation = Quaternion.LookRotation(direction);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * 5f);
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
        var circle = Physics.OverlapSphere(transform.position, detectionRadius, playerLayer);

        playerInRange = circle.Length > 0;

        if (playerInRange)
        {
            player = circle[0].transform;

            Shoot();
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

        agent.SetDestination(hit.position);
    }
}
