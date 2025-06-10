using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class GameOverManager : MonoBehaviour
{
    public GameObject gameOverPanel;
    public TextMeshProUGUI timerText;
    public GameObject playerObject;
    public float timeLimit = 60f;

    private float timeLeft;
    private bool isGameOver = false;

    void Start()
    {
        gameOverPanel.SetActive(false);
        timeLeft = PlayerPrefs.GetFloat("GameTime", timeLimit);
    }

    void Update()
    {
        if (isGameOver) return;

        timeLeft -= Time.deltaTime;
        timerText.text = Mathf.Ceil(timeLeft).ToString() + "s";

        if (timeLeft <= 0)
        {
            timerText.text = "Time's up!";
            TriggerGameOver();
        }
    }

    public void TriggerGameOver()
    {
        if (isGameOver) return;

        isGameOver = true;
        Debug.Log("✅ Game Over Triggered");
        gameOverPanel.SetActive(true);

        if (playerObject != null)
            playerObject.GetComponent<CustomPlayerController>().enabled = false;
    }

    public void Retry()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void ToMainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }

    // ✅ الدالة الجديدة للخروج من اللعبة
    public void QuitGame()
    {
        Debug.Log("🛑 Quit Game pressed");
        Application.Quit();
    }
}
