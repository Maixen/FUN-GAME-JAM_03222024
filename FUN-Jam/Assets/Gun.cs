using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour
{
    [SerializeField]
    private LayerMask enemy;
    [SerializeField]
    private float resetTime;

    private RaycastHit hit;
    private bool readyToShoot;

    private void OnEnable()
    {
        readyToShoot = true;
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0) && readyToShoot)
        {
            readyToShoot = false;

            if (Physics.Raycast(transform.position, transform.forward, out hit, 1000f, enemy))
            {
                Debug.Log("Enemy hit");

                hit.collider.gameObject.GetComponent<Enemy>()?.TakeDamage(25);

                HitmarkerManager.instance.PlayHitmarker();
            }

            GetComponentInChildren<Effex>()?.OnFire();

            Invoke(nameof(ResetShoot), resetTime);
        }
    }

    private void ResetShoot()
    {
        readyToShoot = true;
    }
}
