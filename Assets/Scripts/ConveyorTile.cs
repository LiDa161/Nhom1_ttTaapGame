using UnityEngine;

public class ConveyorTile : MonoBehaviour
{
    [Header("Move")]
    public Vector2 moveDirection = Vector2.right;

    [Header("Direction")]
    public float targetRotationZ = 0f;

    public float moveSpeed = 1.5f;

    public Vector3 GetNextPosition(Vector3 currentPos)
    {
        return currentPos + (Vector3)(moveDirection * moveSpeed * Time.deltaTime);
    }
}
