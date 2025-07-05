using UnityEngine;
using System.Collections;

public class DraggableSnap : MonoBehaviour
{
    public Transform[] anchorPoints;
    public float snapThreshold = 1f;
    public float snapSpeed = 5f;
    public AnchorSlot[] anchorSlots; // Phải tương ứng với anchorPoints

    private Vector3 offset;
    private bool isDragging = false;
    private bool isSnapped = false;
    private Transform currentSnapPoint = null;
    private AnchorSlot currentSlot = null;

    public bool IsSnapped => isSnapped;

    void OnMouseDown()
    {
        isDragging = true;
        offset = transform.position - GetMouseWorldPos();

        // Clear slot cũ nếu có
        currentSlot?.ClearCharacter();
        currentSlot = null;
    }

    void OnMouseDrag()
    {
        if (isDragging)
        {
            transform.position = GetMouseWorldPos() + offset;
        }
    }

    void OnMouseUp()
    {
        isDragging = false;

        float closestDist = float.MaxValue;
        int closestIndex = -1;

        for (int i = 0; i < anchorPoints.Length; i++)
        {
            float dist = Vector3.Distance(transform.position, anchorPoints[i].position);
            if (dist < closestDist)
            {
                closestDist = dist;
                closestIndex = i;
            }
        }

        if (closestIndex != -1 && closestDist <= snapThreshold)
        {
            isSnapped = true;
            currentSnapPoint = anchorPoints[closestIndex];
            currentSlot = anchorSlots[closestIndex];

            transform.position = currentSnapPoint.position;
            currentSlot?.AssignCharacter(gameObject);
        }
        else
        {
            isSnapped = false;
            StartCoroutine(SmoothSnapBack());
        }
    }

    Vector3 GetMouseWorldPos()
    {
        Vector3 screenPos = Input.mousePosition;
        screenPos.z = Camera.main.WorldToScreenPoint(transform.position).z;
        return Camera.main.ScreenToWorldPoint(screenPos);
    }

    IEnumerator SmoothSnapBack()
    {
        if (currentSnapPoint == null)
            yield break;

        while (Vector3.Distance(transform.position, currentSnapPoint.position) > 0.01f)
        {
            transform.position = Vector3.Lerp(transform.position, currentSnapPoint.position, Time.deltaTime * snapSpeed);
            yield return null;
        }

        transform.position = currentSnapPoint.position;
        isSnapped = true;
    }
}
