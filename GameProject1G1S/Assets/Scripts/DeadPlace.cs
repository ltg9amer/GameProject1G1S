using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeadPlace : MonoBehaviour
{
    private Collider2D detect;

    private void Start()
    {
        detect = Physics2D.OverlapCircle(transform.position, 0.1f, 8);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, 0.1f);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == 7)
        {
            Destroy(collision.gameObject); //오브젝트 풀링으로 수정 예정
        }
    }
}
