using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhysicsPickUp : MonoBehaviour
{
    public static PhysicsPickUp instance;

    [SerializeField]
    private float forceApplied;
    public float range;
    [SerializeField]
    private float maxVelocity;
    [SerializeField]
    private Transform targetPosTransform;
    [SerializeField]
    private LayerMask pickup;

    private Rigidbody target;
    RaycastHit hit;

    private bool hasTarget;

    private void Awake()
    {
        instance = this;
    }

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

        if (hasTarget)
        {
            Vector3 forceDirection = (targetPosTransform.position - target.position).normalized;

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
            targetPosTransform.position = hit.point;

            hasTarget = true;
        }
    }

    private void RemoveTarget()
    {
        hasTarget = false;
    }
}
