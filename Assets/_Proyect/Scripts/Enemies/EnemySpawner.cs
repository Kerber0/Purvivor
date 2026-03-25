using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [Header("Enemy Spawn")]
    [SerializeField] private GameObject enemyPrefab;
    [SerializeField] private float spawnRate = 2f;

    [Header("Boss")]
    [SerializeField] private GameObject bossPrefab;

    [Header("Spawn Area")]
    [SerializeField] private float spawnRangeX = 8f;
    [SerializeField] private float spawnRangeY = 4f;

    private float spawnTimer;
    private bool bossSpawned = false;

    private GameTimer gameTimer;

    void Awake()
    {
        gameTimer = FindFirstObjectByType<GameTimer>();
    }

    void Update()
    {
        HandleEnemySpawn();
        HandleBossSpawn();
    }

    // NORMAL ENEMIES

    void HandleEnemySpawn()
    {
        if (bossSpawned) return; // Deja de spawnear enemigos normales cuando el boss aparecio

        spawnTimer += Time.deltaTime;

        if (spawnTimer >= spawnRate)
        {
            SpawnEnemy();
            spawnTimer = 0f;
        }
    }

    void SpawnEnemy()
    {
        Vector2 pos = GetRandomPosition();

        Instantiate(enemyPrefab, pos, Quaternion.identity);
    }

    // BOSS

    void HandleBossSpawn()
    {
        if (bossSpawned) return;
        if (gameTimer == null) return;

        if (gameTimer.GetTime() >= gameTimer.GetBossTime())
        {
            SpawnBoss();
            bossSpawned = true;
        }
    }

    void SpawnBoss()
    {
        Vector2 pos = GetRandomPosition();

        Instantiate(bossPrefab, pos, Quaternion.identity);

        Debug.Log("🔥 BOSS SPAWNEADO 🔥");
    }

    // POSITION

    Vector2 GetRandomPosition()
    {
        float x = Random.Range(-spawnRangeX, spawnRangeX);
        float y = Random.Range(-spawnRangeY, spawnRangeY);

        return new Vector2(x, y);
    }
}