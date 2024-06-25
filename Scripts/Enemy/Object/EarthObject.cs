using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EarthObject : MonoBehaviour
{
    private float airborneForce = 5f;
    private float targetTime = 0.3f;
    private float curTime;

    // Update is called once per frame
    void Update()
    {
        curTime += Time.deltaTime;
        if (curTime >= targetTime) 
        { 
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            Rigidbody2D rb = collision.GetComponent<Rigidbody2D>();
            Player player = collision.GetComponent<Player>();
            Transform blockHitBoxTransform = collision.transform.Find("BlockHitBox");
            BoxCollider2D blockHitBoxCollider = blockHitBoxTransform.GetComponent<BoxCollider2D>();
            if (rb != null )
            {
                if (!blockHitBoxCollider.enabled)
                {
                    rb.velocity = new Vector2(rb.velocity.x, airborneForce);
                }
                else
                {
                    Debug.Log("Blocked!");
                }
                RadeManager.Instance.DamageToPlayer(1.5f, player.isBlock);
            }
        }
    }
}
