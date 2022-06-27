using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class IntroScene : MonoBehaviour
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
        else if (playerMove.ListCnt == 1)
        {
            textMenu.text = "Quit";

            if (Input.GetKeyDown(KeyCode.Space))
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

            if (Input.GetKeyDown(KeyCode.Space))
            {
                SceneManager.LoadScene("OptionScene");
            }
        }
    }
}
