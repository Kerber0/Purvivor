using UnityEngine;

public class AutoAttack : MonoBehaviour
{
    [SerializeField] private float baseFireRate = 1f;

    private float timer;
    private PlayerStats stats;
    private DamageSystem damageSystem;
    private Animator animator;

    private void Awake()
    {
        stats = GetComponent<PlayerStats>();
        damageSystem = GetComponent<DamageSystem>();
        animator = GetComponent<Animator>();
    }

    private void Update()
    {
        if (GameManager.Instance != null && GameManager.Instance.IsGameOver)
            return;

        timer += Time.deltaTime;

        float fireRate = baseFireRate / stats.AttackSpeed;

        if (timer >= fireRate)
        {
            Shoot();
            timer = 0f;
        }
    }

    private void Shoot()
    {
        EnemyHealth target = FindClosestEnemy();

        if (target == null) return;

        if (animator != null)
        {
            animator.SetTrigger("Attack");
        }

        damageSystem.DealDamage(target, AttackType.Ranged);
    }

    private EnemyHealth FindClosestEnemy()
    {
        EnemyHealth[] enemies = FindObjectsByType<EnemyHealth>(FindObjectsSortMode.None);

        float closestDistance = Mathf.Infinity;
        EnemyHealth closest = null;

        foreach (EnemyHealth enemy in enemies)
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