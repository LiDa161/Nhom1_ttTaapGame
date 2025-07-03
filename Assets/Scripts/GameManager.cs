using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class GameManager : MonoBehaviour
{
    [Header("Prefabs và Vị trí")]
    public GameObject sushiPrefab;
    public List<Transform> sushiSpawnPoints;
    public List<Transform> conveyorPoints;

    [Header("UI")]
    public Button playPauseButton;
    public Button restartButton;
    public Sprite playSprite;
    public Sprite pauseSprite;

    private GameObject currentSushi;
    private BoxMover currentMover;
    private bool isPlaying = false;
    private int currentLevel = 0;

    void Start()
    {
        playPauseButton.onClick.AddListener(TogglePlayPause);
        restartButton.onClick.AddListener(RestartGame);
        SpawnSushi();
    }

    void TogglePlayPause()
    {
        if (currentMover == null)
        {
            Debug.LogWarning("Không có sushi để điều khiển.");
            return;
        }

        isPlaying = !isPlaying;

        if (isPlaying)
        {
            currentMover.StartMoving();
            playPauseButton.image.sprite = pauseSprite;
        }
        else
        {
            currentMover.StopMoving();
            playPauseButton.image.sprite = playSprite;
        }
    }

    void RestartGame()
    {
        if (currentSushi != null)
        {
            Destroy(currentSushi);
            currentMover = null;
        }

        isPlaying = false;
        playPauseButton.image.sprite = playSprite;
        SpawnSushi();
    }

    void SpawnSushi()
    {
        if (currentLevel >= sushiSpawnPoints.Count)
        {
            Debug.LogError("Không có vị trí spawn cho level hiện tại!");
            return;
        }

        Transform spawnPoint = sushiSpawnPoints[currentLevel];
        currentSushi = Instantiate(sushiPrefab, spawnPoint.position, Quaternion.identity);
        currentMover = currentSushi.GetComponent<BoxMover>();

        if (currentMover != null)
        {
            currentMover.conveyorPoints = conveyorPoints;
        }
        else
        {
            Debug.LogError("Prefab Sushi thiếu BoxMover!");
        }
    }
}
