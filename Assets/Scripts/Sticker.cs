using UnityEngine;

public enum SushiShape
{ Square, Circle, Triangle }

public class ShapeSticker : MonoBehaviour
{
    public SushiShape shapeType;

    private void OnTriggerEnter2D(Collider2D other)
    {
        SushiShapeController controller = other.GetComponent<SushiShapeController>();
        if (controller != null && controller.currentShape != shapeType)
        {
            controller.SetShape(shapeType);
        }
    }
}