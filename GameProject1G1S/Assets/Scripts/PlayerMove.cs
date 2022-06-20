using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    [SerializeField] private float moveDelay;
    private StageDrawer stageDrawer;
    private PlayerHP playerHP;
    private LineRenderer lineRenderer;
    private List<Vector3> positionList = new List<Vector3>();
    private int listCnt;
    private float keyTime;
    private float leftMoveSpeed;
    private float rightMoveSpeed;

    public List<Vector3> PositionList => positionList;
    public int ListCnt
    {
        get { return listCnt; }
        set { listCnt = value; }
    }

    private void Awake()
    {
        stageDrawer = GameObject.Find("StageDrawer").GetComponent<StageDrawer>();
        playerHP = GetComponent<PlayerHP>();
        lineRenderer = GameObject.Find("StageDrawer").GetComponent<LineRenderer>();
    }

    private void Start()
    {
        if (stageDrawer.Vertex != 100)
        {
            InvokeRepeating("AutoMove", 0f, moveDelay);
        }
        else
        {
            transform.position = new Vector3(lineRenderer.GetPosition(0).y, lineRenderer.GetPosition(0).x, 0);
        }

        for (int i = 0; i < lineRenderer.positionCount - (stageDrawer.Vertex != 2 ? 1 : 0); i++)
        {
            positionList.Add(lineRenderer.GetPosition(i));
        }
    }

    private void Update()
    {
        if (stageDrawer.Vertex != 100)
        {
            Move();
        }
        else
        {
            MoveApeirogon();
        }
    }

    private void Move()
    {
        if (playerHP.CurrentHP == playerHP.MaxHP)
        {
            if (Input.GetKeyDown(KeyCode.LeftArrow))
            {
                if (listCnt < positionList.Count - 1)
                {
                    listCnt++;
                }
                else
                {
                    listCnt = 0;
                }
            }

            if (Input.GetKeyDown(KeyCode.RightArrow))
            {
                if (listCnt > 0)
                {
                    listCnt--;
                }
                else
                {
                    listCnt = positionList.Count - 1;
                }
            }

            if (Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.RightArrow))
            {
                keyTime += Time.deltaTime;
            }

            if (Input.GetKeyUp(KeyCode.LeftArrow) || Input.GetKeyUp(KeyCode.RightArrow))
            {
                keyTime = 0;
            }

            transform.position = new Vector3(positionList[listCnt].y, positionList[listCnt].x, 0);
        }
        else
        {
            if (Input.GetKeyDown(KeyCode.LeftArrow))
            {
                if (listCnt > 0)
                {
                    listCnt--;
                }
            }

            if (Input.GetKeyDown(KeyCode.RightArrow))
            {
                if (listCnt < positionList.Count - 1)
                {
                    listCnt++;
                }
            }

            if (Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.RightArrow))
            {
                keyTime += Time.deltaTime;
            }

            if (Input.GetKeyUp(KeyCode.LeftArrow) || Input.GetKeyUp(KeyCode.RightArrow))
            {
                keyTime = 0;
            }

            transform.position = new Vector3(-positionList[listCnt].y, positionList[listCnt].x, 0);
        }
    }

    private void AutoMove()
    {
        if (playerHP.CurrentHP == playerHP.MaxHP)
        {
            if (Input.GetKey(KeyCode.LeftArrow) && keyTime >= moveDelay)
            {
                if (listCnt < positionList.Count - 1)
                {
                    listCnt++;
                }
                else
                {
                    listCnt = 0;
                }
            }

            if (Input.GetKey(KeyCode.RightArrow) && keyTime >= moveDelay)
            {
                if (listCnt > 0)
                {
                    listCnt--;
                }
                else
                {
                    listCnt = positionList.Count - 1;
                }
            }
        }
        else
        {
            if (Input.GetKey(KeyCode.LeftArrow) && keyTime >= moveDelay)
            {
                if (listCnt > 0)
                {
                    listCnt--;
                }
            }

            if (Input.GetKey(KeyCode.RightArrow) && keyTime >= moveDelay)
            {
                if (listCnt < positionList.Count - 1)
                {
                    listCnt++;
                }
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

        if (transform.localEulerAngles.z <= 0.001f && ((float)playerHP.CurrentHP / playerHP.MaxHP) * 360 < 360)
        {
            rightMoveSpeed = 0;
        }
        else if (transform.localEulerAngles.z >= ((float)playerHP.CurrentHP / playerHP.MaxHP) * 360)
        {
            leftMoveSpeed = 0;
        }
        else
        {
            leftMoveSpeed = moveDelay;
            rightMoveSpeed = moveDelay;
        }
    }
}
