using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneController : MonoBehaviour
{
    // Hàm chuyển sang SelectLevelScene
    public void GoToSelectLevel()
    {
        SceneManager.LoadScene("SelectLevelScene");
    }

    // Hàm chuyển sang GuideScene
    public void GoToGuide()
    {
        SceneManager.LoadScene("GuideScene");
    }
    public void Edit()
    {
        SceneManager.LoadScene("Start");
    }
}