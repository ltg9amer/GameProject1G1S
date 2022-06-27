using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverScene : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI textMenu;
    [SerializeField] private PlayerMove playerMove;

    private void Update()
    {
        if (playerMove.ListCnt == 0)
        {
            textMenu.text = "Stage Select";

            if (Input.GetKeyDown(KeyCode.Space))
            {
                SceneManager.LoadScene("StageSelectScene");
            }
        }
        else
        {
            textMenu.text = "Restart";

            if (Input.GetKeyDown(KeyCode.Space))
            {
                PlayerPrefs.SetInt("StageNumber", PlayerPrefs.GetInt("StageNumber", 1));
                SceneManager.LoadScene("PlayScene");
            }
        }
    }
}
