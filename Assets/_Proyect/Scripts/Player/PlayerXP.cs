using UnityEngine;

public class PlayerXP : MonoBehaviour
{
    [SerializeField] private int currentXP = 0;
    [SerializeField] private int level = 1;
    [SerializeField] private int xpToNextLevel = 50;

    private DebugUI debugUI;

    void Start()
    {
        debugUI = FindFirstObjectByType<DebugUI>();
    }

    public void AddXP(int amount)
    {
        currentXP += amount;

        debugUI?.ShowMessage("XP: +" + amount, 1f);

        if (currentXP >= xpToNextLevel)
        {
            LevelUp();
        }
    }

    void LevelUp()
    {
        level++;
        currentXP = 0;

        debugUI?.ShowMessage("🔥 LEVEL UP! " + level, 2f);

        xpToNextLevel += 25; 
    }
}