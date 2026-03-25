using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    [Header("Health")]
    [SerializeField] private float maxHP = 100f;
    [SerializeField] private float hpRegen = 0f;
    [SerializeField] private float lifeSteal = 0f;

    [Header("Combat")]
    [SerializeField] private float damage = 10f;
    [SerializeField] private float meleeDamage = 0f;
    [SerializeField] private float rangedDamage = 0f;
    [SerializeField] private float attackSpeed = 1f;
    [SerializeField] private float critChance = 0f;

    [Header("Defense")]
    [SerializeField] private float armor = 0f;
    [SerializeField] private float dodge = 0f;

    [Header("Utility")]
    [SerializeField] private float speed = 5f;
    [SerializeField] private float range = 10f;
    [SerializeField] private float luck = 0f;
    [SerializeField] private float pickupRange = 1f;

    [Header("Dash")]
    [SerializeField] private int maxDashCharges = 1;

    // Buffs temporales
    private float tempMoveSpeed = 0f;
    private float tempDamage = 0f;
    private float tempAttackSpeed = 0f;
    private float tempArmor = 0f;
    private int tempDashCharges = 0;

    public void ApplyUpgrade(UpgradeData upgrade)
    {
        maxHP += upgrade.maxHP;
        hpRegen += upgrade.hpRegen;
        lifeSteal += upgrade.lifeSteal;

        damage += upgrade.damage;
        meleeDamage += upgrade.meleeDamage;
        rangedDamage += upgrade.rangedDamage;
        attackSpeed += upgrade.attackSpeed;
        critChance += upgrade.critChance;

        armor += upgrade.armor;
        dodge += upgrade.dodge;

        speed += upgrade.speed;
        range += upgrade.range;
        luck += upgrade.luck;
        pickupRange += upgrade.pickupRange;

        maxDashCharges += upgrade.extraDashCharges;

        ClampStats();
    }

    private void ClampStats()
    {
        critChance = Mathf.Clamp(critChance, 0f, 100f);
        dodge = Mathf.Clamp(dodge, 0f, 100f);
        attackSpeed = Mathf.Max(attackSpeed, 0.1f);
        maxHP = Mathf.Max(maxHP, 1f);
        speed = Mathf.Max(speed, 0f);
        pickupRange = Mathf.Max(pickupRange, 0f);
        range = Mathf.Max(range, 0f);
        maxDashCharges = Mathf.Max(maxDashCharges, 1);
    }

    // Métodos para buffs temporales
    public void AddTemporaryMoveSpeed(float value)
    {
        tempMoveSpeed += value;
    }

    public void AddTemporaryDamage(float value)
    {
        tempDamage += value;
    }

    public void AddTemporaryAttackSpeed(float value)
    {
        tempAttackSpeed += value;
    }

    public void AddTemporaryArmor(float value)
    {
        tempArmor += value;
    }

    public void AddTemporaryDashCharges(int value)
    {
        tempDashCharges += value;
    }

    // Movement
    public float MoveSpeed => speed + tempMoveSpeed;

    // Health
    public float MaxHP => maxHP;
    public float HPRegen => hpRegen;
    public float LifeSteal => lifeSteal;

    // Combat
    public float Damage => damage + tempDamage;
    public float MeleeDamage => meleeDamage;
    public float RangedDamage => rangedDamage;
    public float AttackSpeed => attackSpeed + tempAttackSpeed;
    public float CritChance => critChance;

    // Defense
    public float Armor => armor + tempArmor;
    public float Dodge => dodge;

    // Utility
    public float Range => range;
    public float Luck => luck;
    public float PickupRange => pickupRange;

    // Dash
    public int MaxDashCharges => maxDashCharges + tempDashCharges;
}