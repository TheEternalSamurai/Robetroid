using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class InGameMenuManager : MonoBehaviour
{
    public static bool isPaused;
    public GameObject pauseMenu;
    public GameObject winMenu;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Return))
        {
            if (isPaused)
                Resume();
            else
                Pause();
        }
    }

    public void Resume()
    {
        pauseMenu.SetActive(false);
        Time.timeScale = 1f;
        isPaused = false;
    }
    
    public void Restart()
    {
        FindObjectOfType<AudioManager>().Stop("Stage Music");
        FindObjectOfType<AudioManager>().Play("Stage Music");
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void Quit()
    {
        SceneManager.LoadScene("MainMenu");
    }

    private void Pause()
    {
        pauseMenu.SetActive(true);
        Time.timeScale = 0f;
        isPaused = true;
    }
}
