using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHP : MonoBehaviour
{
    [SerializeField] private StageDrawer stageDrawer;
    [SerializeField] private PlayerMove playerMove;
    [SerializeField] private LineRenderer lineRenderer;
    [SerializeField] private int maxHP;
    private int currentHP;

    public int MaxHP => maxHP;
    public int CurrentHP => currentHP;

    private void Start()
    {
        currentHP = maxHP;
    }

    public void TakeDamage(int damage)
    {
        currentHP -= damage;

        StartCoroutine("OnDamage");

        if (currentHP != maxHP - maxHP * (float)damage / maxHP)
        {
            for (int i = 1; i <= damage; i++)
            {
                if (playerMove.ListCnt == playerMove.PositionList.Count - i)
                {
                    playerMove.ListCnt -= damage - (i - 1);
                }
            }

            for (int i = 1; i <= damage; i++)
            {
                playerMove.PositionList.RemoveAt(playerMove.PositionList.Count - 1);
            }
        }
        else
        {
            for (int i = 1; i <= Mathf.Ceil((float)stageDrawer.Vertex / 2 - 1); i++)
            {
                if (playerMove.ListCnt == i)
                {
                    playerMove.ListCnt = playerMove.PositionList.Count - i;
                }
                else if (playerMove.ListCnt == playerMove.PositionList.Count - i)
                {
                    playerMove.ListCnt = i;
                }
            }

            if (damage > 1)
            {
                for (int i = 1; i < damage; i++)
                {
                    if (playerMove.ListCnt == playerMove.PositionList.Count - i)
                    {
                        playerMove.ListCnt -= damage - i;
                    }
                }

                for (int i = 1; i < damage; i++)
                {
                    playerMove.PositionList.RemoveAt(playerMove.PositionList.Count - 1);
                }
            }
        }

        lineRenderer.positionCount -= damage;
    }

    IEnumerator OnDamage()
    {
        gameObject.GetComponent<SpriteRenderer>().color = Color.red;

        yield return new WaitForSeconds(0.1f);

        gameObject.GetComponent<SpriteRenderer>().color = Color.white;

        StopCoroutine("OnDamage");
    }
}
