using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour
{
    [SerializeField]
    private LayerMask enemy;
    [SerializeField]
    private float resetTime;
    [SerializeField]
    private GameObject hitEffect;
    [SerializeField]
    private float hitEffectTime;
    [SerializeField]
    private ParticleSystem muzzleFlash;

    private RaycastHit hit;
    private bool readyToShoot;

    private void OnEnable()
    {
        readyToShoot = true;
        muzzleFlash.Stop();
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0) && readyToShoot)
        {
            readyToShoot = false;

            muzzleFlash.Play();

            if (Physics.Raycast(transform.position, transform.forward, out hit, 1000f, enemy))
            {
                Debug.Log("Enemy hit");

                hit.collider.gameObject.GetComponent<Enemy>()?.TakeDamage(25);

                HitmarkerManager.instance.PlayHitmarker();
            }

            Physics.Raycast(transform.position, transform.forward, out hit, 1000f);

            GameObject hitEffectInstantiation = Instantiate(hitEffect, hit.point, Quaternion.LookRotation(hit.normal));
            Destroy(hitEffectInstantiation, hitEffectTime);

            GetComponentInChildren<Effex>()?.OnFire();

            Invoke(nameof(ResetShoot), resetTime);
        }
    }

    private void ResetShoot()
    {
        readyToShoot = true;
    }
}
