using UnityEngine;

public class SushiShapeController : MonoBehaviour
{
    public Sprite squareSprite;
    public Sprite circleSprite;
    public Sprite triangleSprite;

    private SpriteRenderer sr;
    public SushiShape currentShape;

    private void Awake()
    {
        sr = GetComponent<SpriteRenderer>();
    }

    private void Start()
    {
        UpdateSprite(); // Set initial shape
    }

    public void SetShape(SushiShape newShape)
    {
        if (newShape == currentShape) return;

        currentShape = newShape;
        UpdateSprite();
    }

    private void UpdateSprite()
    {
        switch (currentShape)
        {
            case SushiShape.Square:
                sr.sprite = squareSprite;
                break;

            case SushiShape.Circle:
                sr.sprite = circleSprite;
                break;

            case SushiShape.Triangle:
                sr.sprite = triangleSprite;
                break;
        }
    }
}