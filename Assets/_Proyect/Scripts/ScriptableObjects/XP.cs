using UnityEngine;

public class XPOrb : MonoBehaviour
{
    [SerializeField] private int xpAmount = 10;
    private Transform player;
    private PlayerStats stats;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        stats = player.GetComponent<PlayerStats>();
    }

    void Update()
    {
        float dist = Vector2.Distance(transform.position, player.position);

        if (dist < stats.PickupRange)
        {
            transform.position = Vector2.MoveTowards(
                transform.position,
                player.position,
                5f * Time.deltaTime
            );
        }
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collision.CompareTag("Player")) return;

        PlayerXP xp = collision.GetComponent<PlayerXP>();

        if (xp != null)
        {
            xp.AddXP(xpAmount);
        }

        Destroy(gameObject);
    }
}