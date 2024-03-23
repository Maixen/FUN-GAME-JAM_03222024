using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponHandler : MonoBehaviour
{
    [SerializeField]
    private GameObject sword;
    [SerializeField]
    private GameObject gun;

    private void Start()
    {
        sword.SetActive(true);
        gun.SetActive(false);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            SwitchActiveState(sword);
            SwitchActiveState(gun);
        }
    }

    private void SwitchActiveState(GameObject go)
    {
        if (go.activeSelf)
        {
            go.SetActive(false);
        }
        else
        {
            go.SetActive(true);
        }
    }
}
