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

    public Transform Target
    {
        get => target; set
        {
            target = value;
        }
    }

    private void Start()
    {
        _transform = transform;
    }

    private void LateUpdate()
    {
        if(target == null) return;

        Vector3 desiredPosition = target.position + offset;
        Vector3 smoothedPosition = Vector3.Lerp(_transform.position, desiredPosition, smoothness);
        _transform.position = smoothedPosition;

    }
}
