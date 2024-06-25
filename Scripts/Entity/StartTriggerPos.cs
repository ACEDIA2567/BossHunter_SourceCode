using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartTriggerPos : MonoBehaviour
{
    // �÷��̾ ù �浹 �� �� ���� �� BGM ����
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            GameManager.Instance.FightStart();
            // �ݺ� �������� ����ó����(��Ȱ��ȭ�ص� ��)
            Destroy(gameObject);
        }
    }
}
