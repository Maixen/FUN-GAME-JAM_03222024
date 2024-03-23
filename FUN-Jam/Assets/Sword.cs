using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sword : MonoBehaviour
{
    [SerializeField]
    private LayerMask enemy;
    [SerializeField]
    private Animator a;
    [SerializeField]
    private float swipeTime;
    [SerializeField]
    private float swipeTimeBuffer;
    [SerializeField]
    private Transform attackPoint;
    [SerializeField]
    private float radius;
    [SerializeField]
    private float resetTime;

    private bool swipeRight;
    private RaycastHit[] hits;
    private bool readyToAttack;

    private void OnEnable()
    {
        readyToAttack = true;
    }

    private void Update()
    {
        if (Input.GetMouseButton(0) && readyToAttack)
        {
            readyToAttack = false;

            hits = Physics.SphereCastAll(attackPoint.position, radius, Vector3.forward, radius, enemy);

            foreach (RaycastHit hit in hits)
            {
                hit.collider.gameObject.GetComponent<Enemy>()?.TakeDamage(100);
            }

            if (hits.Length > 0)
            {
                HitmarkerManager.instance.PlayHitmarker();
            }

            swipeRight = !swipeRight;
            a.SetBool("Right", swipeRight);

            a.SetBool("Sword", true);

            Invoke(nameof(ResetAnimator), swipeTime + swipeTimeBuffer);

            GetComponentInChildren<Effex>()?.OnFire();

            Invoke(nameof(ResetShoot), resetTime);
        }
    }

    private void ResetShoot()
    {
        readyToAttack = true;
    }

    private void ResetAnimator()
    {
        a.SetBool("Sword", false);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(attackPoint.position, radius);
    }
}
