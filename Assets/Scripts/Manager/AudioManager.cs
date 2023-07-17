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

    public AudioSource Impulse
    {
        get { return impulse; }
        set { impulse = value; }
    }
    public AudioSource Metropolis
    {
        get { return metropolis; }
        set { metropolis = value; }
    }
    public AudioSource Neon
    {
        get { return neon; }
        set { neon = value; }
    }
    public AudioSource Siren
    {
        get { return siren; }
        set { siren = value; }
    }
    public AudioSource Pop
    {
        get { return pop; }
        set { pop = value; }
    }
    public bool IsPlayImpulse
    {
        get { return isPlayImpulse; }
        set { isPlayImpulse = value; }
    }
    public bool IsPlayMetropolis
    {
        get { return isPlayMetropolis; }
        set { isPlayMetropolis = value; }
    }
    public bool IsPlayNeon
    {
        get { return isPlayNeon; }
        set { isPlayNeon = value; }
    }
    public bool IsPlaySiren
    {
        get { return isPlaySiren; }
        set { isPlaySiren = value; }
    }

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
}
