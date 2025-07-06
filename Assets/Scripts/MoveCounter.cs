using UnityEngine;
using TMPro;

public class MoveCounterManager : MonoBehaviour
{
    [Header("Settings")]
    public int parLimit = 6;

    [Header("UI")]
    public TextMeshProUGUI moveCounterText;

    public static MoveCounterManager Instance;

    private int moveCount = 0;
    private bool hasLost = false;
    private bool hasWon = false;

    private void Awake()
    {
        Instance = this;
        ResetCounter();
    }

    public void RegisterMove()
    {
        if (hasLost || hasWon) return;

        moveCount++;
        UpdateUI();

        if (moveCount >= parLimit)
        {
            TriggerLose();
        }
    }

    public void StopCountingOnWin()
    {
        hasWon = true;
    }

    public void ResetCounter()
    {
        moveCount = 0;
        hasLost = false;
        hasWon = false;
        UpdateUI();
    }

    public bool ParLimitReached()
    {
        return hasLost;
    }

    private void TriggerLose()
    {
        Debug.Log("You lose!");
        hasLost = true;
        StopAllSushiMovement();
        Invoke(nameof(RestartGameBecauseFail), 2f);
    }

    private void StopAllSushiMovement()
    {
        foreach (var mover in Object.FindObjectsByType<BoxMover>(FindObjectsSortMode.None))
        {
            mover.StopMoving();
        }
    }

    private void RestartGameBecauseFail()
    {
        Debug.Log("Restarting...");
        GameManager gameManager = Object.FindFirstObjectByType<GameManager>();
        if (gameManager != null)
        {
            gameManager.RestartGame();
        }
    }

    private void UpdateUI()
    {
        if (moveCounterText != null)
        {
            moveCounterText.text = $"{moveCount:00}/{parLimit:00}";
        }
    }
}