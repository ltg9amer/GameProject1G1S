using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayStageApeirogon : MonoBehaviour
{
    private Image playStageApeirogon;
    private PlayerHP playerHP;

    private void Start()
    {
        playStageApeirogon = GetComponent<Image>();
        playerHP = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerHP>();
    }

    private void Update()
    {
        playStageApeirogon.fillAmount = (float)playerHP.CurrentHP / playerHP.MaxHP;
    }
}