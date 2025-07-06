using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class StickerDragHandler : MonoBehaviour
{
    private Vector3 offset;
    private bool isDragging = false;

    private Vector3 originalRandomOffset;
    private Collider2D col;

    public float snapRadius = 1f;
    public LayerMask conveyorLayer;
    public LayerMask forbiddenLayer; // dot layer, where stickers must NOT go

    private void Start()
    {
        col = GetComponent<Collider2D>();
        originalRandomOffset = transform.position; // store spawn position for reset
    }

    private void OnMouseDown()
    {
        isDragging = true;
        offset = transform.position - GetMouseWorldPos();
        col.enabled = false; // prevent sushi from touching during drag
    }

    private void OnMouseDrag()
    {
        if (isDragging)
        {
            transform.position = GetMouseWorldPos() + offset;
        }
    }

    private void OnMouseUp()
    {
        isDragging = false;
        col.enabled = true;

        TrySnapToConveyor();
    }

    private Vector3 GetMouseWorldPos()
    {
        Vector3 mouse = Input.mousePosition;
        mouse.z = 10f;
        return Camera.main.ScreenToWorldPoint(mouse);
    }

    private void TrySnapToConveyor()
    {
        Collider2D[] hits = Physics2D.OverlapCircleAll(transform.position, snapRadius, conveyorLayer);

        if (hits.Length == 0)
        {
            ReturnToReady();
            return;
        }

        // Find nearest conveyor tile
        Transform closest = hits[0].transform;
        float closestDist = Vector2.Distance(transform.position, closest.position);

        foreach (var hit in hits)
        {
            float dist = Vector2.Distance(transform.position, hit.transform.position);
            if (dist < closestDist)
            {
                closest = hit.transform;
                closestDist = dist;
            }
        }

        // Check for forbidden objects at that location
        Collider2D overlap = Physics2D.OverlapPoint(closest.position, forbiddenLayer);
        if (overlap != null)
        {
            ReturnToReady();
            return;
        }

        // Safe to snap
        transform.position = closest.position;
    }

    private void ReturnToReady()
    {
        transform.position = originalRandomOffset;
    }

    // Called by GameManager on restart
    public void ResetToReady()
    {
        transform.position = originalRandomOffset;
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, snapRadius); // just show snap radius now
    }
}