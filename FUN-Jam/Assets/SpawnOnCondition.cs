using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum SpawnOnConditionType
{
    PlayerInArea,
    NoEnemysInArea
}

public class SpawnOnCondition : MonoBehaviour
{
    [SerializeField]
    private SpawnOnConditionType condition;

    [SerializeField]
    private GameObject inventory;

    [SerializeField]
    private Vector3 checkSize;

    [SerializeField]
    private LayerMask player;
    [SerializeField]
    private LayerMask enemy;

    private void Update()
    {
        if (condition == SpawnOnConditionType.PlayerInArea && Physics.OverlapBox(transform.position, checkSize, Quaternion.identity, player).Length != 0)
        {
            OnEventEnter();
        }
        else if (condition == SpawnOnConditionType.NoEnemysInArea && Physics.OverlapBox(transform.position, checkSize, Quaternion.identity, enemy).Length == 0)
        {
            OnEventEnter();
        }
    }

    private void OnEventEnter()
    {
        Instantiate(inventory, transform.position, Quaternion.identity);
        Destroy(gameObject);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireCube(transform.position, checkSize * 2);
    }
}
