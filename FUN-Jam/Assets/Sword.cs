using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sword : MonoBehaviour
{
    [SerializeField]
    private LayerMask enemy;
    [SerializeField]
    private Transform attackPoint;
    [SerializeField]
    private float radius;
    [SerializeField]
    private float resetTime;

    private RaycastHit[] hits;
    private bool readyToAttack;

    private void OnEnable()
    {
        readyToAttack = true;
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0) && readyToAttack)
        {
            readyToAttack = false;

            hits = Physics.SphereCastAll(attackPoint.position, radius, Vector3.forward, 1000f, enemy);

            foreach (RaycastHit hit in hits)
            {
                hit.collider.gameObject.GetComponent<Enemy>()?.TakeDamage(100);
            }

            if (hits.Length > 0)
            {
                HitmarkerManager.instance.PlayHitmarker();
            }

            GetComponentInChildren<Effex>()?.OnFire();

            Invoke(nameof(ResetShoot), resetTime);
        }
    }

    private void ResetShoot()
    {
        readyToAttack = true;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(attackPoint.position, radius);
    }
}
