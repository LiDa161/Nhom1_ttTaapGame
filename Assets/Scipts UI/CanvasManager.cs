using UnityEngine;
using UnityEngine.UI;

public class CanvasManager : MonoBehaviour
{
    public GameObject HomeCanvas;
    public GameObject StartCanvas;
    public GameObject HowToPlayCanvas;

    // Mảng chứa các Prefabs level
    public GameObject[] LevelPrefabs;

    // Vị trí sinh level
    public Transform spawnPoint;

    void Start()
    {
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
        ClearCurrentLevel(); // Xóa level hiện tại khi quay lại
    }

    // Sinh ra level tương ứng khi nhấn nút Level
    public void OnLevelButtonClicked(int levelIndex)
    {
        if (levelIndex >= 0 && levelIndex < LevelPrefabs.Length && LevelPrefabs[levelIndex] != null && spawnPoint != null)
        {
            ClearCurrentLevel(); // Xóa level cũ trước khi sinh level mới
            Instantiate(LevelPrefabs[levelIndex], spawnPoint.position, Quaternion.identity);
        }
    }

    // Hàm xóa level hiện tại (nếu có)
    private void ClearCurrentLevel()
    {
        GameObject[] existingLevels = GameObject.FindGameObjectsWithTag("Level");
        foreach (GameObject level in existingLevels)
        {
            Destroy(level);
        }
    }
}