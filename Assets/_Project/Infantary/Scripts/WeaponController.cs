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
    [SerializeField] private float accuracy = 0.5f; // 0.0f to 1.0f
    [SerializeField] private TrailRenderer trailRenderer;

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
        // Calculate the direction with some random offset based on accuracy
        Vector3 bulletDirection = firePoint.forward + Random.insideUnitSphere * (1f - accuracy);
        bulletDirection.Normalize();

        // Perform raycast to check for hit
        RaycastHit hit;
        if (Physics.Raycast(firePoint.position, firePoint.forward, out hit, fireRange))
        {
            var trail = Instantiate(trailRenderer, firePoint.position, quaternion.identity);
            StartCoroutine(SpawnTrail(trail, hit));
            Debug.Log("Hit object: " + hit.transform.name);
        }

        // Debug draw the ray
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
        if (Physics.CheckSphere(hit.point, 0.1f))
        {
            if (hit.transform.TryGetComponent(out IDamegeble dmg))
            {
                dmg.TakeDamage(10);
            }
        }
        Destroy(trail.gameObject);
    }
}
