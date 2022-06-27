using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] private StageData stageData;
    [SerializeField] private float moveSpeed;
    private Vector3 moveDirection;
    private ObjectPooler bulletPooler;

    public Vector3 MoveDirection
    {
        get { return moveDirection; }
        set { moveDirection = value; }
    }

    private void Start()
    {
        bulletPooler = GameObject.Find("DeadPlace").GetComponent<ObjectPooler>();
    }

    private void Update()
    {
        Move();
        PositionDestroy();
    }

    private void Move()
    {
        transform.position += moveDirection * moveSpeed * Time.deltaTime;
    }

    private void PositionDestroy()
    {
        if (transform.position.x > stageData.LimitMax.x || transform.position.x < stageData.LimitMin.x || transform.position.y > stageData.LimitMax.y || transform.position.y < stageData.LimitMin.y)
        {
            bulletPooler.ReturnObject(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == 8)
        {
            bulletPooler.ReturnObject(gameObject);
        }
    }
}
