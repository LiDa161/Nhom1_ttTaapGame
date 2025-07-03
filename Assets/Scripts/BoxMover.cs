using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class BoxMover : MonoBehaviour
{
    public List<Transform> conveyorPoints;
    public float moveInterval = 0.5f;
    public float moveDuration = 0.2f;

    private int currentIndex = 0;
    private bool isMoving = false;

    public void StartMoving()
    {
        if (isMoving) return;

        if (conveyorPoints == null || conveyorPoints.Count == 0)
        {
            Debug.LogError("BoxMover: conveyorPoints chưa gán!");
            return;
        }

        isMoving = true;
        StartCoroutine(MoveRoutine());
    }

    public void StopMoving()
    {
        isMoving = false;
        StopAllCoroutines();
    }

    IEnumerator MoveRoutine()
    {
        while (isMoving)
        {
            currentIndex = (currentIndex + 1) % conveyorPoints.Count;
            Transform nextPoint = conveyorPoints[currentIndex];
            transform.DOMove(nextPoint.position, moveDuration);
            yield return new WaitForSeconds(moveInterval);
        }
    }
}
