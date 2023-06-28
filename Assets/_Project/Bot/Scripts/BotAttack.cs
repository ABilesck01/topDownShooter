using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UIElements;

public class BotAttack : WeaponController
{
    public override void Update()
    {

    }

    public void HandleShoot()
    {
        if (Time.time >= nextFireTime)
        {
            Shoot();
        }
    }
}
