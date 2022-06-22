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
    private int originPositionCount;

    public int MaxHP => maxHP;
    public int CurrentHP => currentHP;

    private void Start()
    {
        currentHP = maxHP;
        originPositionCount = lineRenderer.positionCount - 1;
    }

    public void TakeDamage(int damage)
    {
        currentHP -= damage;

        if (stageDrawer.Vertex != 100)
        {
            if (currentHP != maxHP - maxHP * (float)damage / maxHP)
            {
                if (playerMove.ListCnt == playerMove.PositionList.Count - 1)
                {
                    playerMove.ListCnt--;
                }

                playerMove.PositionList.RemoveAt(playerMove.PositionList.Count - 1);
            }
            else
            {
                if (stageDrawer.Vertex == 2)
                {
                    playerMove.PositionList.RemoveAt(playerMove.PositionList.Count - 1);
                }

                for (int i = 1; i <= (stageDrawer.Vertex != 2 ? Mathf.Ceil((float)stageDrawer.Vertex / 2 - 1) : 0); i++)
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
            }

            lineRenderer.positionCount--;
        }
        else
        {
            lineRenderer.positionCount -= (int)(originPositionCount * ((float)damage / maxHP));

            if (transform.localEulerAngles.z > (float)currentHP / maxHP * 360)
            {
                transform.position = new Vector3(-lineRenderer.GetPosition(lineRenderer.positionCount - 1).y, lineRenderer.GetPosition(lineRenderer.positionCount - 1).x, 0);
                transform.localEulerAngles = new Vector3(0, 0, (float)currentHP / maxHP) * 360;
            }
        }
    }
}
