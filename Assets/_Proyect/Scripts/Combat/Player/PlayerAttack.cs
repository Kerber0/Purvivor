using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    private DamageSystem damageSystem;

    void Awake()
    {
        damageSystem = GetComponent<DamageSystem>();
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Attack();
        }
    }

    void Attack()
    {
        EnemyHealth target = GetTargetUnderMouse();

        if (target == null) return;

        damageSystem.DealDamage(target, AttackType.Melee);
    }

    EnemyHealth GetTargetUnderMouse()
    {
        Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Collider2D hit = Physics2D.OverlapPoint(mousePos);

        if (hit == null) return null;

        return hit.GetComponent<EnemyHealth>();
    }
}