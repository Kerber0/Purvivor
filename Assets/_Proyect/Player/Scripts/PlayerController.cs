using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("Dash")]
    [SerializeField] private float dashSpeed = 12f;
    [SerializeField] private float dashDuration = 0.15f;
    [SerializeField] private float dashRechargeTime = 1f;
    [SerializeField] private KeyCode dashKey = KeyCode.Space;
    [SerializeField] private CircularMapBounds mapBounds;

    private Rigidbody2D rb;
    private Vector2 movement;
    private Vector2 lastDirection = Vector2.right;
    private Vector2 dashDirection;
    private PlayerStats stats;
    private SpriteRenderer sprite;

    private bool isDashing = false;
    private float dashTimer = 0f;

    private int currentDashCharges;
    private int lastKnownMaxDashCharges;
    private float dashRechargeTimer = 0f;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        stats = GetComponent<PlayerStats>();
        sprite = GetComponent<SpriteRenderer>();
        mapBounds = GetComponent<CircularMapBounds>();
    }

    void Start()
    {
        currentDashCharges = stats.MaxDashCharges;
        lastKnownMaxDashCharges = stats.MaxDashCharges;
    }

    void Update()
    {
        HandleMovement();
        HandleFlip();
        HandleDashInput();
        HandleDashTimers();
        SyncDashChargesWithStats();
    }

    void HandleMovement()
    {
        if (isDashing) return;

        movement = new Vector2(
            Input.GetAxisRaw("Horizontal"),
            Input.GetAxisRaw("Vertical")
        ).normalized;

        if (movement != Vector2.zero)
        {
            lastDirection = movement;
        }
    }

    void HandleFlip()
    {
        Vector2 visualDirection = isDashing ? dashDirection : movement;

        if (visualDirection.x > 0 && sprite.flipX)
        {
            sprite.flipX = false;
        }
        else if (visualDirection.x < 0 && !sprite.flipX)
        {
            sprite.flipX = true;
        }
    }

    void HandleDashInput()
    {
        if (isDashing) return;
        if (currentDashCharges <= 0) return;

        if (Input.GetKeyDown(dashKey))
        {
            StartDash();
        }
    }

    void StartDash()
    {
        isDashing = true;
        dashTimer = dashDuration;

        dashDirection = movement != Vector2.zero ? movement : lastDirection;

        currentDashCharges--;

        if (currentDashCharges < stats.MaxDashCharges)
        {
            dashRechargeTimer = dashRechargeTime;
        }
    }

    void HandleDashTimers()
    {
        if (isDashing)
        {
            dashTimer -= Time.deltaTime;

            if (dashTimer <= 0f)
            {
                isDashing = false;
            }
        }

        if (currentDashCharges < stats.MaxDashCharges)
        {
            dashRechargeTimer -= Time.deltaTime;

            if (dashRechargeTimer <= 0f)
            {
                currentDashCharges++;

                if (currentDashCharges < stats.MaxDashCharges)
                {
                    dashRechargeTimer = dashRechargeTime;
                }
            }
        }
    }

    void SyncDashChargesWithStats()
    {
        if (stats.MaxDashCharges > lastKnownMaxDashCharges)
        {
            int difference = stats.MaxDashCharges - lastKnownMaxDashCharges;
            currentDashCharges += difference;
        }

        if (currentDashCharges > stats.MaxDashCharges)
        {
            currentDashCharges = stats.MaxDashCharges;
        }

        lastKnownMaxDashCharges = stats.MaxDashCharges;
    }

   void FixedUpdate()
    {
        Vector2 targetPosition;

        if (isDashing)
        {
            targetPosition = rb.position + dashDirection * dashSpeed * Time.fixedDeltaTime;
        }
        else
        {
            targetPosition = rb.position + movement * stats.MoveSpeed * Time.fixedDeltaTime;
        }

        if (mapBounds != null)
        {
            targetPosition = mapBounds.ClampInsideCircle(targetPosition);
        }

        rb.MovePosition(targetPosition);
    }

    public int GetCurrentDashCharges()
    {
        return currentDashCharges;
    }

    public int GetMaxDashCharges()
    {
        return stats.MaxDashCharges;
    }

    public bool IsDashing()
    {
        return isDashing;
    }
}