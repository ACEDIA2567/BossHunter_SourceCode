using UnityEngine;

public class BlockHitBox : MonoBehaviour
{
    [SerializeField] private string targetTag = "Enemy";

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag(targetTag))
        {
            // ���Ƴ��� ���� ���¹̳� ����??
        }
    }
}
