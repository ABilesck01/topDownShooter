using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UIElements;

public class BotAttack : WeaponController
{
    private BotController controller;
    private Transform myTransform;
    private Transform target;

    public override void Awake()
    {
        base.Awake();
        myTransform = transform;
        controller = GetComponent<BotController>();
    }

    public void SetTarget(Transform target)
    {
        this.target = target;
    }

    public override void Update()
    {
        if (controller.botState != BotState.attack) return;
        if (target == null) return;

        myTransform.LookAt(new Vector3(target.position.x, myTransform.position.y, target.position.z));

        if(Time.time >= nextFireTime)
        {
            Debug.Log("aaaa");
            Shoot();
        }
    }
}
