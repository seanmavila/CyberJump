using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuUIHandler : MonoBehaviour
{
    public GameObject soundWindow;
    public GameObject instructionWindow;

    private void Start()
    {
        if (soundWindow.activeInHierarchy)
        {
            soundWindow.SetActive(false);
        }
        
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            soundWindow.SetActive(false);
        }
    }

    public void PlayGame()
    {
        SceneManager.LoadScene(1);
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void GoBack()
    {
        soundWindow.SetActive(false);
    }

    public void GoToSound()
    {
        soundWindow.SetActive(true);
    }

    public void OpenInstructions()
    {
        instructionWindow.SetActive(true);
    }

    public void GoToMenu()
    {
        SceneManager.LoadScene(0);
        SoundManager.instance.MusicStop();
        SoundManager.instance.ResetMusic();
    }
    public void GoToCredits()
    {
        SceneManager.LoadScene(2);
    }
}
