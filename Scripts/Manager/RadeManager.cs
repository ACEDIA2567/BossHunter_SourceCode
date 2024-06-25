using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RadeManager : MonoBehaviour
{
    public static RadeManager Instance;

    public Minotaur minotaur;
    public Player player;
    public ParticleSystem bossHitParticle;

    private void Awake()
    {
        if (Instance != null) return;
        else
        {
            Instance = this;
        }
        player = GameManager.Instance.player.GetComponent<Player>();
    }

    public void DamageToBoss()
    {
        float playerPower = player.m_attackPower;
        float bossDefensePower = minotaur.defensePower;
        float damage = CalculateDamage(playerPower, bossDefensePower);
        int crit = Random.Range(0, 10);
        if (crit <= 1)
        {
            damage *= 2;
        }
        minotaur.minoCurHp -= damage;
        BossHPUpdate();

        // particle
        bossHitParticle.transform.position = minotaur.transform.position + new Vector3(0, 1f, 0);
        ParticleSystem.EmissionModule em = bossHitParticle.emission;
        em.SetBurst(0, new ParticleSystem.Burst(0, Mathf.Ceil(playerPower / 200f)));
        ParticleSystem.MainModule mm = bossHitParticle.main;
        mm.startSpeedMultiplier = playerPower / 100f;
        bossHitParticle.Play();
    }

    public void ReflectAttackToBoss()
    {
        float damage = 10000;
        minotaur.minoCurHp -= damage;
        minotaur.defensePower -= 1;
        BossHPUpdate();
    }

    public void DamageToPlayer(float additionalDamage, bool isBlock)
    {
        float bossPower = minotaur.attackPower;
        float damage = CalculateDamage(bossPower, 0);
        damage *= additionalDamage;
        if(isBlock )
        {
            if(player.m_blockKeepTime > 0 && player.m_blockKeepTime <= 0.15f)
            {
                damage = 0;
                ReflectAttackToBoss();
            }
            else
            {
                damage = damage * 0.2f;
            }
        }
        if (player.IsPlayerRolling())
        {
            damage = 0f;
        }
        player.hp -= damage;
        UIManager.Instance.playerHP.fillAmount = player.hp / 100;

        // player hurt animation
        if (player.hp > 0 && damage > 0f)
        {
            player.HurtAnimation();
        }
    }

    private float CalculateDamage(float attackPower, float defensePower)
    {
        float minAttackPower = attackPower * 0.9f;
        float maxAttackPower = attackPower * 1.1f;

        float randomAttackPower = Random.Range(minAttackPower, maxAttackPower);

        float damageReduction = defensePower / 100f;
        float damage = randomAttackPower * (1 - damageReduction);

        return damage;
    }

    private void BossHPUpdate()
    {
        UIManager.Instance.bossHP.fillAmount = minotaur.minoCurHp / minotaur.maxHp;
    }
}
