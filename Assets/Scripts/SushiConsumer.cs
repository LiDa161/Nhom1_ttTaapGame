using UnityEngine;

public class SushiDotReceiver : MonoBehaviour
{
    public SushiShape acceptedShape;
    public static int totalRequired;
    private static int currentScore;

    private void OnTriggerEnter2D(Collider2D other)
    {
        var controller = other.GetComponent<SushiShapeController>();
        if (controller == null) return;

        if (controller.currentShape == acceptedShape)
        {
            Destroy(other.gameObject);
            currentScore++;

            Debug.Log($"Accepted sushi! Score: {currentScore}/{totalRequired}");

            if (currentScore >= totalRequired) // WIN CHECK!!!
            {
                Debug.Log("You win!");
                MoveCounterManager.Instance.StopCountingOnWin();
            }
        }
        else
        {
            Debug.Log("Wrong shape! Letting sushi pass.");
        }
    }

    // Call this once before starting the level to define win condition
    public static void SetRequiredSushi(int total)
    {
        totalRequired = total;
        currentScore = 0;
    }
}