using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class IntroScene : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI textMenu;
    [SerializeField] private PlayerMove playerMove;

    private void Awake()
    {
        if (!AudioManager.Instance.IsPlayNeon)
        {
            AudioManager.Instance.PlayNeon();
        }
    }

    private void Update()
    {
        if (playerMove.ListCnt == 0)
        {
            textMenu.text = "Stage Select";

            if (Input.GetKeyDown(KeyCode.Space) && !(Input.GetKey(KeyCode.LeftControl) && Input.GetKey(KeyCode.LeftShift)) && !(Time.timeScale == 0))
            {
                SceneManager.LoadScene("StageSelectScene");
            }
        }
        else if (playerMove.ListCnt == 1)
        {
            textMenu.text = "Quit";

            if (Input.GetKeyDown(KeyCode.Space) && !(Input.GetKey(KeyCode.LeftControl) && Input.GetKey(KeyCode.LeftShift)) && !(Time.timeScale == 0))
            {
#if UNITY_EDITOR
                UnityEditor.EditorApplication.isPlaying = false;
#else
                Application.Quit();
#endif
            }
        }
        else
        {
            textMenu.text = "Option";

            if (Input.GetKeyDown(KeyCode.Space) && !(Input.GetKey(KeyCode.LeftControl) && Input.GetKey(KeyCode.LeftShift)) && !(Time.timeScale == 0))
            {
                SceneManager.LoadScene("OptionScene");
            }
        }
    }
}
