using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class SushiSpawnInfo
{
    public GameObject sushiPrefab;
    public Transform spawnPoint;
}

public class GameManager : MonoBehaviour
{
    [Header("Setup")]
    public List<SushiSpawnInfo> sushiSpawns;

    public List<Transform> conveyorPoints;
    public float spawnDelay = 0.5f;

    [Header("UI")]
    public Button playPauseButton;

    public Button restartButton;
    public Sprite playSprite;
    public Sprite pauseSprite;

    private List<GameObject> currentSushis = new List<GameObject>();
    private bool isPlaying = false;
    private Coroutine spawnRoutine;
    private Coroutine gameTickRoutine;

    private void Start()
    {
        playPauseButton.onClick.AddListener(TogglePlayPause);
        restartButton.onClick.AddListener(RestartGame);

        SpawnAllSushiWithSpacing();
    }

    private void TogglePlayPause()
    {
        isPlaying = !isPlaying;

        if (isPlaying)
        {
            foreach (var sushi in currentSushis)
                sushi.GetComponent<BoxMover>().StartMoving();

            gameTickRoutine = StartCoroutine(GameTickRoutine()); // start ticking
            playPauseButton.image.sprite = pauseSprite;
        }
        else
        {
            foreach (var sushi in currentSushis)
                sushi.GetComponent<BoxMover>().StopMoving();

            if (gameTickRoutine != null) StopCoroutine(gameTickRoutine); // stop ticking
            playPauseButton.image.sprite = playSprite;
        }
    }

    private IEnumerator GameTickRoutine()
    {
        while (isPlaying)
        {
            MoveCounterManager.Instance?.RegisterMove();
            yield return new WaitForSeconds(0.5f);
        }
    }

    public void RestartGame()
    {
        if (spawnRoutine != null)
            StopCoroutine(spawnRoutine);

        MoveCounterManager.Instance?.ResetCounter();

        foreach (var sushi in currentSushis)
        {
            if (sushi)
                Destroy(sushi);
        }

        currentSushis.Clear();
        isPlaying = false;
        playPauseButton.image.sprite = playSprite;

        // Reset all stickers to ready area
        foreach (var sticker in Object.FindObjectsByType<StickerDragHandler>(FindObjectsSortMode.None))
        {
            sticker.ResetToReady();
        }

        // Respawn
        SpawnAllSushiWithSpacing();

        RobotManager.Instance?.RespawnAllCustomers();
    }

    private void SpawnAllSushiWithSpacing()
    {
        spawnRoutine = StartCoroutine(SpawnSushiOneByOne());
        SushiDotReceiver.SetRequiredSushi(sushiSpawns.Count);
    }

    private IEnumerator SpawnSushiOneByOne()
    {
        int indexOffset = 0;

        foreach (var info in sushiSpawns)
        {
            GameObject sushi = Instantiate(info.sushiPrefab, info.spawnPoint.position, Quaternion.identity);
            var mover = sushi.GetComponent<BoxMover>();

            if (mover != null)
            {
                mover.conveyorPoints = conveyorPoints;
                mover.startIndexOffset = indexOffset;
                if (isPlaying)
                    mover.StartMoving();
            }
            else
            {
                Debug.LogError("Sushi prefab is missing BoxMover.");
            }

            currentSushis.Add(sushi);
            indexOffset += 2; //space sushi on conveyor path, not just time
            yield return new WaitForSeconds(spawnDelay);
        }
    }
}