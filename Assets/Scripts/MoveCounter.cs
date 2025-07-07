using UnityEngine;
using UnityEngine.UI; // Thêm để sử dụng UI Image
using TMPro;

public class MoveCounterManager : MonoBehaviour
{
    [Header("Settings")]
    public int parLimit = 6;

    [Header("UI")]
    public TextMeshProUGUI moveCounterText;
    public GameObject losePanel; // Panel hiển thị thông báo Lose
    public Image loseImage; // Image hiển thị ảnh Lose

    public static MoveCounterManager Instance;

    private int moveCount = 0;
    private bool hasLost = false;
    private bool hasWon = false;

    private void Awake()
    {
        Instance = this;
        ResetCounter();
    }

    private void Start()
    {
        // Ẩn panel lose khi bắt đầu
        if (losePanel != null)
        {
            losePanel.SetActive(false);
        }
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
        // Ẩn panel lose khi reset
        if (losePanel != null)
        {
            losePanel.SetActive(false);
        }
    }

    public bool ParLimitReached()
    {
        return hasLost;
    }

    private void TriggerLose()
    {
        Debug.Log("You lose!");
        hasLost = true;
        // Hiển thị panel với ảnh Lose
        if (losePanel != null && loseImage != null)
        {
            losePanel.SetActive(true);
            loseImage.enabled = true; // Đảm bảo ảnh được bật
        }
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