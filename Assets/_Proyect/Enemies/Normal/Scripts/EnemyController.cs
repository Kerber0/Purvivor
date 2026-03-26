using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class EnemyController : MonoBehaviour
{
    [SerializeField] private float speed = 2f;
    [SerializeField] private CircularMapBounds mapBounds;

    private Transform player;
    private Rigidbody2D rb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();

        if (mapBounds == null)
        {
            mapBounds = FindFirstObjectByType<CircularMapBounds>();
        }
    }

    private void Start()
    {
        GameObject playerObj = GameObject.FindGameObjectWithTag("Player");

        if (playerObj != null)
        {
            player = playerObj.transform;
        }
    }

    private void FixedUpdate()
    {
        if (GameManager.Instance != null && GameManager.Instance.IsGameOver)
        {
            rb.linearVelocity = Vector2.zero;
            return;
        }

        if (player == null)
        {
            rb.linearVelocity = Vector2.zero;
            return;
        }

        Vector2 direction = ((Vector2)player.position - rb.position).normalized;
        rb.linearVelocity = direction * speed;

        if (mapBounds != null)
        {
            rb.position = mapBounds.ClampInsideCircle(rb.position);
        }
    }
}