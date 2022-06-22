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
    private float leftMoveSpeed;
    private float rightMoveSpeed;

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
        if (stageDrawer.Vertex != 100)
        {
            if (Input.GetKeyDown(KeyCode.LeftArrow))
            {
                StartCoroutine(LeftMove());
            }
            else if (Input.GetKeyUp(KeyCode.LeftArrow))
            {
                StopAllCoroutines();
            }
            
            if (Input.GetKeyDown(KeyCode.RightArrow))
            {
                StartCoroutine(RightMove());
            }
            else if (Input.GetKeyUp(KeyCode.RightArrow))
            {
                StopAllCoroutines();
            }
        }
        else
        {
            MoveApeirogon();
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
                transform.position = new Vector3(positionList[listCnt].y, positionList[listCnt].x, 0);

                yield return new WaitForSeconds(moveDelay);
            }
            else
            {
                if (listCnt > 0)
                {
                    listCnt--;
                }

                transform.position = new Vector3(-positionList[listCnt].y, positionList[listCnt].x, 0);

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

                transform.position = new Vector3(positionList[listCnt].y, positionList[listCnt].x, 0);

                yield return new WaitForSeconds(moveDelay);
            }
            else
            {
                if (listCnt < positionList.Count - 1)
                {
                    listCnt++;
                }

                transform.position = new Vector3(-positionList[listCnt].y, positionList[listCnt].x, 0);

                yield return new WaitForSeconds(moveDelay);
            }
        }
    }

    private void MoveApeirogon()
    {
        if (Input.GetKey(KeyCode.RightArrow))
        {
            transform.RotateAround(Vector3.zero, Vector3.forward, leftMoveSpeed);
        }

        if (Input.GetKey(KeyCode.LeftArrow))
        {
            transform.RotateAround(Vector3.zero, Vector3.back, rightMoveSpeed);
        }

        if (transform.localEulerAngles.z <= 0.001 && playerHP.CurrentHP != playerHP.MaxHP)
        {
            rightMoveSpeed = 0;
            leftMoveSpeed = moveDelay;
        }
        else if (transform.localEulerAngles.z >= (float)playerHP.CurrentHP / playerHP.MaxHP * 360)
        {
            leftMoveSpeed = 0;
            rightMoveSpeed = moveDelay;
        }
        else
        {
            leftMoveSpeed = moveDelay;
            rightMoveSpeed = moveDelay;
        }
    }
}
