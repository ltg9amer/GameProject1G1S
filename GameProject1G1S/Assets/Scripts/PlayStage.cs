using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayStage : MonoBehaviour
{
    private Image playStage;
    private PlayerHP playerHP;

    private void Start()
    {
        playStage = GetComponent<Image>();
        playerHP = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerHP>();
    }

    private void Update()
    {
        playStage.fillAmount = (float)playerHP.CurrentHP / playerHP.MaxHP;
    }
}