using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHP : MonoBehaviour
{
    [SerializeField] private int maxHP;
    private PlayerMoveApeirogon playerMoveApeirogon;
    private int currentHP;

    public int MaxHP => maxHP;
    public int CurrentHP => currentHP;

    private void Start()
    {
        currentHP = maxHP;

        if (gameObject.name == "PlayerApeirogon")
        {
            playerMoveApeirogon = GetComponent<PlayerMoveApeirogon>();
        }
    }

    public void TakeDamage(int damage)
    {
        currentHP -= damage;

        playerMoveApeirogon.AutoPositionList.RemoveAt(0);
        playerMoveApeirogon.AutoRotationList.RemoveAt(0);

        if (playerMoveApeirogon != null)
        {
            if (transform.localEulerAngles.z >= ((float)CurrentHP / MaxHP) * 360)
            {
                if (transform.localEulerAngles.z >= (360 - ((float)CurrentHP / MaxHP) * 360) / 2 + ((float)CurrentHP / MaxHP) * 360)
                {
                    transform.position = playerMoveApeirogon.AutoPositionList[playerMoveApeirogon.AutoPositionList.Count - 1];
                    transform.localEulerAngles = playerMoveApeirogon.AutoRotationList[playerMoveApeirogon.AutoRotationList.Count - 1];
                }
                else
                {
                    transform.position = playerMoveApeirogon.AutoPositionList[0];
                    transform.localEulerAngles = playerMoveApeirogon.AutoRotationList[0];
                }
            }
        }
    }
}
