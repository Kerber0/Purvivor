using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    private bool isPlayerDead = false;
    private bool isGameOver = false;

    public bool IsPlayerDead => isPlayerDead;
    public bool IsGameOver => isGameOver;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
    }

    public void SetPlayerDead()
    {
        if (isPlayerDead) return;

        isPlayerDead = true;
        isGameOver = true;

        Debug.Log("GameManager: Player muerto, game over.");
    }
}