using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [Header("Enemy Spawn")]
    [SerializeField] private GameObject enemyPrefab;
    [SerializeField] private float spawnRate = 2f;

    [Header("Boss")]
    [SerializeField] private GameObject bossPrefab;

    [Header("Map Bounds")]
    [SerializeField] private CircularMapBounds mapBounds;

    [Header("Spawn Settings")]
    [SerializeField] private float bossSpawnRadiusOffset = 2f;
    [SerializeField] private float minDistanceFromPlayer = 5f;

    private float spawnTimer;
    private bool bossSpawned = false;

    private GameTimer gameTimer;
    private Transform player;

    private void Awake()
    {
        gameTimer = FindFirstObjectByType<GameTimer>();

        if (mapBounds == null)
        {
            mapBounds = FindFirstObjectByType<CircularMapBounds>();
        }

        GameObject playerObj = GameObject.FindGameObjectWithTag("Player");
        if (playerObj != null)
        {
            player = playerObj.transform;
        }
    }

    private void Update()
    {
        if (GameManager.Instance != null && GameManager.Instance.IsGameOver)
            return;

        HandleEnemySpawn();
        HandleBossSpawn();
    }

    void HandleEnemySpawn()
    {
        if (bossSpawned) return;

        spawnTimer += Time.deltaTime;

        if (spawnTimer >= spawnRate)
        {
            SpawnEnemy();
            spawnTimer = 0f;
        }
    }

    void SpawnEnemy()
    {
        Vector2 pos = GetRandomPositionInsideCircle();
        Instantiate(enemyPrefab, pos, Quaternion.identity);
    }

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
        Vector2 pos = GetBossSpawnPosition();

        Instantiate(bossPrefab, pos, Quaternion.identity);

        Debug.Log("🔥 BOSS SPAWNEADO 🔥");
    }

    Vector2 GetRandomPositionInsideCircle()
    {
        if (mapBounds == null)
        {
            return transform.position;
        }

        Vector2 center = mapBounds.Center;
        float radius = mapBounds.Radius;

        Vector2 randomPos;
        int attempts = 0;

        do
        {
            randomPos = center + Random.insideUnitCircle * radius;
            attempts++;

            if (player == null) break;
            if (attempts > 30) break;
        }
        while (Vector2.Distance(randomPos, player.position) < minDistanceFromPlayer);

        return randomPos;
    }

    Vector2 GetBossSpawnPosition()
    {
        if (mapBounds == null)
        {
            return transform.position;
        }

        Vector2 center = mapBounds.Center;
        float radius = Mathf.Max(1f, mapBounds.Radius - bossSpawnRadiusOffset);

        Vector2 dir = Random.insideUnitCircle.normalized;
        if (dir == Vector2.zero)
        {
            dir = Vector2.right;
        }

        return center + dir * radius;
    }
}