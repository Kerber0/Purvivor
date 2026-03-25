using UnityEngine;

public class AutoAttack : MonoBehaviour
{
    [SerializeField] private float baseFireRate = 1f;

    private float timer;
    private PlayerStats stats;
    private DamageSystem damageSystem;

    void Awake()
    {
        stats = GetComponent<PlayerStats>();
        damageSystem = GetComponent<DamageSystem>();
    }

    void Update()
    {
        timer += Time.deltaTime;

        float fireRate = baseFireRate / stats.AttackSpeed;

        if (timer >= fireRate)
        {
            Shoot();
            timer = 0f;
        }
        Debug.Log("AutoAttack funcionando");
    }

    void Shoot()
    {
        EnemyHealth target = FindClosestEnemy();

        if (target == null) return;

        damageSystem.DealDamage(target, AttackType.Ranged);
    }

    EnemyHealth FindClosestEnemy()
    {
        EnemyHealth[] enemies = FindObjectsByType<EnemyHealth>(FindObjectsSortMode.None);

        float closestDistance = Mathf.Infinity;
        EnemyHealth closest = null;

        foreach (var enemy in enemies)
        {
            float dist = Vector2.Distance(transform.position, enemy.transform.position);

            if (dist < closestDistance)
            {
                closestDistance = dist;
                closest = enemy;
            }
        }

        return closest;
    }
}