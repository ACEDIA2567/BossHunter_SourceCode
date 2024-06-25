using UnityEngine;

public class WeaponHitBox : MonoBehaviour
{
    [SerializeField] private string targetTag = "Boss";

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag(targetTag))
        {
            // �Ʒ� �ڵ带 enemy�� damage ���� �ɷ� �ٲٸ� ��.
            //TestManager.Instance.HitEnemy();
            if(collision.name == "BigSlash(Clone)")
            {
                BigSlashHitBox bb =  collision.GetComponent<BigSlashHitBox>();
                Debug.Log("����");
                bb.flipX = !bb.flipX;
                bb.targetTag = "Boss";
            }
            else
            {
                RadeManager.Instance.DamageToBoss();
            }
        }
    }
}
