using UnityEngine;

public class AttackController : MonoBehaviour
{
    private PlayerAttack manualAttack;
    private AutoAttack autoAttack;
    private bool isAuto = true;
    private DebugUI debugUI;

    private void Awake()
    {
        manualAttack = GetComponent<PlayerAttack>();
        autoAttack = GetComponent<AutoAttack>();
        debugUI = FindFirstObjectByType<DebugUI>();
    }

    private void Start()
    {
        SetAutoAttack(isAuto);
    }

    private void Update()
    {
        if (GameManager.Instance != null && GameManager.Instance.IsGameOver)
            return;

        if (Input.GetKeyDown(KeyCode.T))
        {
            isAuto = !isAuto;
            SetAutoAttack(isAuto);
        }
    }

    private void SetAutoAttack(bool enabled)
    {
        if (autoAttack != null)
            autoAttack.enabled = enabled;

        if (manualAttack != null)
            manualAttack.enabled = !enabled;

        debugUI?.ShowMessage("Modo: " + (enabled ? "AUTO" : "MANUAL"), 2f);
    }
}