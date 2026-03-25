using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Rigidbody2D rb;
    private Vector2 movement;
    private PlayerStats stats;
    private SpriteRenderer sprite;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        stats = GetComponent<PlayerStats>();
        sprite = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        HandleMovement();
        HandleFlip();
    }

    void HandleMovement()
    {
        movement = new Vector2(
            Input.GetAxisRaw("Horizontal"),
            Input.GetAxisRaw("Vertical")
        ).normalized;
    }

    void HandleFlip() // voltea el sprite según la dirección horizontal
    {
        if (movement.x > 0 && sprite.flipX)
        {
            sprite.flipX = false;
        }
        else if (movement.x < 0 && !sprite.flipX)
        {
            sprite.flipX = true;
        }
    }

    void FixedUpdate()
    {
        rb.MovePosition(rb.position + movement * stats.MoveSpeed * Time.fixedDeltaTime);
    }
}