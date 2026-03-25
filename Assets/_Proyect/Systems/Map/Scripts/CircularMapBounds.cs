using UnityEngine;

public class CircularMapBounds : MonoBehaviour
{
    [SerializeField] private Transform centerPoint;
    [SerializeField] private float radius = 25f;

    public Vector2 Center => centerPoint != null ? (Vector2)centerPoint.position : (Vector2)transform.position;
    public float Radius => radius;

    public Vector2 ClampInsideCircle(Vector2 position)
    {
        Vector2 center = Center;
        Vector2 offset = position - center;

        if (offset.magnitude > radius)
        {
            offset = offset.normalized * radius;
            return center + offset;
        }

        return position;
    }

    public bool IsInsideCircle(Vector2 position)
    {
        return Vector2.Distance(position, Center) <= radius;
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.cyan;

        Vector2 center = centerPoint != null ? centerPoint.position : transform.position;
        const int segments = 64;

        Vector3 prev = center + new Vector2(radius, 0f);

        for (int i = 1; i <= segments; i++)
        {
            float angle = i * Mathf.PI * 2f / segments;
            Vector3 next = center + new Vector2(Mathf.Cos(angle), Mathf.Sin(angle)) * radius;
            Gizmos.DrawLine(prev, next);
            prev = next;
        }
    }
}