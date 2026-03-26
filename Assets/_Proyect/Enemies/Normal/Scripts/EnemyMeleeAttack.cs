using UnityEngine;

public class EnemyAttack : MonoBehaviour
{
    [SerializeField] private int damage = 10;
    [SerializeField] private float attackCooldown = 1f;

    private float timer;

    void Update()
    {
        if (GameManager.Instance != null && GameManager.Instance.IsGameOver)
            return;

        timer += Time.deltaTime;
    }

    void OnCollisionStay2D(Collision2D collision)
    {
        if (GameManager.Instance != null && GameManager.Instance.IsGameOver) return;
        if (!collision.gameObject.CompareTag("Player")) return;
        if (timer < attackCooldown) return;

        PlayerHealth playerHealth = collision.gameObject.GetComponent<PlayerHealth>();

        if (playerHealth != null)
        {
            playerHealth.TakeDamage(damage);
        }

        timer = 0f;
    }
}