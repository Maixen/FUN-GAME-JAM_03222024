using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Receiver : MonoBehaviour
{
    [SerializeField]
    private TextMeshPro partsNeededDisplay;
    [SerializeField]
    private int partsNeeded;
    [SerializeField]
    private LayerMask pickup;

    private bool won;

    private void OnEnable()
    {
        won = false;
    }

    private void Update()
    {
        RaycastHit[] rayHits = Physics.SphereCastAll(transform.position, 2, Vector3.forward, 2, pickup);

        partsNeededDisplay.text = $"{rayHits.Length} / {partsNeeded}";

        if (rayHits.Length >= partsNeeded && won == false)
        {
            won = true;
            GameManager.instance.LoadNextLevel();
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, 2f);
    }
}
