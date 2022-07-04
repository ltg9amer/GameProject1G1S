using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class VolumeScene : MonoBehaviour
{
    [SerializeField] private Slider bgmVolumeSlider;
    [SerializeField] private Slider sfxVolumeSlider;
    [SerializeField] private Image bgmRaycast;
    [SerializeField] private Image sfxRaycast;
    [SerializeField] private TextMeshProUGUI textBGMVolume;
    [SerializeField] private TextMeshProUGUI textSFXVolume;
    private bool isBGMChanged;
    private bool isSFXChanged;

    private void Start()
    {
        isBGMChanged = true;
        isSFXChanged = false;
    }

    private void Update()
    {
        textBGMVolume.text = $"BGM\n<size=75%>{bgmVolumeSlider.value}</size>";
        textSFXVolume.text = $"SFX\n<size=75%>{sfxVolumeSlider.value}</size>";

        if (isBGMChanged)
        {
            bgmRaycast.raycastTarget = false;
            sfxRaycast.raycastTarget = true;
            textBGMVolume.color = Color.white;
            textBGMVolume.fontSize = 80;
            textSFXVolume.color = Color.gray;
            textSFXVolume.fontSize = 60;
        }
        else if (isSFXChanged)
        {
            bgmRaycast.raycastTarget = true;
            sfxRaycast.raycastTarget = false;
            textBGMVolume.color = Color.gray;
            textBGMVolume.fontSize = 60;
            textSFXVolume.color = Color.white;
            textSFXVolume.fontSize = 80;
        }

        bgmVolumeSlider.value = PlayerPrefs.GetFloat("BGMVolume", 1) * 100;
        sfxVolumeSlider.value = PlayerPrefs.GetFloat("SFXVolume", 1) * 100;

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

        if (Input.GetKeyDown(KeyCode.Space) && !(Input.GetKey(KeyCode.LeftControl) && Input.GetKey(KeyCode.LeftShift)) && !(Time.timeScale == 0))
        {
            if (isBGMChanged)
            {
                isBGMChanged = false;
                isSFXChanged = true;
            }
            else if (isSFXChanged)
            {
                SceneManager.LoadScene("OptionScene");
            }
        }
    }

    public void VolumeChange()
    {
        if (isBGMChanged)
        {
            PlayerPrefs.SetFloat("BGMVolume", bgmVolumeSlider.value / 100);
        }
        else if (isSFXChanged)
        {
            PlayerPrefs.SetFloat("SFXVolume", sfxVolumeSlider.value / 100);
        }
    }

    IEnumerator VolumeUp()
    {
        while (true)
        {
            if (isBGMChanged)
            {
                bgmVolumeSlider.value++;
            }
            else if (isSFXChanged)
            {
                sfxVolumeSlider.value++;
            }

            yield return new WaitForSeconds(0.05f);
        }
    }

    IEnumerator VolumeDown()
    {
        while (true)
        {
            if (isBGMChanged)
            {
                bgmVolumeSlider.value--;
            }
            else if (isSFXChanged)
            {
                sfxVolumeSlider.value--;
            }

            yield return new WaitForSeconds(0.05f);
        }
    }
}
