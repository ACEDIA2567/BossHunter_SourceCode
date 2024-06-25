using System.Collections;
using System.Threading;
using System.Threading.Tasks;
using TMPro;
using UnityEngine;
public class Minotaur : Monster
{
    private float minoMaxHp = 200000f;
    public float minoCurHp;
    public float minoAttackPower = 20f;
    public float minoDefensePower = 3f;
    private float minoSpeed = 2f;
    private float attackCoolDown = 3f;
    private float coolDown;
    public GameObject player;
    public Player playerComponent;
    public bool isMoving = true;
    public bool isAttacking = false;
    private bool isInitDone = false;
    private bool isBigSlashDone = false;
    private Animator animator;
    private SpriteRenderer spriteRenderer;
    [SerializeField] private GameObject earthObject;
    [SerializeField] private GameObject slashHitBox;
    [SerializeField] private GameObject initHitBox;
    [SerializeField] private GameObject bigSlashObject;
    private int bigSlashCount = 0;
    // ¿”Ω√
    [SerializeField] private TextMeshProUGUI curHpText;
    private void Start()
    {
        maxHp = minoMaxHp;
        minoCurHp = maxHp;
        attackPower = minoAttackPower;
        defensePower = minoDefensePower;
        speed = minoSpeed;
        currentHp = maxHp;
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        player = GameObject.FindGameObjectWithTag("Player");
        playerComponent = player.GetComponent<Player>();
    }
    private void Update()
    {
        float distance = Vector3.Distance(transform.position, player.transform.position);
        if (!isInitDone)
        {
            if (distance <= 10f)
            {
                InitReady();
            }
        }
        if ((minoCurHp / maxHp) < 0.4f && !isBigSlashDone)
        {
            BigReady();
        }
        if (!isAttacking && defensePower <= 0)
        {
            DestroyArmor();
        }
        if (!isAttacking)
        {
            Search();
        }
        coolDown += Time.deltaTime;
        if (player != null && coolDown >= attackCoolDown && !isAttacking)
        {
            coolDown = 0;
            DoAction();
        }
        if (isAttacking && isMoving)
        {
            isAttacking = false;
            isMoving = false;
        }
        if (minoCurHp <= 0)
        {
            Death();
        }
    }
    public override void DoAction()
    {
        int randint = Random.Range(0, 3);
        base.DoAction();
        if (randint == 0)
        {
            WindMillReady();
        }
        else if (randint == 1)
        {
            EarthCrashReady();
        }
        else if (randint == 2)
        {
            SlashReady();
        }
        else
        {
            Debug.Log("π∫∞° ¿ﬂ∏¯µ ");
        }
    }
    private void SpriteFlip()
    {
        if (player.transform.position.x > transform.position.x)
        {
            spriteRenderer.flipX = false;
        }
        else
        {
            spriteRenderer.flipX = true;
        }
    }
    private void Search()
    {
        isMoving = true;
        SpriteFlip();
        if (isMoving && player != gameObject)
        {
            float distance = Vector3.Distance(transform.position, player.transform.position);
            if (distance > 5.0f)
            {
                transform.position = Vector3.MoveTowards(transform.position, player.transform.position, speed * Time.deltaTime);
                animator.SetBool("isRun", true);
                distance = Vector3.Distance(transform.position, player.transform.position);
                if (distance <= 5.0f)
                {
                    isMoving = false;
                    animator.SetBool("isRun", false);
                }
            }
            else
            {
                isMoving = false;
                animator.SetBool("isRun", false);
            }
        }
        else
        {
            Debug.Log("8 ¿Ã«œ");
        }
    }
    // DestroyArmor
    private void DestroyArmor()
    {
        isAttacking = true;
        animator.SetBool("isDestroyArmor", true);
    }
    private void RepairArmor()
    {
        animator.SetBool("isDestroyArmor", false);
        animator.SetBool("isRepairArmor", true);
    }
    private void RepairArmorEnd()
    {
        defensePower = minoDefensePower;
        animator.SetBool("isRepairArmor", false);
        isAttacking = false;
    }
    // EarthCrash
    private void EarthCrashReady()
    {
        isAttacking = true;
        animator.SetBool("isEarthCrashReady", true);
    }
    private void EarthCrash()
    {
        animator.SetBool("isEarthCrashReady", false);
        animator.SetBool("isEarthCrash", true);
        StartCoroutine(EarthObjectCoroutine());
    }
    private IEnumerator EarthObjectCoroutine()
    {
        Vector3 earthPosition = new Vector3(transform.position.x, transform.position.y + 0.3f, transform.position.z);
        Vector3 forwardEarthPosition = earthPosition;
        Vector3 backEarthPosition = earthPosition;
        Vector3 addPosition = new Vector3(2.5f, 0, 0);
        for (int i = 0; i < 5; i++)
        {
            forwardEarthPosition += addPosition;
            backEarthPosition -= addPosition;
            GameObject fo = Instantiate(earthObject, transform);
            fo.transform.position = forwardEarthPosition;
            GameObject bo = Instantiate(earthObject, transform);
            bo.transform.position = backEarthPosition;
            yield return new WaitForSeconds(0.3f);
        }
    }
    private void EarthCrashEnd()
    {
        isAttacking = false;
        animator.SetBool("isEarthCrash", false);
    }
    //WindMill
    private void WindMillReady()
    {
        isAttacking = true;
        animator.SetBool("isWindMillReady", true);
    }
    private void WindMill()
    {
        animator.SetBool("isWindMillReady", false);
        animator.SetBool("isWindMill", true);
        float distance = Vector3.Distance(transform.position, player.transform.position);
        if (distance < 4)
        {
            Rigidbody2D rb = player.GetComponent<Rigidbody2D>();
            Transform blockHitBoxTransform = player.transform.Find("BlockHitBox");
            BoxCollider2D blockHitBoxCollider = blockHitBoxTransform.GetComponent<BoxCollider2D>();
            if (!blockHitBoxCollider.enabled)
            {
                Vector2 knockbackForce;
                if (spriteRenderer.flipX)
                {
                    knockbackForce = new Vector2(-10f, 5f);
                }
                else
                {
                    knockbackForce = new Vector2(10f, 5f);
                }
                rb.AddForce(knockbackForce, ForceMode2D.Impulse);
            }
            else
            {
                Debug.Log("Blocked!");
            }
            RadeManager.Instance.DamageToPlayer(1.5f, playerComponent.isBlock);
        }
    }
    private void WindMillEnd()
    {
        isAttacking = false;
        animator.SetBool("isWindMill", false);
    }
    // Slash
    private void SlashReady()
    {
        isAttacking = true;
        animator.SetBool("isSlashReady", true);
    }
    private void Slash()
    {
        animator.SetBool("isSlashReady", false);
        animator.SetBool("isSlash", true);
    }
    private void SlashMove()
    {
        slashHitBox.SetActive(true);
        Vector3 targetPosition;
        if (spriteRenderer.flipX)
        {
            targetPosition = new Vector3(transform.position.x - 10, transform.position.y, transform.position.z);
        }
        else
        {
            targetPosition = new Vector3(transform.position.x + 10, transform.position.y, transform.position.z);
        }
        transform.position = Vector3.MoveTowards(transform.position, targetPosition, (speed));
    }
    private void SlashEnd()
    {
        isAttacking = false;
        animator.SetBool("isSlash", false);
    }
    // Init
    private void InitReady()
    {
        isAttacking = true;
        isInitDone = true;
        animator.SetBool("isInitReady", true);
    }
    private void InitPattern()
    {
        animator.SetBool("isInitReady", false);
        animator.SetBool("isInitPattern", true);
        transform.position = player.transform.position + new Vector3(0, 10, 0);
        initHitBox.SetActive(true);
    }
    private void InitEnd()
    {
        animator.SetBool("isInitPattern", false);
        isAttacking = false;
    }
    // BigSlash
    private void BigReady()
    {
        isAttacking = true;
        isBigSlashDone = true;
        animator.SetBool("isBigReady", true);
    }
    private void BigSlashReady()
    {
        animator.SetBool("isBigReady", false);
        animator.SetBool("isBigSlashReady", true);
    }
    private void BigSlash()
    {
        if (bigSlashCount < 3)
        {
            bigSlashCount++;
            animator.SetBool("isBigSlash", true);
            GameObject go = Instantiate(bigSlashObject);
            go.transform.position = transform.position;
            animator.SetBool("isBigSlash", false);
        }
        else
        {
            BigSlashEnd();
        }
    }
    private void BigSlashEnd()
    {
        animator.SetBool("isBigSlash", false);
        animator.SetBool("isBigSlashReady", false);
        isAttacking = false;
    }
    // Death
    private void Death()
    {
        animator.SetBool("isDeath", true);
        isAttacking = true;
        StartCoroutine(GameManager.Instance.PlayerWin());
    }
}