using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.InputSystem;
using Random = UnityEngine.Random;

public class WeaponController : MonoBehaviour
{
    [SerializeField] private Transform firePoint;
    [SerializeField] private float fireRange = 100f;
    [SerializeField] private float fireRate = 0.1f;
    [SerializeField] private TrailRenderer trailRenderer;
    [Space]
    [SerializeField] private ParticleSystem muzzleFlash;
    [SerializeField] private GameObject bloodImpact;

    private float nextFireTime;

    public void OnFirePress(InputAction.CallbackContext ctx)
    {
        if (ctx.ReadValue<float>() >= 1f)
        {
            Shoot();
            nextFireTime = Time.time + 1f / fireRate;
        }
        
    }

    private void Shoot()
    {
        muzzleFlash.Play();
        RaycastHit hit;
        if (Physics.Raycast(firePoint.position, firePoint.forward, out hit, fireRange))
        {
            var trail = Instantiate(trailRenderer, firePoint.position, quaternion.identity);
            StartCoroutine(SpawnTrail(trail, hit));
            if (hit.transform.TryGetComponent(out IDamegeble dmg))
            {
                Instantiate(bloodImpact, hit.point, Quaternion.LookRotation(hit.normal));
                dmg.TakeDamage(10);
            }
            Debug.Log("Hit object: " + hit.transform.name);
        }

        Debug.DrawRay(firePoint.position, firePoint.forward * fireRange, Color.red, 0.1f);
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
