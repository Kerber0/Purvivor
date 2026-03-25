using UnityEngine;
using TMPro;

public class DashUI : MonoBehaviour
{
    [SerializeField] private PlayerController player;
    [SerializeField] private TextMeshProUGUI dashText;

    void Update()
    {
        if (player == null) return;

        int current = player.GetCurrentDashCharges();
        int max = player.GetMaxDashCharges();

        dashText.text = "Dash: " + current + " / " + max;
    }
}