using UnityEngine;

public class SushiDotReceiver : MonoBehaviour
{
    [Header("Receiver Settings")]
    public SushiShape acceptedShape;

    public static int totalRequired = 0;
    private static int currentScore = 0;

    public event System.Action OnSushiConsumed;

    private void OnTriggerEnter2D(Collider2D other)
    {
        var controller = other.GetComponent<SushiShapeController>();
        if (controller == null) return;

        if (controller.currentShape == acceptedShape)
        {
            HandleCorrectSushi(other.gameObject);
        }
        else
        {
            Debug.Log("Sushi ignored due to incorrect shape.");
        }
    }

    private void HandleCorrectSushi(GameObject sushi)
    {
        Destroy(sushi);
        currentScore++;
        Debug.Log($"Accepted sushi! Score: {currentScore}/{totalRequired}");

        OnSushiConsumed?.Invoke();

        if (currentScore >= totalRequired)
        {
            TriggerWin();
        }
    }

    private void TriggerWin()
    {
        Debug.Log("You win!");
        MoveCounterManager.Instance.StopCountingOnWin();
    }

    /// <summary>
    /// Call this at the beginning of the level to set the required number of correct sushi.
    /// </summary>
    public static void SetRequiredSushi(int total)
    {
        totalRequired = total;
        currentScore = 0;
    }
}