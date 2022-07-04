using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class DeveloperCode : MonoBehaviour
{
    public static DeveloperCode Instance;

    [SerializeField] private TMP_InputField inputDeveloperCode;
    private bool isOpened;
    private bool phaseSkip;

    public bool IsOpened => isOpened;
    public bool PhaseSkip
    {
        get { return phaseSkip; }
        set { phaseSkip = value; }
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

        PlayerPrefs.DeleteKey("BGM1Volume");
        PlayerPrefs.DeleteKey("BGM2Volume");
        PlayerPrefs.DeleteKey("BGM3Volume");
    }

    private void Update()
    {
        if (((Input.GetKey(KeyCode.LeftControl) && Input.GetKey(KeyCode.LeftShift) && Input.GetKeyDown(KeyCode.Space)) || (Input.GetKey(KeyCode.LeftControl) && Input.GetKeyDown(KeyCode.LeftShift) && Input.GetKey(KeyCode.Space)) || (Input.GetKeyDown(KeyCode.LeftControl) && Input.GetKey(KeyCode.LeftShift) && Input.GetKey(KeyCode.Space))) && !isOpened)
        {
            if (!AudioManager.Instance.IsPlayNeon)
            {
                AudioManager.Instance.PlayNeon();
            }
            
            isOpened = true;
            inputDeveloperCode.GetComponent<RectTransform>().DOAnchorPosY(-30, 1f).SetUpdate(true);
            inputDeveloperCode.text = "";
            Time.timeScale = 0.0f;
        }
        else if (((Input.GetKey(KeyCode.LeftControl) && Input.GetKey(KeyCode.LeftShift) && Input.GetKeyDown(KeyCode.Space)) || (Input.GetKey(KeyCode.LeftControl) && Input.GetKeyDown(KeyCode.LeftShift) && Input.GetKey(KeyCode.Space)) || (Input.GetKeyDown(KeyCode.LeftControl) && Input.GetKey(KeyCode.LeftShift) && Input.GetKey(KeyCode.Space))) && isOpened)
        {
            isOpened = false;
            inputDeveloperCode.GetComponent<RectTransform>().DOAnchorPosY(60, 1f).SetUpdate(true);
            Time.timeScale = 1.0f;
        }
    }

    public void codeReader()
    {
        int intValue;
        bool boolValue;
        string[] codeReaderArray;
        codeReaderArray = inputDeveloperCode.text.Split(' ');

        if (codeReaderArray[0] == "stageOpened" && codeReaderArray[1] == "=" && int.TryParse(codeReaderArray[2], out intValue) && codeReaderArray.Length == 3)
        {
            if (intValue <= 10 && intValue >= 0)
            {
                PlayerPrefs.SetInt("StageOpened", intValue);
            }
        }

        if (codeReaderArray[0] == "bgmVolume" && codeReaderArray[1] == "=" && int.TryParse(codeReaderArray[2], out intValue) && codeReaderArray.Length == 3)
        {
            if (intValue <= 100 && intValue >= 0)
            {
                PlayerPrefs.SetFloat($"BGMVolume", (float)intValue / 100);
            }
        }

        if (codeReaderArray[0] == "sfxVolume" && codeReaderArray[1] == "=" && int.TryParse(codeReaderArray[2], out intValue) && codeReaderArray.Length == 3)
        {
            if (intValue <= 100 && intValue >= 0)
            {
                PlayerPrefs.SetFloat($"SFXVolume", (float)intValue / 100);
            }
        }

        if (codeReaderArray[0] == "phaseSkip" && codeReaderArray[1] == "=" && (int.TryParse(codeReaderArray[2], out intValue) || codeReaderArray[2] == "true" || codeReaderArray[2] == "false") && codeReaderArray.Length == 3)
        {
            if (intValue == 1 || codeReaderArray[2] == "true")
            {
                phaseSkip = true;
            }
            else if (intValue == 0 || codeReaderArray[2] == "false")
            {
                phaseSkip = false;
            }
        }

        if (codeReaderArray[0] == "quit" && codeReaderArray.Length == 1)
        {
#if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
#else
                Application.Quit();
#endif
        }

        if ((codeReaderArray[0] == "ÀÌÅÂ°ï" || codeReaderArray[0] == "¹ÚÇö¼­" || codeReaderArray[0] == "°æµ¿¿±" || codeReaderArray[0] == "¹®Çö¿õ" || codeReaderArray[0] == "¹ÚÇö¿í" || codeReaderArray[0] == "¿ÀÁöÈ«" || codeReaderArray[0] == "ÀÌµµÀ±" || codeReaderArray[0] == "ÀÌÁØÀÌ" || codeReaderArray[0] == "È«ÀÎ±â") && codeReaderArray.Length == 1)
        { 
            if (!GameManager.Instance)
            {
                SceneManager.LoadScene("CreditScene");
            }
        }

        inputDeveloperCode.text = "";
        isOpened = false;
        inputDeveloperCode.GetComponent<RectTransform>().DOAnchorPosY(60, 1f).SetUpdate(true);
        Time.timeScale = 1.0f;
    }
}
