using UnityEngine;

[CreateAssetMenu(menuName = "Upgrades/Upgrade")]
public class UpgradeData : ScriptableObject
{
    public string upgradeName;
    public string description;

    [Header("Health")]
    public float maxHP;
    public float hpRegen;
    public float lifeSteal;

    [Header("Combat")]
    public float damage;
    public float meleeDamage;
    public float rangedDamage;
    public float attackSpeed;
    public float critChance;

    [Header("Defense")]
    public float armor;
    public float dodge;

    [Header("Utility")]
    public float speed;
    public float range;
    public float luck;
    public float pickupRange;
}