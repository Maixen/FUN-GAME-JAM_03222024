using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CrosshairPainter : MonoBehaviour
{
    [SerializeField]
    private Transform playerCam;
    [SerializeField]
    private Image img;
    [SerializeField]
    private LayerMask enemy;
    [SerializeField]
    private LayerMask pickup;

    private float pickupRange;

    private void Start()
    {
        pickupRange = PhysicsPickUp.instance.range;
    }

    private void Update()
    {
        if (Physics.Raycast(playerCam.position, playerCam.forward, 1000f, enemy))
        {
            img.color = Color.red;
        }
        else if (Physics.Raycast(playerCam.position, playerCam.forward, pickupRange, pickup))
        {
            img.color = Color.green;
        }
        else
        {
            img.color = Color.white;
        }
    }
}
