using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class OptionScene : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI textMenu;
    [SerializeField] private PlayerMove playerMove;

    private void Update()
    {
        if (playerMove.ListCnt == 0)
        {
            textMenu.text = "Back";

            if (Input.GetKeyDown(KeyCode.Space))
            {
                SceneManager.LoadScene("IntroScene");
            }
        }
        else if (playerMove.ListCnt == 1)
        {
            if (Input.GetKey(KeyCode.Space))
            {
                textMenu.text = "Coming Soon";
            }
            else
            {
                textMenu.text = "Volume";
            }
        }
        else if (playerMove.ListCnt == 2)
        {
            textMenu.text = "How to Play";

            if (Input.GetKeyDown(KeyCode.Space))
            {
                SceneManager.LoadScene("HowToPlayScene");
            }
        }
        else
        {
            textMenu.text = "Credit";

            if (Input.GetKeyDown(KeyCode.Space))
            {
                SceneManager.LoadScene("CreditScene");
            }
        }
    }
}
