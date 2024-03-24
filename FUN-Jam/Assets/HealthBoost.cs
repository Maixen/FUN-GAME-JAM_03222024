using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthBoost : MonoBehaviour
{
    [SerializeField]
    private int healthBoost;
    [SerializeField]
    private float popRange;
    [SerializeField]
    private LayerMask player;
    [SerializeField]
    private GameObject popEffect;
    [SerializeField]
    private float destroyPopEffect;

    private void Update()
    {
        Collider[] hits = Physics.OverlapSphere(transform.position, popRange, player);

        if (hits.Length != 0)
        {
            hits[0].GetComponentInParent<PlayerHealth>().Heal(healthBoost);

            GameObject popEffectInstantiation = Instantiate(popEffect, transform.position, Quaternion.identity);
            Destroy(popEffectInstantiation, destroyPopEffect);

            Destroy(gameObject);
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, popRange);
    }
}
