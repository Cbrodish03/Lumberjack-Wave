using UnityEngine;
using UnityEngine.SceneManagement;
using System;

public class MainMenuUI : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Time.timeScale = 1;
    }

    public void StartGameButton()
    {
        SceneManager.LoadScene(1);
    }

    public void ExitGameButton()
    {
        Application.Quit();
    }
}
