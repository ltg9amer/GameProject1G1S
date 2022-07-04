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
    [SerializeField] private AudioSource siren;
    [SerializeField] private AudioSource pop;
    private bool isPlayImpulse;
    private bool isPlayMetropolis;
    private bool isPlayNeon;
    private bool isPlaySiren;

    public bool IsPlayImpulse => isPlayImpulse;
    public bool IsPlayMetropolis => isPlayMetropolis;
    public bool IsPlayNeon => isPlayNeon;
    public bool IsPlaySiren => isPlaySiren;

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
    }

    private void Update()
    {
        impulse.volume = PlayerPrefs.GetFloat("BGMVolume", 1);
        metropolis.volume = PlayerPrefs.GetFloat("BGMVolume", 1);
        neon.volume = PlayerPrefs.GetFloat("BGMVolume", 1);
        siren.volume = PlayerPrefs.GetFloat("SFXVolume", 1);
        pop.volume = PlayerPrefs.GetFloat("SFXVolume", 1);
    }

    public void PlayImpulse()
    {
        isPlayMetropolis = false;
        isPlayNeon = false;
        isPlaySiren = false;
        isPlayImpulse = true;
        neon.Stop();
        impulse.Play();
    }

    public void PlayMetropolis()
    {
        isPlayImpulse = false;
        isPlayNeon = false;
        isPlaySiren = false;
        isPlayMetropolis = true;
        siren.Stop();
        metropolis.Play();
    }

    public void PlayNeon()
    {
        isPlayImpulse = false;
        isPlayMetropolis = false;
        isPlaySiren = false;
        isPlayNeon = true;
        impulse.Stop();
        metropolis.Stop();
        siren.Stop();
        neon.Play();
    }

    public void PlaySiren()
    {
        isPlayImpulse = false;
        isPlayMetropolis = false;
        isPlayNeon = false;
        isPlaySiren = true;
        impulse.Stop();
        neon.Stop();
        siren.Play();
    }

    public void PlayPop()
    {
        pop.Play();
    }
}
