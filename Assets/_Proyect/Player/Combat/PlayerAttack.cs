using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    private DamageSystem damageSystem;
    private Animator animator;

    private void Awake()
    {
        damageSystem = GetComponent<DamageSystem>();
        animator = GetComponent<Animator>();
    }

    private void Update()
    {
        if (GameManager.Instance != null && GameManager.Instance.IsGameOver)
            return;

        if (Input.GetMouseButtonDown(0))
        {
            Attack();
        }
    }

    private void Attack()
    {
        // Dispara animación aunque no le pegues a nadie
        if (animator != null)
        {
            animator.SetTrigger("Attack");
        }

        EnemyHealth target = GetTargetUnderMouse();

        if (target == null) return;

        damageSystem.DealDamage(target, AttackType.Melee);
    }

    private EnemyHealth GetTargetUnderMouse()
    {
        Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Collider2D hit = Physics2D.OverlapPoint(mousePos);

        if (hit == null) return null;

        return hit.GetComponent<EnemyHealth>();
    }
}