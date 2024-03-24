using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    [SerializeField]
    private NavMeshAgent agent;

    [SerializeField]
    private GameObject inventory;

    [SerializeField]
    private GameObject deathEffect;
    [SerializeField]
    private float destroyDeathEffect;

    [SerializeField]
    private Transform playerObj;

    [SerializeField]
    private float followDistance;
    [SerializeField]
    private float attackDistance;
    [SerializeField]
    private float attackDamage;
    [SerializeField]
    private float attackTime;
    [SerializeField]
    private float attackedNormalTime;
    private bool canAttack;

    [SerializeField]
    private int health;

    [SerializeField]
    private List<Transform> patrolPoints = new List<Transform>();

    private int currentPatrolPoint;

    private float distanceToPlayer;

    private float hit;

    private void Start()
    {
        currentPatrolPoint = 0;
        canAttack = true;
    }

    private void Update()
    {
        distanceToPlayer = Vector3.Distance(transform.position, playerObj.position);

        if (distanceToPlayer <= attackDistance)
        {
            agent.SetDestination(transform.position);

            if (canAttack )
            {
                canAttack = false;

                playerObj.GetComponent<PlayerHealth>().TakeDamage(attackDamage);

                Invoke(nameof(ResetAttack), attackTime);
            }
        }
        else if (distanceToPlayer <= followDistance)
        {
            agent.SetDestination(playerObj.position);
        }
        else if (hit > 0f)
        {
            hit -= Time.deltaTime;
            agent.SetDestination(playerObj.position);
        }
        else
        {
            if (Vector3.Distance(transform.position, patrolPoints[currentPatrolPoint].position) < 1f)
            {
                currentPatrolPoint++;

                if (currentPatrolPoint > patrolPoints.Count - 1)
                {
                    currentPatrolPoint = 0;
                }
            }

            agent.SetDestination(patrolPoints[currentPatrolPoint].position);
        }
    }

    private void ResetAttack()
    {
        canAttack = true;
    }

    public void TakeDamage(int damage)
    {
        health -= damage;

        hit = attackedNormalTime;

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
        Instantiate(inventory, transform.position, Quaternion.identity);

        GameObject deathEffectInstantiation = Instantiate(deathEffect, transform.position, Quaternion.identity);
        Destroy(deathEffectInstantiation, destroyDeathEffect);

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
        Gizmos.DrawLine(transform.position, agent.destination);

        Gizmos.color = Color.blue;

        for (int i = 0; i < patrolPoints.Count - 1; i++)
        {
            if (patrolPoints.Count -1 >= i + 1)
            {
                Gizmos.DrawLine(patrolPoints[i].position, patrolPoints[i + 1].position);
            }
        }

        Gizmos.DrawLine(patrolPoints[patrolPoints.Count - 1].position, patrolPoints[0].position);

        foreach (Transform patrolPoint in patrolPoints)
        {
            Gizmos.DrawSphere(patrolPoint.position, 0.25f);
        }
    }
}
