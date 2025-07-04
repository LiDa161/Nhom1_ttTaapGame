using UnityEngine;

public class FoodBox : MonoBehaviour
{
    public string foodType = "purple";

    private void OnTriggerEnter2D(Collider2D other)
    {
        var eater = other.GetComponent<FoodEater>();
        if (eater != null)
        {
            eater.TryEat(foodType);
        }
    }
}
