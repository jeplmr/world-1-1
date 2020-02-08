using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement; 

public class PauseMenu : MonoBehaviour
{
    public static bool isPaused = false;
    public GameObject pauseMenuUI;
    public AudioSource music;
    public AudioSource pauseTone; 

    void Update()
    {
      if (Input.GetKeyDown(KeyCode.Escape) || Input.GetButtonDown("Submit"))
        {
            if (isPaused)
            {
                Resume(); 
            } else
            {
                Pause(); 
            }
        }  
    }


    public void Resume()
    {
        pauseMenuUI.SetActive(false);
        music.UnPause();
        pauseTone.Play();
        Time.timeScale = 1f;
        isPaused = false; 
    }

    public void Pause()
    {
        pauseMenuUI.SetActive(true);
        music.Pause();
        pauseTone.Play(); 
        Time.timeScale = 0f;
        isPaused = true; 
    }

    public void GoToMenu()
    {
        SceneManager.LoadScene(0);
        Resume(); 
    }

    public void QuitGame()
    {
        Application.Quit(); 
    }

}


