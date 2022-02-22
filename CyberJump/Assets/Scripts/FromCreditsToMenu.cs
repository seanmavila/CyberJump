using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FromCreditsToMenu : MonoBehaviour
{
    public void GoToMenu()
    {
        SceneManager.LoadScene(0);
        SoundManager.instance.MusicStop();
        SoundManager.instance.ResetMusic();
    }
}
