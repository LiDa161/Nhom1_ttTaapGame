using UnityEngine;
using System.Collections;

public class DraggableSnap : MonoBehaviour
{
    public Transform[] anchorPoints; // Nhiều điểm neo
    public float snapThreshold = 3f;
    public float snapSpeed = 5f;

    private Vector3 offset;
    private bool isDragging = false;
    private bool isSnapped = false;
    private Transform currentSnapPoint = null;

    public bool IsSnapped => isSnapped;

    void OnMouseDown()
    {
        isDragging = true;
        offset = transform.position - GetMouseWorldPos();
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
        Transform closestAnchor = null;

        foreach (var anchor in anchorPoints)
        {
            float dist = Vector3.Distance(transform.position, anchor.position);
            if (dist < closestDist)
            {
                closestDist = dist;
                closestAnchor = anchor;
            }
        }

        if (closestAnchor != null && closestDist <= snapThreshold)
        {
            isSnapped = true;
            currentSnapPoint = closestAnchor;
            transform.position = currentSnapPoint.position;
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

