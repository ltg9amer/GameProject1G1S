using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShooting : MonoBehaviour
{
    [SerializeField] private ObjectPooler bulletPooler;
    [SerializeField] private float shootingDelay;

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            StartCoroutine("Shooting");
        }
        else if (Input.GetMouseButtonUp(0))
        {
            StopCoroutine("Shooting");
        }
    }

    IEnumerator Shooting()
    {
        while (true)
        {
            GameObject bullet = bulletPooler.SpawnObject(transform.position, transform.rotation);
            bullet.GetComponent<Bullet>().MoveDirection = new Vector3(Camera.main.ScreenToWorldPoint(Input.mousePosition).x, Camera.main.ScreenToWorldPoint(Input.mousePosition).y, 0) - bullet.transform.position;
            yield return new WaitForSeconds(shootingDelay);
        }
    }
}
