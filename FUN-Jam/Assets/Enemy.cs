using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.AI;
using static UnityEngine.GraphicsBuffer;
using TMPro;

public class Enemy : MonoBehaviour
{
    [SerializeField]
    private NavMeshAgent agent;

    [SerializeField]
    private GameObject inventory;

    [SerializeField]
    private TextMeshPro healthDisplay;

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
    private float attackTimeFirst;
    [SerializeField]
    private float attackTimeReset;
    [SerializeField]
    private float attackedNormalTime;
    private bool canAttack;
    private bool canAttackFast;

    [SerializeField]
    private int health;

    [SerializeField]
    private List<Transform> patrolPoints = new List<Transform>();

    private int currentPatrolPoint;

    private float distanceToPlayer;

    private float hit;

    private float startHealth;

    private void Start()
    {
        startHealth = health;
        currentPatrolPoint = 0;
        canAttack = false;
        canAttackFast = true;
    }

    private void Update()
    {
        distanceToPlayer = Vector3.Distance(transform.position, playerObj.position);

        if (distanceToPlayer <= attackDistance)
        {
            agent.SetDestination(transform.position);

            Vector3 direction = playerObj.position - transform.position;
            Quaternion targetRotation = Quaternion.LookRotation(direction);
            float step = agent.angularSpeed * Time.deltaTime;
            transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, step);

            if (canAttack)
            {
                canAttack = false;

                playerObj.GetComponent<PlayerHealth>().TakeDamage(attackDamage);
                StopAllCoroutines();
                StartCoroutine(ResetAttack());
            }

            if (canAttackFast)
            {
                canAttackFast = false;
                StopAllCoroutines();
                StartCoroutine(ResetAttackFirst());
            }

        }
        else if (distanceToPlayer <= followDistance)
        {
            agent.SetDestination(playerObj.position);
            canAttack = false;
            canAttackFast = true;
        }
        else if (hit > 0f)
        {
            hit -= Time.deltaTime;
            agent.SetDestination(playerObj.position);
            canAttack = false;
            canAttackFast = true;
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
            canAttack = false;
            canAttackFast = true;
        }
    }

    IEnumerator ResetAttack()
    {
        yield return new WaitForSeconds(attackTimeReset);

        canAttack = true;
    }

    IEnumerator ResetAttackFirst()
    {
        yield return new WaitForSeconds(attackTimeFirst);

        canAttack = true;
    }

    public void TakeDamage(int damage)
    {
        health -= damage;

        hit = attackedNormalTime;

        if (healthDisplay != null)
            healthDisplay.text = $"{health} / {startHealth}";

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
        deathEffect.transform.localScale = new Vector3(3, 3, 3);
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
