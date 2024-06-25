using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossHitBox : MonoBehaviour
{
    [SerializeField] private string targetTag = "Player";
    private float existTime;

    private void OnEnable()
    {
        existTime = 0;
    }
    private void Update()
    {
        existTime += Time.deltaTime;
        if(existTime >= 0.05f)
        {
            gameObject.SetActive(false);
        }
    }
    
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag(targetTag))
        {
            Player player = collision.GetComponent<Player>();
            RadeManager.Instance.DamageToPlayer(0.4f, player.isBlock);
        }
    }
}
