using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DeveloperCode : MonoBehaviour
{
    [SerializeField] private TMP_InputField inputDeveloperCode;
    private bool isOpened;

    public bool IsOpened => isOpened;

    private void Update()
    {
        if (((Input.GetKey(KeyCode.LeftControl) && Input.GetKey(KeyCode.LeftShift) && Input.GetKeyDown(KeyCode.Space)) || (Input.GetKey(KeyCode.LeftControl) && Input.GetKeyDown(KeyCode.LeftShift) && Input.GetKey(KeyCode.Space)) || (Input.GetKeyDown(KeyCode.LeftControl) && Input.GetKey(KeyCode.LeftShift) && Input.GetKey(KeyCode.Space))) && !isOpened)
        {
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
        int value;
        string[] codeReaderArray;
        codeReaderArray = inputDeveloperCode.text.Split(' ');

        if (codeReaderArray[0] == "stageOpened" && codeReaderArray[1] == "=" && int.TryParse(codeReaderArray[2], out value))
        {
            if (value <= 10 || value >= 0)
            {
                PlayerPrefs.SetInt("StageOpened", value);
            }
            else
            {
                PlayerPrefs.SetInt("StageOpened", PlayerPrefs.GetInt("StageOpened", 1));
            }
        }

        value = 0;

        for (int i = 0; i < codeReaderArray.Length; i++)
        {
            codeReaderArray[i] = null;
        }

        inputDeveloperCode.text = "";
    }
}
