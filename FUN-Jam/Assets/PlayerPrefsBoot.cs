using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerPrefsBoot : MonoBehaviour
{
    private void Start()
    {
        if (SceneManager.GetActiveScene().name != "OnBoot")
            PlayerPrefs.SetString("lastScene", SceneManager.GetActiveScene().name);
    }
}
