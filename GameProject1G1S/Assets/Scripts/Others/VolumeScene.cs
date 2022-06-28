using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class VolumeScene : MonoBehaviour
{
    [SerializeField] private Slider volumeSlider;
    [SerializeField] private TextMeshProUGUI textCurrentVolume;
    private AudioSource[] audioSource;

    private void Awake()
    {
        audioSource = AudioManager.Instance.GetComponents<AudioSource>();

        volumeSlider.value = PlayerPrefs.GetFloat("VolumeSliderValue", 100);
    }

    private void Update()
    {
        textCurrentVolume.text = volumeSlider.value.ToString();

        if (Input.GetKeyDown(KeyCode.D))
        {
            StartCoroutine("VolumeUp");
        }
        else if (Input.GetKeyUp(KeyCode.D))
        {
            StopCoroutine("VolumeUp");
        }

        if (Input.GetKeyDown(KeyCode.A))
        {
            StartCoroutine("VolumeDown");
        }
        else if (Input.GetKeyUp(KeyCode.A))
        {
            StopCoroutine("VolumeDown");
        }
    }

    public void VolumeChange()
    {
        for (int i = 0; i < audioSource.Length; i++)
        {
            audioSource[i].volume = volumeSlider.value / 100;

            PlayerPrefs.SetFloat($"BGM{i}Volume", audioSource[i].volume);
        }

        PlayerPrefs.SetFloat("VolumeSliderValue", volumeSlider.value);
    }

    IEnumerator VolumeUp()
    {
        while (true)
        {
            volumeSlider.value += 1;

            yield return new WaitForSeconds(0.05f);
        }
    }

    IEnumerator VolumeDown()
    {
        while (true)
        {
            volumeSlider.value -= 1;

            yield return new WaitForSeconds(0.05f);
        }
    }
}
