using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BigSlashHitBox : MonoBehaviour
{
    public string targetTag = "Player";
    private float existTime;
    public bool flipX;
    GameObject player;
    SpriteRenderer spriteRenderer;

    private void Start()
    {
        targetTag = "Player";
        existTime = 0;
        player = GameObject.FindGameObjectWithTag("Player");
        spriteRenderer = GetComponent<SpriteRenderer>();
    }
    private void Update()
    {
        existTime += Time.deltaTime;
        if (existTime >= 5f)
        {
            Destroy(gameObject);
        }
        Move();
    }

    private void SpriteFlip()
    {
        if (player.transform.position.x > transform.position.x)
        {
            spriteRenderer.flipX = flipX ? true : false; // 방향 반전을 고려한 스프라이트 플립
        }
        else
        {
            spriteRenderer.flipX = flipX ? false : true; // 방향 반전을 고려한 스프라이트 플립
        }
    }

    private void Move()
    {
        SpriteFlip();

        Vector3 direction = (player.transform.position - transform.position).normalized;
        if (flipX)
        {
            direction = -direction; // 방향 반전 적용
        }

        // 오브젝트를 이동시킵니다.
        transform.position = Vector3.MoveTowards(transform.position, transform.position + direction, 10 * Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag(targetTag))
        {
            if(targetTag == "Player")
            {
                Player player = collision.GetComponent<Player>();
                RadeManager.Instance.DamageToPlayer(1.5f, false);
                Destroy(gameObject);
            }
            else if(targetTag == "Boss")
            {
                Minotaur minotaur = collision.GetComponent<Minotaur>();
                RadeManager.Instance.ReflectAttackToBoss();
                Destroy(gameObject);
            }
            
        }
    }
}
