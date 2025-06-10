using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class MainMenu : MonoBehaviour
{
    public GameObject chooseLevelButton;  // زر "اختر مستواك"
    public GameObject beginnerButton;     // زر "مبتدئ"
    public GameObject proButton;          // زر "محترف"
    public TextMeshProUGUI levelTitle;    // النص اللي فوق (اختر المستوى)

    private void Start()
    {
        // بالبداية، نظهر فقط زر "اختر مستواك"
        chooseLevelButton.SetActive(true);
        beginnerButton.SetActive(false);
        proButton.SetActive(false);
        levelTitle.gameObject.SetActive(false);
    }

    public void OnChooseLevelPressed()
    {
        // لما يضغط "اختر مستواك"
        chooseLevelButton.SetActive(false);

        levelTitle.text = "";
        levelTitle.gameObject.SetActive(true);
        beginnerButton.SetActive(true);
        proButton.SetActive(true);
    }

    public void OnBeginnerSelected()
    {
        Debug.Log("Beginner selected!");
        // وقت المبتدئ: 90 ثانية
        PlayerPrefs.SetFloat("GameTime", 90f); // or 45f for pro
        SceneManager.LoadScene("desert");
    }

    public void OnProSelected()
    {
        Debug.Log("Pro selected!");
        // وقت المحترف: 45 ثانية
        PlayerPrefs.SetFloat("GameTime", 45f); // 45 sec
        SceneManager.LoadScene("desert");
    }

    public void OnQuitPressed()
    {
        Application.Quit();
        Debug.Log("Quit Game");
    }
}
