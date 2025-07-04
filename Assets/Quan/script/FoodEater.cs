using UnityEngine;

public class FoodEater : MonoBehaviour
{
    public string expectedFoodType = "purple";
    public GameObject reactionPrefab;

    public Transform characterRoot; // Gốc của toàn bộ nhân vật (ví dụ: SushiCharacter)

    public void TryEat(string incomingType)
    {
        if (incomingType == expectedFoodType)
        {
            Debug.Log("[FoodEater] Ăn đúng => phản ứng");

            if (reactionPrefab != null)
            {
                Instantiate(reactionPrefab, characterRoot.position, Quaternion.identity);
            }

            Destroy(characterRoot.gameObject); // Xoá toàn bộ nhân vật
        }
        else
        {
            Debug.Log("[FoodEater] Sai loại => không làm gì");
        }
    }
}
