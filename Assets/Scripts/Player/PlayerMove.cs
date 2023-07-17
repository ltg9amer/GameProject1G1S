using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    [SerializeField] private StageDrawer stageDrawer;
    [SerializeField] private PlayerHP playerHP;
    [SerializeField] private LineRenderer lineRenderer;
    [SerializeField] private float moveDelay;
    private List<Vector3> positionList = new List<Vector3>();
    private int listCnt;

    public List<Vector3> PositionList => positionList;
    public int ListCnt
    {
        get { return listCnt; }
        set { listCnt = value; }
    }

    private void Start()
    {
        transform.position = new Vector3(lineRenderer.GetPosition(0).y, lineRenderer.GetPosition(0).x, 0);

        for (int i = 0; i < lineRenderer.positionCount - (stageDrawer.Vertex != 2 ? 1 : 0); i++)
        {
            positionList.Add(lineRenderer.GetPosition(i));
        }
    }

    private void Update()
    {
        if (!(Time.timeScale == 0))
        {
            if (Input.GetKeyDown(KeyCode.A))
            {
                StartCoroutine("LeftMove");
            }
            else if (Input.GetKeyUp(KeyCode.A))
            {
                StopCoroutine("LeftMove");
            }

            if (Input.GetKeyDown(KeyCode.D))
            {
                StartCoroutine("RightMove");
            }
            else if (Input.GetKeyUp(KeyCode.D))
            {
                StopCoroutine("RightMove");
            }

            if (playerHP.CurrentHP == playerHP.MaxHP)
            {
                transform.position = new Vector3(positionList[listCnt].y, positionList[listCnt].x, 0);
            }
            else
            {
                transform.position = new Vector3(-positionList[listCnt].y, positionList[listCnt].x, 0);
            }
        }
    }

    IEnumerator LeftMove()
    {
        while (true)
        {
            if (playerHP.CurrentHP == playerHP.MaxHP)
            {
                if (listCnt < positionList.Count - 1)
                {
                    listCnt++;
                }
                else
                {
                    listCnt = 0;
                }

                yield return new WaitForSeconds(moveDelay);
            }
            else
            {
                if (listCnt > 0)
                {
                    listCnt--;
                }

                yield return new WaitForSeconds(moveDelay);
            }
        }
    }

    IEnumerator RightMove()
    {
        while (true)
        {
            if (playerHP.CurrentHP == playerHP.MaxHP)
            {
                if (listCnt > 0)
                {
                    listCnt--;
                }
                else
                {
                    listCnt = positionList.Count - 1;
                }

                yield return new WaitForSeconds(moveDelay);
            }
            else
            {
                if (listCnt < positionList.Count - 1)
                {
                    listCnt++;
                }

                yield return new WaitForSeconds(moveDelay);
            }
        }
    }
}
