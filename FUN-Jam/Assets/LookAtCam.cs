using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class LookAtCam : MonoBehaviour
{
    [SerializeField]
    private Transform playerCamera;

    private void Update()
    {
        transform.rotation = Quaternion.LookRotation(transform.position - playerCamera.position);
    }
}
