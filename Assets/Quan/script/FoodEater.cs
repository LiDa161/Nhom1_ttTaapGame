using UnityEngine;

public class FoodEater : MonoBehaviour
{
    public string expectedFoodType = "purple";
    public GameObject reactionPrefab;
    public Transform characterRoot;

    public void TryEat(string incomingType)
    {
        if (incomingType == expectedFoodType)
        {
            Debug.Log("[FoodEater] Ăn đúng => phản ứng");

            if (reactionPrefab != null)
            {
                Instantiate(reactionPrefab, characterRoot.position, Quaternion.identity);
            }

            Destroy(characterRoot.gameObject);
        }
        else
        {
            Debug.Log("[FoodEater] Sai loại => không làm gì");
        }
    }
}


