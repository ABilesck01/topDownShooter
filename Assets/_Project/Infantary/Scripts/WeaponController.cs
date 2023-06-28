using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;
using Random = UnityEngine.Random;

public class WeaponController : MonoBehaviour
{
    [SerializeField] private Transform firePoint;
    [SerializeField] private float fireRange = 100f;
    [SerializeField] private float fireRate = 0.1f;
    [SerializeField] private int ammo;
    [SerializeField] private int damage;
    [SerializeField] private float timeToReload;
    [SerializeField] private TrailRenderer trailRenderer;
    [Space]
    [SerializeField] private AudioSource gunAudio;
    [SerializeField] private ParticleSystem muzzleFlash;
    [SerializeField] private GameObject bloodImpact;
    
    [HideInInspector]public InfantaryInputs infantaryInputs;
    private Animator animator;

    public UnityEvent OnKillPlayer;

    private int currentAmmo;
    [HideInInspector]public float nextFireTime;
    private bool hasShot = false;
    private bool isReloading = false;

    private WeaponView weaponView;

    public WeaponView WeaponView
    {
        get => weaponView; set
        { 
            weaponView = value;
            weaponView.SetMaxAmmo(ammo);
        }
    }

    public int Ammo { get => ammo; set => ammo = value; }
    public int Damage { get => damage; set => damage = value; }
    public float FireRate { get => fireRate; set => fireRate = value; }

    public virtual void Awake()
    {
        animator = GetComponent<Animator>();
        currentAmmo = ammo;
        gunAudio = GetComponent<AudioSource>();
    }

    public void SetInfantaryInput(InfantaryInputs inputs)
    {
        infantaryInputs = inputs;
    }

    public virtual void GetInputs()
    {
        if (infantaryInputs.FireInput && Time.time >= nextFireTime &&!hasShot)
        {
            Shoot();
        }

        if(!infantaryInputs.FireInput)
        {
            hasShot = false;
        }
    }

    public virtual void Update()
    {
        GetInputs();
        GetReloadInputs();
    }

    public void GetReloadInputs()
    {
        if (infantaryInputs.ReloadInput && !isReloading)
        {
            StartCoroutine(Reload());
        }
    }

    public void Shoot()
    {
        if (currentAmmo <= 0) return;

        if (isReloading) return;

        hasShot = true;

        animator.SetTrigger("fire");
        gunAudio?.Play();
        muzzleFlash.Play();
        
        RaycastHit hit;
        if (Physics.Raycast(firePoint.position, firePoint.forward, out hit, fireRange))
        {
            var trail = Instantiate(trailRenderer, firePoint.position, quaternion.identity);
            StartCoroutine(SpawnTrail(trail, hit));
            if (hit.transform.TryGetComponent(out IDamegeble dmg))
            {
                Instantiate(bloodImpact, hit.point, Quaternion.LookRotation(hit.normal));
                if (dmg.TakeDamage(damage))
                    OnKillPlayer?.Invoke();
            }
            Debug.Log("Hit object: " + hit.transform.name);
        }

        nextFireTime = Time.time + 1f / fireRate;

        currentAmmo--;
        weaponView?.SetAmmo(currentAmmo);
        if (currentAmmo <= 0) 
        {
            StartCoroutine(Reload());
        }

        Debug.DrawRay(firePoint.position, firePoint.forward * fireRange, Color.red, 0.1f);
    }

    private IEnumerator Reload()
    {
        isReloading = true;
        weaponView?.SetReloading();
        animator.SetTrigger("reload");
        yield return new WaitForSeconds(timeToReload);
        currentAmmo = ammo;
        weaponView?.SetAmmo(currentAmmo);
        isReloading = false;
    }

    private IEnumerator SpawnTrail(TrailRenderer trail, RaycastHit hit)
    {
        float time = 0;
        Vector3 startPosition = trail.transform.position;
        while (time < 1)
        {
            trail.transform.position = Vector3.Lerp(startPosition, hit.point, time);
            time += Time.deltaTime / trail.time;
            yield return null;
        }

        trail.transform.position = hit.point;
        Destroy(trail.gameObject);
    }
}
