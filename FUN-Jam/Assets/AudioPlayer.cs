using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioPlayer : MonoBehaviour
{
    [SerializeField]
    private AudioSource audio;
    [SerializeField]
    private AudioClip music;

    private void Start()
    {
        audio.clip = music;
        audio.loop = true;
        audio.Play();   
    }
}

