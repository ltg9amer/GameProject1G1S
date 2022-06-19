using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    [SerializeField] private float moveDelay;
    private PlayerHP playerHP;
    private GameObject stage;
    private LineRenderer lineRenderer;
    private int listCnt;
    private float keyTime;
    private float leftMoveSpeed;
    private float rightMoveSpeed;

    public GameObject Stage => stage;

    private void Start()
    {
        playerHP = GetComponent<PlayerHP>();
        stage = GameObject.FindGameObjectWithTag("Stage");
        lineRenderer = GameObject.Find("StageDrawer").GetComponent<LineRenderer>();

        if (stage.name != "Stage10")
        {
            InvokeRepeating("AutoMove", 0f, moveDelay);
        }
    }

    private void Update()
    {
        if (stage.name != "Stage10")
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
                if (listCnt > 0)
                {
                    listCnt--;
                }
                else
                {
                    listCnt = lineRenderer.positionCount - (stage.name != "Stage1" ? 2 : 1);
                }
            }

            if (Input.GetKeyDown(KeyCode.RightArrow))
            {
                if (listCnt < lineRenderer.positionCount - (stage.name != "Stage1" ? 2 : 1))
                {
                    listCnt++;
                }
                else
                {
                    listCnt = 0;
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

            transform.position = new Vector3(lineRenderer.GetPosition(listCnt).y, lineRenderer.GetPosition(listCnt).x, 0);
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
                if (listCnt < lineRenderer.positionCount - (stage.name != "Stage1" ? 1 : 0))
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

            transform.position = new Vector3(lineRenderer.GetPosition(listCnt).y, lineRenderer.GetPosition(listCnt).x, 0);
            //좌표 좌우가 뒤집히는 현상 수정 예정
            //2번째 타격부터 (0, 0, 0)으로 이동하는 현상 수정 예정
        }
    }

    private void AutoMove()
    {
        if (playerHP.CurrentHP == playerHP.MaxHP)
        {
            if (Input.GetKey(KeyCode.LeftArrow) && keyTime >= moveDelay)
            {
                if (listCnt > 0)
                {
                    listCnt--;
                }
                else
                {
                    listCnt = lineRenderer.positionCount - (stage.name != "Stage1" ? 2 : 1);
                }
            }

            if (Input.GetKey(KeyCode.RightArrow) && keyTime >= moveDelay)
            {
                if (listCnt < lineRenderer.positionCount - (stage.name != "Stage1" ? 2 : 1))
                {
                    listCnt++;
                }
                else
                {
                    listCnt = 0;
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
                if (listCnt < lineRenderer.positionCount - (stage.name != "Stage1" ? 1 : 0))
                {
                    listCnt++;
                }
            }
        }
    }

    private void MoveApeirogon()
    {
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            transform.RotateAround(Vector3.zero, Vector3.forward, leftMoveSpeed);
        }

        if (Input.GetKey(KeyCode.RightArrow))
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
