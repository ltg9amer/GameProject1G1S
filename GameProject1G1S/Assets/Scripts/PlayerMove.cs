using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    [SerializeField] private List<Vector3> vertexList = new List<Vector3>();
    [SerializeField] private float moveDelay;
    private PlayerHP playerHP;
    private GameObject stage;
    private int listCnt;
    private float keyTime;
    private float leftMoveSpeed;
    private float rightMoveSpeed;

    public List<Vector3> VertexList => vertexList;
    public GameObject Stage => stage;

    private void Start()
    {
        playerHP = GetComponent<PlayerHP>();
        stage = GameObject.FindGameObjectWithTag("Stage");

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
        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            if (listCnt == 0)
            {
                listCnt = vertexList.Count - 1;
            }
            else
            {
                listCnt--;
            }
        }

        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            if (listCnt == vertexList.Count - 1)
            {
                listCnt = 0;
            }
            else
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

        transform.position = vertexList[listCnt];
    }

    private void AutoMove()
    {
        if (Input.GetKey(KeyCode.LeftArrow) && keyTime >= moveDelay)
        {
            if (listCnt == 0)
            {
                listCnt = vertexList.Count - 1;
            }
            else
            {
                listCnt--;
            }
        }

        if (Input.GetKey(KeyCode.RightArrow) && keyTime >= moveDelay)
        {
            if (listCnt == vertexList.Count - 1)
            {
                listCnt = 0;
            }
            else
            {
                listCnt++;
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
