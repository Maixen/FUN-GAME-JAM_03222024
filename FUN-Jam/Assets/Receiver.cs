using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Receiver : MonoBehaviour
{
    [SerializeField]
    private int partsNeeded;
    [SerializeField]
    private LayerMask pickup;

    private void Update()
    {
        RaycastHit[] rayHits = Physics.SphereCastAll(transform.position, 2, Vector3.forward, 2, pickup);

        if (rayHits.Length >= partsNeeded)
        {
            Debug.Log("You won");
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, 2f);
    }
}
