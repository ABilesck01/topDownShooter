using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoWeaponController : WeaponController
{
    public override void GetInputs()
    {
        if (infantaryInputs.FireInput && Time.time >= nextFireTime)
        {
            Shoot();
        }
    }
}
