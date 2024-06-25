using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartTriggerPos : MonoBehaviour
{
    // 플레이어가 첫 충돌 시 적 스폰 및 BGM 변경
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            GameManager.Instance.FightStart();
            // 반복 실행으로 삭제처리함(비활성화해도 됨)
            Destroy(gameObject);
        }
    }
}
