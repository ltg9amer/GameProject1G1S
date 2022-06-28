using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance;

    [SerializeField] private AudioSource impulse;
    [SerializeField] private AudioSource metropolis;
    [SerializeField] private AudioSource neon;
    private bool isPlay;

    public bool IsPlay => isPlay;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
            return;
        }

        DontDestroyOnLoad(gameObject);

        impulse.volume = PlayerPrefs.GetFloat($"BGM1Volume", 100);
        metropolis.volume = PlayerPrefs.GetFloat($"BGM2Volume", 100);
        neon.volume = PlayerPrefs.GetFloat($"BGM3Volume", 100);
    }

    public void PlayImpulse()
    {
        isPlay = false;
        metropolis.Stop();
        neon.Stop();
        impulse.Play();
    }

    public void PlayMetropolis()
    {
        isPlay = false;
        impulse.Stop();
        neon.Stop();
        metropolis.Play();
    }

    public void PlayNeon()
    {
        isPlay = true;
        impulse.Stop();
        metropolis.Stop();
        neon.Play();
    }

}
