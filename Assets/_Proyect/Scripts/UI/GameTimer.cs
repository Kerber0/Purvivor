using UnityEngine;

public class GameTimer : MonoBehaviour
{
    [Header("Boss Timing")]
    [SerializeField] private float bossTime = 300f; // 5 minutos
    [SerializeField] private float warningTime = 5f;

    private float timer;
    private bool warned = false;

    private DebugUI debugUI;

    void Awake()
    {
        debugUI = FindFirstObjectByType<DebugUI>();
    }

    void Update()
    {
        timer += Time.deltaTime;

        HandleBossWarning();
    }

    // WARNING

    void HandleBossWarning()
    {
        if (warned) return;

        if (timer >= bossTime - warningTime)
        {
            debugUI?.ShowMessage("⚠️ BOSS INMINENTE ⚠️", 3f);
            warned = true;
        }
    }

    // TIME DISPLAY

    void OnGUI()
    {
        int minutes = Mathf.FloorToInt(timer / 60);
        int seconds = Mathf.FloorToInt(timer % 60);

        string timeText = minutes.ToString("00") + ":" + seconds.ToString("00");

        GUI.Label(new Rect(10, 40, 100, 20), timeText);
    }

    // ACCESS

    public float GetTime()
    {
        return timer;
    }

    public float GetBossTime()
    {
        return bossTime;
    }
}