using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StageSelectScene : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI textStage;
    [SerializeField] private PlayerMove playerMove;

    private void Update()
    {
        if (playerMove.ListCnt != 0)
        {
            if (playerMove.ListCnt <= PlayerPrefs.GetInt("StageOpened", 1))
            {
                textStage.text = $"Stage {playerMove.ListCnt}";

                if (Input.GetKeyDown(KeyCode.Space))
                {
                    PlayerPrefs.SetInt("StageNumber", playerMove.ListCnt);
                    SceneManager.LoadScene("PlayScene");
                }
            }
            else
            {
                if (Input.GetKey(KeyCode.Space))
                {
                    textStage.text = "Locked Stage";
                }
                else
                {
                    textStage.text = $"Stage {playerMove.ListCnt}";
                }
            }
        }
        else
        {
            textStage.text = "Back";

            if (Input.GetKeyDown(KeyCode.Space))
            {
                SceneManager.LoadScene("IntroScene");
            }
        }
    }
}
