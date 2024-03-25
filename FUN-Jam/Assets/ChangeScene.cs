using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ChangeScene : MonoBehaviour
{
    [SerializeField]
    private string scene;
    [SerializeField]
    private KeyCode startOrSkip;
    [SerializeField]
    private float timeToGoOn;

    private void Start()
    {
        if (timeToGoOn != -1f)
        {
            Invoke(nameof(LoadScene), timeToGoOn);
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(startOrSkip))
        {
            LoadScene();
        }
    }

    private void LoadScene()
    {
        if (PlayerPrefs.GetString("lastScene") != "OnEnd")
            SceneManager.LoadScene(PlayerPrefs.GetString("lastScene"));
    }
}
