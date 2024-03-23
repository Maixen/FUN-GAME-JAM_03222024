using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitmarkerManager : MonoBehaviour
{
    public static HitmarkerManager instance;

    [SerializeField]
    private float hitmarkerPlayDuration;

    [SerializeField]
    private GameObject hitmarker;

    private void Awake()
    {
        instance = this;
    }

    public void PlayHitmarker()
    {
        GameObject hitmarkerInstance = Instantiate(hitmarker, transform);
        Destroy(hitmarkerInstance, hitmarkerPlayDuration + 0.1f);
    }
}
