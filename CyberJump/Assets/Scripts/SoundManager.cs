using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SoundManager : MonoBehaviour
{
    public AudioSource efxSource;
    public AudioSource musicSource;
    public AudioClip mainMusic;
    public static SoundManager instance = null;

    private Slider volumeSlider;
    public float lowPitchRange = .95f;
    public float highPitchRange = 1.05f;




    // Start is called before the first frame update
    public void Start()
    {
        SetVolumeSlider();
    }


    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else if (instance != this)
        {
            Destroy(gameObject);
        }
        DontDestroyOnLoad(gameObject);

        SetVolumeSlider();
    }

    public void PlaySingle(AudioClip clip)
    {
        float randomPitch = Random.Range(lowPitchRange, highPitchRange);

        efxSource.pitch = randomPitch;
        efxSource.clip = clip;
        efxSource.Play();
    }

    public void ChangeMusic(AudioClip clip)
    {
        musicSource.clip = clip;
        musicSource.Play();
    }

    public void MusicStop()
    {
        musicSource.Pause();
    }

    public void KillSound()
    {
        Destroy(gameObject);
    }

    public void ChangeVolume()
    {
        volumeSlider = FindObjectOfType<Slider>();
        AudioListener.volume = volumeSlider.value;
        Save();
    }

    private void Load()
    {
        volumeSlider.value = PlayerPrefs.GetFloat("musicVolume");
    }
    private void Save()
    {
        PlayerPrefs.SetFloat("musicVolume", volumeSlider.value);
    }

    public void SetVolumeSlider()
    {
        volumeSlider = FindObjectOfType<Slider>();

        if (!PlayerPrefs.HasKey("musicVolume"))
        {
            PlayerPrefs.SetFloat("musicVolume", 1);
            Load();
        }
        else
        {
            Load();
        }

        
    }

    public void ResetMusic()
    {
        ChangeMusic(mainMusic);
    }
}
