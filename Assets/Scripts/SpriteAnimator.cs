using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class SpriteAnimator : MonoBehaviour
{
    public List<Sprite> sprites;         // Danh sách sprite
    public float frameRate = 10f;        // Tốc độ (frame/giây)

    private Image image;                 // Nếu bạn dùng UI (Canvas)
    private SpriteRenderer spriteRenderer; // Nếu bạn dùng SpriteRenderer

    void Start()
    {
        image = GetComponent<Image>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        StartCoroutine(PlayAnimation());
    }

    IEnumerator PlayAnimation()
    {
        float delay = 1f / frameRate;

        for (int i = 0; i < sprites.Count; i++)
        {
            if (image != null)
                image.sprite = sprites[i];
            else if (spriteRenderer != null)
                spriteRenderer.sprite = sprites[i];

            yield return new WaitForSeconds(delay);
        }

        Destroy(gameObject); // Tự xoá sau khi chạy xong
    }
}
