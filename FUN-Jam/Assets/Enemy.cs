using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    [SerializeField]
    private NavMeshAgent agent;

    [SerializeField]
    private Transform playerObj;

    [SerializeField]
    private float followDistance;
    [SerializeField]
    private float attackDistance;

    [SerializeField]
    private int health;

    private float distanceToPlayer;

    private void Update()
    {
        distanceToPlayer = Vector3.Distance(transform.position, playerObj.position);

        if (distanceToPlayer <= attackDistance)
        {
            agent.SetDestination(transform.position);

            // Attack player
        }
        else if (distanceToPlayer <= followDistance)
        {
            agent.SetDestination(playerObj.position);
        }
        else
        {
            // Do a patrol routine
        }
    }

    public void TakeDamage(int damage)
    {
        health -= damage;

        if (health <= 0)
        {
            Die();
        }
        else
        {

        }
    }

    private void Die()
    {
        Destroy(gameObject);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackDistance);

        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, followDistance);

        Gizmos.color = Color.green;
        Gizmos.DrawLine(transform.position, playerObj.position);
    }
}
