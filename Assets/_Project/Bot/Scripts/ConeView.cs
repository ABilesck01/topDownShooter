using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConeView : MonoBehaviour
{
    [SerializeField] private float viewRange = 10f;
    [SerializeField] private float viewAngle = 45f;
    [SerializeField] private Vector3 offset;
    [SerializeField] private LayerMask targetLayer;

    [Space]
    public Transform target;

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
                target = null;
            }
        }

        

        CheckView();
    }

    private void CheckView()
    {
        Collider[] targets = Physics.OverlapSphere(myTransform.position, viewRange, targetLayer);

        foreach (Collider target in targets)
        {
            // Check if the target is within the view angle
            Vector3 directionToTarget = target.transform.position - (myTransform.position + offset);
            float angleToTarget = Vector3.Angle(myTransform.forward, directionToTarget);

            if (angleToTarget <= viewAngle / 2f)
            {
                // The target is within the cone view
                Debug.Log("Target detected: " + target.name);
                this.target = target.transform;
                return;
            }
        }

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
    }
}
