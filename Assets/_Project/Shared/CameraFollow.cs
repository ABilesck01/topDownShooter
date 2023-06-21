using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField] private Transform target;
    [SerializeField] private float smoothness = 0.5f;
    [SerializeField] private Vector3 offset;

    private Transform _transform;

    private void Start()
    {
        _transform = transform;
        offset = _transform.position - target.position;
    }

    private void LateUpdate()
    {
        Vector3 desiredPosition = target.position + offset;
        Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothness);
        transform.position = smoothedPosition;

    }
}
