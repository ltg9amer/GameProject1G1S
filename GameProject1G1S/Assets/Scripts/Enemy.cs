using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] private float moveSpeed;
    private Vector3 moveDirection;
    private Transform deadPlace;
    private ObjectPooler enemyPooler;

    private void Start()
    {
        deadPlace = GameObject.Find("DeadPlace").GetComponent<Transform>();
        enemyPooler = GameObject.Find("EnemySpawner").GetComponent<ObjectPooler>();
    }

    private void Update()
    {
        moveDirection = deadPlace.position - transform.position;

        moveDirection.Normalize();

        transform.position += moveDirection * moveSpeed * Time.deltaTime;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.name == "DeadPlace")
        {
            enemyPooler.ReturnObject(gameObject);
        }
    }
}
