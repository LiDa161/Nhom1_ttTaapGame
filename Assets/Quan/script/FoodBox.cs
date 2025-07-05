using UnityEngine;

public class FoodBox : MonoBehaviour
{
    public string foodType = "purple"; // Loại của đồ ăn này

    private void OnTriggerEnter2D(Collider2D other)
    {
        FoodPoint foodPoint = other.GetComponent<FoodPoint>();

        if (foodPoint != null)
        {
            bool wasEaten = foodPoint.TryFeedWithFood(foodType);

            if (wasEaten)
            {
                Destroy(gameObject); // Chỉ xoá FoodBox nếu ăn thành công
            }
        }
    }

}
