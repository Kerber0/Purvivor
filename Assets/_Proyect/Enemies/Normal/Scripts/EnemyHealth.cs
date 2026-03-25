using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    [Header("Health")]
    [SerializeField] private float maxHealth = 30f;

    [Header("Drops")]
    [SerializeField] private GameObject xpPrefab;

    private float currentHealth;

    private DebugUI debugUI;
    private Transform player;
    private PlayerStats playerStats;

    void Awake()
    {
        currentHealth = maxHealth;
        debugUI = FindFirstObjectByType<DebugUI>();
    }

    void Start()
    {
        GameObject playerObj = GameObject.FindGameObjectWithTag("Player");

        if (playerObj != null)
        {
            player = playerObj.transform;
            playerStats = player.GetComponent<PlayerStats>();
        }
    }
    
    // DAMAGE

    public void TakeDamage(int amount)
    {
        ApplyDamage(amount);
    }

    void ApplyDamage(float damage)
    {
        currentHealth -= damage;
        currentHealth = Mathf.Max(currentHealth, 0);

        debugUI?.ShowMessage("Enemy -" + damage, 0.5f);

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    // DEATH

    void Die()
    {
        DropXP();

        debugUI?.ShowMessage("💀 Enemy murió", 1f);

        Destroy(gameObject);
    }

    // DROPS

    void DropXP()
    {
        if (xpPrefab == null) return;

        float luck = playerStats != null ? playerStats.Luck : 0f;

        int dropCount = 1;

        if (Random.value < luck / 100f)
        {
            dropCount++; // bonus por suerte
        }

        for (int i = 0; i < dropCount; i++)
        {
            Instantiate(
                xpPrefab,
                transform.position + (Vector3)Random.insideUnitCircle * 0.3f,
                Quaternion.identity
            );
        }
    }
}