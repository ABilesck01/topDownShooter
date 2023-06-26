using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class WeaponView : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI txtAmmo;

    private int maxAmmo;

    public void SetMaxAmmo(int maxAmmo)
    {
        this.maxAmmo = maxAmmo;
        SetAmmo(maxAmmo);
    }

    public void SetReloading()
    {
        txtAmmo.text = "Reloading";
    }

    public void SetAmmo(int ammo)
    {
        txtAmmo.text = $"{ammo}/{maxAmmo}";
    }
}
