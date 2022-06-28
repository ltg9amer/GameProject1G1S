using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverScene : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI textScore;
    [SerializeField] private TextMeshProUGUI textMenu;
    [SerializeField] private PlayerMove playerMove;

    private void Awake()
    {
        AudioManager.Instance.PlayNeon();
    }

    private void Update()
    {
        textScore.text = $"Score: {PlayerPrefs.GetInt("CurrentScore", 0)}   High Score: {PlayerPrefs.GetInt($"Stage{PlayerPrefs.GetInt("StageNumber", 1)}HighScore", 0)}";

        if (playerMove.ListCnt == 0)
        {
            textMenu.text = "Stage Select";

            if (Input.GetKeyDown(KeyCode.Space) && !(Input.GetKey(KeyCode.LeftControl) && Input.GetKey(KeyCode.LeftShift)) && !(Time.timeScale == 0))
            {
                SceneManager.LoadScene("StageSelectScene");
            }
        }
        else
        {
            textMenu.text = "Restart";

            if (Input.GetKeyDown(KeyCode.Space) && !(Input.GetKey(KeyCode.LeftControl) && Input.GetKey(KeyCode.LeftShift)) && !(Time.timeScale == 0))
            {
                PlayerPrefs.SetInt("StageNumber", PlayerPrefs.GetInt("StageNumber", 1));
                SceneManager.LoadScene("PlayScene");
            }
        }
    }
}
