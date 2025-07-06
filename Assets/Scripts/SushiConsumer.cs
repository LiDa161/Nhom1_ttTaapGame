using UnityEngine;

public class SushiConsumer : MonoBehaviour
{
    public static int totalRequired;
    private static int currentScore;
    public float checkRadius = 1.5f;

    private void OnTriggerEnter2D(Collider2D other)
    {
        var sushi = other.GetComponent<SushiShapeController>();
        if (sushi == null) return;

        SushiShape incomingShape = sushi.currentShape;
        bool shouldDestroy = false;

        // 1. Kiểm tra nhân vật xung quanh
        Collider2D[] nearbyCharacters = Physics2D.OverlapCircleAll(transform.position, checkRadius);
        foreach (var col in nearbyCharacters)
        {
            var character = col.GetComponent<Character>();
            if (character != null && character.characterShape == incomingShape)
            {
                shouldDestroy = true;
                Destroy(character.gameObject);
            }
        }

        // 2. Nếu có nhân vật phù hợp -> Xử lý tất cả đồ ăn cùng loại
        if (shouldDestroy)
        {
            // Tìm tất cả đồ ăn cùng loại đang trong trigger
            Collider2D[] allSushiInArea = Physics2D.OverlapCircleAll(transform.position, checkRadius);
            foreach (var sushiCol in allSushiInArea)
            {
                var s = sushiCol.GetComponent<SushiShapeController>();
                if (s != null && s.currentShape == incomingShape)
                {
                    Destroy(sushiCol.gameObject);
                    currentScore++;
                }
            }

            Debug.Log($"Destroyed all {incomingShape}! Score: {currentScore}/{totalRequired}");

            if (currentScore >= totalRequired)
            {
                Debug.Log("You win!");
                MoveCounterManager.Instance.StopCountingOnWin();
            }
        }
    }

    public static void SetRequiredSushi(int total)
    {
        totalRequired = total;
        currentScore = 0;
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.magenta;
        Gizmos.DrawWireSphere(transform.position, checkRadius);
    }
}