using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] private int damage;
    [SerializeField] private float moveSpeed;
    private Vector3 moveDirection;
    private ObjectPooler enemyPooler;
    private PlayerHP playerHP;
    private Transform deadPlace;

    private void Start()
    {
        enemyPooler = GameObject.Find("EnemySpawner").GetComponent<ObjectPooler>();
        playerHP = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerHP>();
        deadPlace = GameObject.Find("DeadPlace").GetComponent<Transform>();
    }

    private void Update()
    {
        Move();
    }

    private void Move()
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

        if (collision.gameObject.layer == 7)
        {
            playerHP.TakeDamage(damage);
            enemyPooler.ReturnObject(gameObject);
        }

        if (collision.gameObject.layer == 9)
        {
            enemyPooler.ReturnObject(gameObject);
        }
    }
}
