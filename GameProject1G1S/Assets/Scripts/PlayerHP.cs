using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHP : MonoBehaviour
{
    [SerializeField] private int maxHP;
    private PlayerMove playerMove;
    private int currentHP;
    private float vertexPositionY;

    public int MaxHP => maxHP;
    public int CurrentHP => currentHP;

    private void Start()
    {
        playerMove = GetComponent<PlayerMove>();
        currentHP = maxHP;
        vertexPositionY = playerMove.VertexList[0].y;
    }

    public void TakeDamage(int damage)
    {
        currentHP -= damage;

        if (playerMove.Stage.name == "Stage1")
        {
            playerMove.VertexList.Insert(0, new Vector3(0, (vertexPositionY - playerMove.VertexList[1].y) * ((float)currentHP / maxHP) + playerMove.VertexList[1].y, 0));
            playerMove.VertexList.RemoveAt(1);
        }
        /*else if (playerMove.Stage.name == "Stage10")
        {
            if (transform.localEulerAngles.z >= ((float)currentHP / maxHP) * 360)
            {
                if (transform.localEulerAngles.z >= (360 - ((float)currentHP / maxHP) * 360) / 2 + ((float)currentHP / maxHP) * 360)
                {
                }
                else
                {
                }
            }
        }*/

        //이 코드 되살릴 예정
    }
}
