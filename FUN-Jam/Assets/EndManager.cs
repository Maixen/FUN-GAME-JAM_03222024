using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndManager : MonoBehaviour
{
    [SerializeField]
    private AudioSource audio;
    [SerializeField]
    private AudioClip clip;
    [SerializeField]
    private float playAfter;
    [SerializeField]
    private GameObject textForClip;

    [SerializeField]
    private AudioClip soundForReveal;
    [SerializeField]
    private GameObject drive;

    private void Start()
    {
        //audio.clip = soundForReveal;
        //audio.Play();

        drive.SetActive(true);
        textForClip.SetActive(false);

        Invoke(nameof(PlayCLipAndText), playAfter);
    }

    private void PlayCLipAndText()
    {
        audio.clip = clip;
        audio.Play();

        drive.SetActive(false);
        textForClip.SetActive(true);
    }
}
