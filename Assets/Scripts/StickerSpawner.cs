using System.Collections.Generic;
using UnityEngine;

public class StickerSpawnArea : MonoBehaviour
{
    public List<GameObject> stickerPrefabs; // assign all 3 types here (square, circle, triangle)
    public Vector2 areaSize = new Vector2(4, 2); // size of spawn zone

    private void Start()
    {
        SpawnEachSticker();
    }

    private void SpawnEachSticker()
    {
        foreach (var prefab in stickerPrefabs)
        {
            Vector2 pos = GetRandomPositionInArea();
            Instantiate(prefab, pos, Quaternion.identity, transform);
        }
    }

    private Vector2 GetRandomPositionInArea()
    {
        Vector2 center = transform.position;
        float x = Random.Range(-areaSize.x / 2, areaSize.x / 2);
        float y = Random.Range(-areaSize.y / 2, areaSize.y / 2);
        return center + new Vector2(x, y);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireCube(transform.position, areaSize);
    }
}