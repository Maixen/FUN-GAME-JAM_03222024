using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    [SerializeField]
    private Transform player;
    [SerializeField]
    private string nextSceneName;
    [SerializeField]
    private KeyCode reloadScene;
    [SerializeField]
    private Animator a;
    [SerializeField]
    private TextMeshProUGUI textForReopening;

    [SerializeField]
    private float startAnimationTime;

    private bool reloading;

    private void Awake()
    {
        Time.timeScale = 1.0f;

        instance = this;
    }

    private void Start()
    {
        a.SetBool("GameOver", false);
        a.SetBool("GameReload", false);
        a.SetBool("GameStart", true);

        Invoke(nameof(GoToIdleAnimation), startAnimationTime);
    }

    private void GoToIdleAnimation()
    {
        a.SetBool("GameStart", false);
    }

    private void Update()
    {
        if (Input.GetKeyDown(reloadScene))
        {
            ReloadScene(false);
        }

        if (player.position.y < -15f)
        {
            ReloadScene(true);
        }
    }

    public void LoadNextLevel()
    {
        if (reloading) { return; }

        reloading = true;

        StartCoroutine(LoadNextSceneWithEffect());
    }

    public void ReloadScene(bool death)
    {
        if (reloading) { return; }

        reloading = true;

        StartCoroutine(ReloadSceneWithEffect(death));
    }

    IEnumerator ReloadSceneWithEffect(bool death)
    {
        if (death)
            a.SetBool("GameOver", true);
        else
            a.SetBool("GameReload", true);

        Time.timeScale = 0.1f;

        yield return new WaitForSeconds(0.75f);

        string currentSceneName = SceneManager.GetActiveScene().name;
        SceneManager.LoadScene(currentSceneName);
    }

    IEnumerator LoadNextSceneWithEffect()
    {
        textForReopening.text = $"Loading next level";
        a.SetBool("GameReload", true);

        Time.timeScale = 0.1f;

        yield return new WaitForSeconds(0.75f);

        SceneManager.LoadScene(nextSceneName);
    }
}
