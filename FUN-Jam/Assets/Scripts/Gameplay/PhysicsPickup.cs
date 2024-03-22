using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhysicsPickup : MonoBehaviour
{
    [SerializeField]
    private float forceApplied;
    [SerializeField]
    private float range;
    [SerializeField]
    private float maxVelocity;
    [SerializeField]
    private LayerMask pickup;

    private Rigidbody target;
    private Vector3 targetPos;
    RaycastHit hit;

    private bool hasTarget;

    private void Update()
    {
        if (Input.GetMouseButtonDown(2))
        {
            AssignNewTarget();
        }

        if (Input.GetMouseButtonUp(2))
        {
            RemoveTarget();
        }

        if (hasTarget )
        {
            Vector3 forceDirection = (targetPos - target.position).normalized;

            target.AddForce(forceDirection * forceApplied * Time.deltaTime, ForceMode.Force);

            if (target.velocity.magnitude > maxVelocity)
            {
                target.velocity = target.velocity.normalized * maxVelocity;
            }
        }
    }

    private void AssignNewTarget()
    {

        if (Physics.Raycast(transform.position, transform.forward, out hit, range, pickup))
        {
            target = hit.collider.gameObject.GetComponent<Rigidbody>();
            targetPos = hit.point;

            hasTarget = true;
        }
    }

    private void RemoveTarget()
    {
        hasTarget = false;
    }
}