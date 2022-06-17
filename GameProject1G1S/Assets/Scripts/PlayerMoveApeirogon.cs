using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMoveApeirogon : MonoBehaviour
{

    [SerializeField] private List<Vector3> autoPositionList = new List<Vector3>();
    [SerializeField] private List<Vector3> autoRotationList = new List<Vector3>();
    [SerializeField] private float moveSpeed;
    private PlayerHP playerHP;
    private float leftMoveSpeed;
    private float rightMoveSpeed;

    public List<Vector3> AutoPositionList => autoPositionList;
    public List<Vector3> AutoRotationList => autoRotationList;

    private void Start()
    {
        playerHP = GetComponent<PlayerHP>();
    }

    private void Update()
    {
        Move();
    }

    private void Move()
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
            leftMoveSpeed = moveSpeed;
            rightMoveSpeed = moveSpeed;
        }
    }
}
