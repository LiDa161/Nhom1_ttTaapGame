using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class BoxMover : MonoBehaviour
{
    public List<Transform> conveyorPoints;
    public float moveInterval = 0.5f;
    public float moveDuration = 0.2f;
    public int startIndexOffset = 0;

    private int currentIndex = 0;
    private bool isMoving = false;
    private bool hasReportedThisStep = false;

    public void StartMoving()
    {
        if (isMoving) return;

        if (conveyorPoints == null || conveyorPoints.Count == 0)
        {
            Debug.LogError("BoxMover: conveyorPoints chưa gán!");
            return;
        }

        currentIndex = startIndexOffset % conveyorPoints.Count;

        isMoving = true;
        StartCoroutine(MoveRoutine());
    }

    public void StopMoving()
    {
        isMoving = false;
        StopAllCoroutines();
    }

    private IEnumerator MoveRoutine()
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