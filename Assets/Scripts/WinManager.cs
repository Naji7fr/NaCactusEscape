using UnityEngine;
using UnityEngine.SceneManagement;

public class WinManager : MonoBehaviour
{
    public GameObject winPanel;

    public void ShowWinPanelAfterDelay()
    {
        Invoke("ShowWinPanel", 3f); // بعد 5 ثواني من الفوز
    }

    void ShowWinPanel()
    {
        if (winPanel != null)
        {
            winPanel.SetActive(true);
        }
    }

    public void ReturnToMenu()
    {
        SceneManager.LoadScene("MainMenu"); // غيّر الاسم حسب اسم مشهد القائمة عندك
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
