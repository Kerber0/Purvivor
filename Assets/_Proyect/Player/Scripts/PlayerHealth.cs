using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    [Header("Invulnerability")]
    [SerializeField] private float invulnerabilityTime = 2f;

    [Header("References")]
    [SerializeField] private SpriteRenderer sprite;
    [SerializeField] private HealthBar healthBar;

    private float currentHealth;
    private bool isInvulnerable;
    private float invulTimer;
    private bool isDead = false;

    private PlayerStats stats;
    private DebugUI debugUI;

    public bool IsDead => isDead;

    void Awake()
    {
        stats = GetComponent<PlayerStats>();
        debugUI = FindFirstObjectByType<DebugUI>();

        if (sprite == null)
            sprite = GetComponent<SpriteRenderer>();

        currentHealth = stats.MaxHP;

        if (healthBar != null)
            healthBar.SetMaxHealth(stats.MaxHP);
    }

    void Update()
    {
        if (isDead) return;

        HandleInvulnerability();
        HandleRegen();
    }

    public void TakeDamage(int amount)
    {
        if (isDead) return;
        if (!CanTakeDamage()) return;

        float finalDamage = CalculateFinalDamage(amount);

        ApplyDamage(finalDamage);

        ActivateInvulnerability();
    }

    bool CanTakeDamage()
    {
        if (isInvulnerable) return false;

        if (TryDodge()) return false;

        return true;
    }

    bool TryDodge()
    {
        if (Random.value < stats.Dodge / 100f)
        {
            debugUI?.ShowMessage("💨 Dodge!", 1f);
            return true;
        }
        return false;
    }

    float CalculateFinalDamage(float damage)
    {
        return damage * (100f / (100f + stats.Armor));
    }

    void ApplyDamage(float damage)
    {
        currentHealth -= damage;
        currentHealth = Mathf.Max(currentHealth, 0);

        healthBar?.SetHealth(currentHealth);

        debugUI?.ShowMessage("🔥 -" + damage.ToString("F1"), 1f);

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    public void Heal(float amount)
    {
        if (isDead) return;

        currentHealth += amount;
        currentHealth = Mathf.Min(currentHealth, stats.MaxHP);

        healthBar?.SetHealth(currentHealth);

        debugUI?.ShowMessage("💚 +" + amount.ToString("F1"), 1f);
    }

    void HandleRegen()
    {
        if (stats.HPRegen <= 0) return;

        currentHealth += stats.HPRegen * Time.deltaTime;
        currentHealth = Mathf.Min(currentHealth, stats.MaxHP);

        healthBar?.SetHealth(currentHealth);
    }

    void ActivateInvulnerability()
    {
        isInvulnerable = true;
        invulTimer = 0f;
    }

    void HandleInvulnerability()
    {
        if (!isInvulnerable) return;

        invulTimer += Time.deltaTime;

        if (sprite != null)
            sprite.enabled = !sprite.enabled;

        if (invulTimer >= invulnerabilityTime)
        {
            isInvulnerable = false;

            if (sprite != null)
                sprite.enabled = true;
        }
    }

    void Die()
    {
        if (isDead) return;

        isDead = true;

        debugUI?.ShowMessage("💀 Player murió", 2f);

        Animator animator = GetComponent<Animator>();
        if (animator != null)
        {
            animator.SetTrigger("Death");
        }

        if (GameManager.Instance != null)
        {
            GameManager.Instance.SetPlayerDead();
        }
    }
}