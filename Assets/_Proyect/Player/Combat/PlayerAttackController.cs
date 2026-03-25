using UnityEngine;

public class AttackController : MonoBehaviour
{
    private PlayerAttack manualAttack;
    private AutoAttack autoAttack;
    private bool isAuto = true;
    private DebugUI debugUI;

    void Awake()
    {
        manualAttack = GetComponent<PlayerAttack>();
        autoAttack = GetComponent<AutoAttack>();
        debugUI = FindFirstObjectByType<DebugUI>();
    }

    void Start()
    {
        SetAutoAttack(isAuto);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.T))
        {
            isAuto = !isAuto;
            SetAutoAttack(isAuto);
        }
    }

    void SetAutoAttack(bool enabled)
    {
        autoAttack.enabled = enabled;
        manualAttack.enabled = !enabled;

        debugUI?.ShowMessage("Modo: " + (enabled ? "AUTO" : "MANUAL"), 2f);
    }
}

