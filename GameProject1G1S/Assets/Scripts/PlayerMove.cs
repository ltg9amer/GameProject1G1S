using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    [SerializeField] List<Vector3> vertexList = new List<Vector3>();
    [SerializeField] private float moveDelay;
    int listCnt;
    float keyTime;

    private void Start()
    {
        InvokeRepeating("Move", 0f, moveDelay);
    }

    private void Update()
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

    private void Move()
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
}
