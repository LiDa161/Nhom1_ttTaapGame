using UnityEngine;

public class FoodPoint : MonoBehaviour
{
    public AnchorSlot targetAnchorSlot;          // Slot được liên kết với FoodPoint
    public string requiredFoodType = "1";   // Món ăn đúng loại (chỉ dùng nếu cố định)
    public GameObject reactionPrefab;            // Prefab hiệu ứng khi ăn thành công

    public bool TryFeedWithFood(string foodType)
    {
        if (targetAnchorSlot == null || !targetAnchorSlot.HasCharacter())
        {
            Debug.Log("[FoodPoint] ❌ Không có nhân vật tại điểm neo!");
            return false;
        }

        GameObject character = targetAnchorSlot.GetCharacter();
        string characterType = character.tag;

        Debug.Log($"[FoodPoint] 🟨 Kiểm tra tag nhân vật: {characterType} vs foodType: {foodType}");

        if (characterType == foodType)
        {
            Debug.Log("[FoodPoint] ✅ Nhân vật ăn thành công!");

            if (reactionPrefab != null)
            {
                Instantiate(reactionPrefab, character.transform.position, Quaternion.identity);
            }

            Destroy(character);
            targetAnchorSlot.ClearCharacter();
            return true;
        }
        else
        {
            Debug.Log("[FoodPoint] ❌ Nhân vật không ăn được loại này!");
            return false;
        }
    }


}
