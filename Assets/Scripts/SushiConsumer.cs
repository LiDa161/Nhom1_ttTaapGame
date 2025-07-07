using UnityEngine;
using UnityEngine.UI; // Để sử dụng UI Image
using UnityEngine.SceneManagement; // Thêm để chuyển Scene

public class SushiDotReceiver : MonoBehaviour
{
    [Header("Receiver Settings")]
    public SushiShape acceptedShape;

    [Header("UI Settings")]
    public GameObject winPanel; // Panel hiển thị thông báo Win
    public Image winImage; // Image hiển thị ảnh Win

    public static int totalRequired = 0;
    private static int currentScore = 0;

    public event System.Action OnSushiConsumed;

    private void Start()
    {
        // Ẩn panel win khi bắt đầu
        if (winPanel != null)
        {
            winPanel.SetActive(false);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        var controller = other.GetComponent<SushiShapeController>();
        if (controller == null) return;

        if (controller.currentShape == acceptedShape)
        {
            HandleCorrectSushi(other.gameObject);
        }
        else
        {
            Debug.Log("Sushi ignored due to incorrect shape.");
        }
    }

    private void HandleCorrectSushi(GameObject sushi)
    {
        Destroy(sushi);
        currentScore++;
        Debug.Log($"Accepted sushi! Score: {currentScore}/{totalRequired}");

        OnSushiConsumed?.Invoke();

        if (currentScore >= totalRequired)
        {
            TriggerWin();
        }
    }

    private void TriggerWin()
    {
        Debug.Log("You win!");
        // Hiển thị panel với ảnh Win
        if (winPanel != null && winImage != null)
        {
            winPanel.SetActive(true);
            winImage.enabled = true; // Đảm bảo ảnh được bật
        }
        MoveCounterManager.Instance.StopCountingOnWin();
        // Chuyển sang Scene SelectLevel sau 2 giây
        Invoke(nameof(GoToSelectLevel), 2f);
    }

    private void GoToSelectLevel()
    {
        SceneManager.LoadScene("SelectLevel");
    }

    /// <summary>
    /// Call this at the beginning of the level to set the required number of correct sushi.
    /// </summary>
    public static void SetRequiredSushi(int total)
    {
        totalRequired = total;
        currentScore = 0;
    }
}