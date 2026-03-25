using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(PlayerStats))]
public class PlayerTemporaryBuffs : MonoBehaviour
{
    private PlayerStats stats;

    private class ActiveBuff
    {
        public TemporaryBuffData data;
        public float timer;
    }

    private readonly List<ActiveBuff> activeBuffs = new();

    private void Awake()
    {
        stats = GetComponent<PlayerStats>();
    }

    private void Update()
    {
        for (int i = activeBuffs.Count - 1; i >= 0; i--)
        {
            activeBuffs[i].timer -= Time.deltaTime;

            if (activeBuffs[i].timer <= 0f)
            {
                RemoveBuff(activeBuffs[i].data);
                activeBuffs.RemoveAt(i);
            }
        }
    }

    public void ApplyTemporaryBuff(TemporaryBuffData buff)
    {
        if (buff == null) return;

        ApplyBuff(buff);

        activeBuffs.Add(new ActiveBuff
        {
            data = buff,
            timer = buff.duration
        });

        Debug.Log("Buff activado: " + buff.buffName);
    }

    private void ApplyBuff(TemporaryBuffData buff)
    {
        stats.AddTemporaryMoveSpeed(buff.bonusMoveSpeed);
        stats.AddTemporaryDamage(buff.bonusDamage);
        stats.AddTemporaryAttackSpeed(buff.bonusAttackSpeed);
        stats.AddTemporaryArmor(buff.bonusArmor);
        stats.AddTemporaryDashCharges(buff.bonusDashCharges);
    }

    private void RemoveBuff(TemporaryBuffData buff)
    {
        stats.AddTemporaryMoveSpeed(-buff.bonusMoveSpeed);
        stats.AddTemporaryDamage(-buff.bonusDamage);
        stats.AddTemporaryAttackSpeed(-buff.bonusAttackSpeed);
        stats.AddTemporaryArmor(-buff.bonusArmor);
        stats.AddTemporaryDashCharges(-buff.bonusDashCharges);

        Debug.Log("Buff terminado: " + buff.buffName);
    }
}