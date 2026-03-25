using UnityEngine;

[CreateAssetMenu(menuName = "Buffs/Temporary Buff")]
public class TemporaryBuffData : ScriptableObject
{
    [Header("Info")]
    public string buffName;
    [TextArea] public string description;
    public float duration = 10f;

    [Header("Stats")]
    public float bonusMoveSpeed = 0f;
    public float bonusDamage = 0f;
    public float bonusAttackSpeed = 0f;
    public float bonusArmor = 0f;
    public int bonusDashCharges = 0;
}