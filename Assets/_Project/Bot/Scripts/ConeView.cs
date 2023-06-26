using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConeView : MonoBehaviour
{
    [SerializeField] private float viewRange = 10f;
    [SerializeField] private float attackRange = 7f;
    [SerializeField] private float viewAngle = 45f;
    [SerializeField] private Vector3 offset;
    [SerializeField] private LayerMask targetLayer;

    [Space]
    public Transform target;

    public ConeViewState currentState;

    private Transform myTransform;

    private void Awake()
    {
        myTransform = transform;
    }

    private void Update()
    {
        if(target != null)
        {
            var distance = Vector3.Distance(target.position, myTransform.position);
            if (distance > viewRange)
            {
                currentState = ConeViewState.noView;
                target = null;
            }
            else if (distance > attackRange)
            {
                currentState = ConeViewState.onView;
            }
            else if(distance <= attackRange) 
            {
                currentState = ConeViewState.onRange;
            }
            else
            {
                currentState = ConeViewState.noView;
            }
        }

        CheckView();
    }

    private void CheckView()
    {
        Collider[] targets = Physics.OverlapSphere(myTransform.position, viewRange, targetLayer);

        foreach (Collider target in targets)
        {
            if(Vector3.Distance(myTransform.position, target.transform.position) < viewRange / 2)
            {
                this.target = target.transform;
                return;
            }

            Vector3 directionToTarget = target.transform.position - (myTransform.position + offset);
            float angleToTarget = Vector3.Angle(myTransform.forward, directionToTarget);

            if (angleToTarget <= viewAngle / 2f)
            {
                this.target = target.transform;
                return;
            }
        }

        currentState = ConeViewState.noView;
        target = null;
    }

    private void OnDrawGizmosSelected()
    {
        if (myTransform == null)
            myTransform = transform;

        // Draw the cone view in the scene view for visualization purposes
        Vector3 leftConeDirection = Quaternion.Euler(0f, -viewAngle / 2f, 0f) * myTransform.forward;
        Vector3 rightConeDirection = Quaternion.Euler(0f, viewAngle / 2f, 0f) * myTransform.forward;

        Gizmos.color = Color.yellow;
        Gizmos.DrawRay(myTransform.position + offset, myTransform.forward * viewRange);
        Gizmos.DrawRay(myTransform.position + offset, leftConeDirection * viewRange);
        Gizmos.DrawRay(myTransform.position + offset, rightConeDirection * viewRange);

        Gizmos.DrawRay(myTransform.position + offset, leftConeDirection.normalized * viewRange);
        Gizmos.DrawRay(myTransform.position + offset, rightConeDirection.normalized * viewRange);

        Gizmos.DrawRay(myTransform.position + offset, (leftConeDirection + rightConeDirection).normalized * viewRange);

        Gizmos.DrawWireSphere(myTransform.position + offset, viewRange / 2);
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(myTransform.position + offset, attackRange);
    }
}

public enum ConeViewState
{
    noView,
    onView,
    onRange
}
