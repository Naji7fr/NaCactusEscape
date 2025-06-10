using UnityEngine;
using TMPro;

public class GameTimer : MonoBehaviour
{
    [Header("References")]
    public TextMeshProUGUI timerText;
    public GameObject playerObject;
    public GameOverManager gameOverManager;

    private float timeLeft;
    private bool timeEnded = false;

    void Start()
    {
        // Get time from PlayerPrefs (set by main menu), default to 60 if not found
        timeLeft = PlayerPrefs.GetFloat("GameTime", 60f);
        Debug.Log($"🕒 Timer started with {timeLeft} seconds");
    }

    void Update()
    {
        if (timeEnded) return;

        timeLeft -= Time.deltaTime;
        timeLeft = Mathf.Max(0, timeLeft);

        UpdateTimerUI();

        if (timeLeft <= 0)
        {
            HandleTimeEnd();
        }
    }

    private void UpdateTimerUI()
    {
        if (timerText != null)
            timerText.text = Mathf.Ceil(timeLeft).ToString() + "s";
    }

    private void HandleTimeEnd()
    {
        timeEnded = true;

        if (timerText != null)
            timerText.text = "Time's up!";

        if (playerObject != null)
        {
            var controller = playerObject.GetComponent<CustomPlayerController>();
            if (controller != null)
                controller.enabled = false;
        }

        if (gameOverManager != null)
        {
            gameOverManager.TriggerGameOver();
        }
        else
        {
            Debug.LogError("❌ GameOverManager not assigned in GameTimer!");
        }
    }

    public void StopTimer()
    {
        timeEnded = true;
    }
}
