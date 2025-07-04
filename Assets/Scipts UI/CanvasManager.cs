using UnityEngine;
using UnityEngine.UI;

public class CanvasManager : MonoBehaviour
{
    public GameObject HomeCanvas;
    public GameObject StartCanvas;
    public GameObject HowToPlayCanvas;

    void Start()
    {
        // Đảm bảo chỉ có HomeCanvas hiển thị ban đầu
        StartCanvas.SetActive(false);
        HowToPlayCanvas.SetActive(false);
        HomeCanvas.SetActive(true);
    }

    // Chuyển sang StartCanvas khi nhấn nút Start
    public void OnStartButtonClicked()
    {
        HomeCanvas.SetActive(false);
        StartCanvas.SetActive(true);
        HowToPlayCanvas.SetActive(false);
    }

    // Chuyển sang HowToPlayCanvas khi nhấn nút ?
    public void OnHowToPlayButtonClicked()
    {
        HomeCanvas.SetActive(false);
        StartCanvas.SetActive(false);
        HowToPlayCanvas.SetActive(true);
    }

    // Quay lại HomeCanvas khi nhấn nút X
    public void OnBackButtonClicked()
    {
        HomeCanvas.SetActive(true);
        StartCanvas.SetActive(false);
        HowToPlayCanvas.SetActive(false);
    }
}