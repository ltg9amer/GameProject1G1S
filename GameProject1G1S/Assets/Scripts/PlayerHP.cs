using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHP : MonoBehaviour
{
    [SerializeField] private int maxHP;
    private StageDrawer stageDrawer;
    private PlayerMove playerMove;
    private LineRenderer lineRenderer;
    private int currentHP;
    private int originPositionCount;

    public int MaxHP => maxHP;
    public int CurrentHP => currentHP;

    private void Start()
    {
        stageDrawer = GameObject.Find("StageDrawer").GetComponent<StageDrawer>();
        playerMove = GetComponent<PlayerMove>();
        lineRenderer = GameObject.Find("StageDrawer").GetComponent<LineRenderer>();
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
            if (currentHP != maxHP - maxHP * (float)damage / maxHP)
            {
                lineRenderer.positionCount -= (int)(originPositionCount * ((float)damage / maxHP));
            }
            else
            {
                lineRenderer.positionCount -= (int)(originPositionCount * ((float)damage / maxHP));
                lineRenderer.positionCount--;
            }

            if (transform.localEulerAngles.z > 360 * ((float)currentHP / maxHP))
            {
                if (transform.localEulerAngles.z > (360 - ((float)CurrentHP / MaxHP) * 360) / 2 + ((float)CurrentHP / MaxHP) * 360)
                {
                    transform.position = new Vector3(lineRenderer.GetPosition(0).y, lineRenderer.GetPosition(0).x, 0);
                    transform.localEulerAngles = new Vector3(0, 0, 0);
                }
                else
                {
                    transform.position = new Vector3(-lineRenderer.GetPosition(lineRenderer.positionCount - 1).y, lineRenderer.GetPosition(lineRenderer.positionCount - 1).x, 0);
                    transform.localEulerAngles = new Vector3(0, 0, 360 * ((float)currentHP / maxHP));
                }
            }
        }
    }
}
