using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShooting : MonoBehaviour
{
    [SerializeField] private StageDrawer stageDrawer;
    [SerializeField] private ObjectPooler bulletPooler;
    [SerializeField] private float shootingDelay;
    private GameObject player;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            StartCoroutine(Shooting());
        }
        else if (Input.GetKeyUp(KeyCode.Space))
        {
            StopAllCoroutines();
        }
    }

    IEnumerator Shooting()
    {
        while (true)
        {
            GameObject bullet = bulletPooler.SpawnObject(transform.position, transform.rotation);
            bullet.GetComponent<Bullet>().MoveDirection = player.transform.position - transform.position;
            yield return new WaitForSeconds(shootingDelay);
        }
    }
}
