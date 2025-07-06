using UnityEngine;
using TMPro;

public class MoveCounterManager : MonoBehaviour
{
    public int parLimit = 6;
    public TextMeshProUGUI moveCounterText; // assign in Inspector

    private int moveCount = 0;
    private bool hasLost = false;

    private bool hasWon = false;

    public static MoveCounterManager Instance;

    private void Awake()
    {
        Instance = this;
        moveCount = 0;
        hasLost = false;
        UpdateUI();
    }

    public void RegisterMove()
    {
        if (hasLost || hasWon) return; // prevent counting after win or loss

        moveCount++;
        UpdateUI();

        if (moveCount >= parLimit) // LOSE CHECK!!!
        {
            Debug.Log("You lose!");
            hasLost = true;
            StopAllSushiMovement();
            Invoke(nameof(RestartGameBecauseFail), 2f);
        }
    }

    public void StopCountingOnWin()
    {
        hasWon = true;
    }

    private void StopAllSushiMovement()
    {
        var movers = Object.FindObjectsByType<BoxMover>(FindObjectsSortMode.None);
        foreach (var mover in movers)
        {
            mover.StopMoving();
        }
    }

    public void RestartGameBecauseFail()
    {
        Debug.Log("Restarting...");
        GameManager gameManager = Object.FindFirstObjectByType<GameManager>();
        if (gameManager != null)
        {
            gameManager.RestartGame();
        }
    }

    public void ResetCounter()
    {
        moveCount = 0;
        hasLost = false;
        UpdateUI();
    }

    private void UpdateUI()
    {
        if (moveCounterText != null)
        {
            moveCounterText.text = $"{moveCount:00}/{parLimit:00}";
        }
    }
}