using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHP : MonoBehaviour
{
    [SerializeField] private int maxHP;
    private PlayerMove playerMove;
    private LineRenderer lineRenderer;
    private int currentHP;

    public int MaxHP => maxHP;
    public int CurrentHP => currentHP;

    private void Start()
    {
        playerMove = GetComponent<PlayerMove>();
        lineRenderer = GameObject.Find("StageDrawer").GetComponent<LineRenderer>();
        currentHP = maxHP;
    }

    public void TakeDamage(int damage)
    {
        currentHP -= damage;

        if (playerMove.Stage.name != "Stage10")
        {
            lineRenderer.positionCount--;
        }
        else
        {
            lineRenderer.positionCount -= 10;
            //10 대신 10이 나오는 식을 구할 예정
        }
    }
}
