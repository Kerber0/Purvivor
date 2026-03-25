using UnityEngine;

public class DamageSystem : MonoBehaviour
{
    private PlayerStats stats;
    private PlayerHealth playerHealth;
    private DebugUI debugUI;

    void Awake()
    {
        stats = GetComponent<PlayerStats>();
        playerHealth = GetComponent<PlayerHealth>();
        debugUI = FindFirstObjectByType<DebugUI>();
    }


    public void DealDamage(EnemyHealth target, AttackType type)
    {
        if (target == null) return;

        float distance = Vector2.Distance(transform.position, target.transform.position);

        if (distance > stats.Range) return;

        float damage = CalculateDamage(type);

        target.TakeDamage((int)damage);

        OnDamageDealt(damage);
    }


    float CalculateDamage(AttackType type)
    {
        float damage = stats.Damage;

        if (type == AttackType.Melee)
        {
            damage += stats.MeleeDamage;
        }
        else if (type == AttackType.Ranged)
        {
            damage += stats.RangedDamage;
        }

        if (IsCriticalHit())
        {
            damage *= 2f;
            debugUI?.ShowMessage("CRIT!", 2f);
        }

        return damage;
    }

    bool IsCriticalHit()
    {
        return Random.value < stats.CritChance / 100f;
    }


    void OnDamageDealt(float damage)
    {
        ApplyLifeSteal(damage);
    }

    void ApplyLifeSteal(float damage)
    {
        if (stats.LifeSteal <= 0) return;

        float heal = damage * (stats.LifeSteal / 100f);
        playerHealth.Heal(heal);
    }
}