using UnityEngine;

public class DoorTrigger : MonoBehaviour
{
    public CoinCollectTask coinCollectTask;
    public GameObject door;
    public float doorOpenDuration = 2f;
    public Vector3 doorOpenVector = new Vector3(0f, -9f, 0f);

    public GameTimer gameTimer;
    public WinManager winManager;

    public AudioClip doorOpenSound; // 🔊 صوت فتح الباب
    private AudioSource audioSource;

    private bool isDoorOpen = false;
    private bool isFullyOpen = false;
    private float openStartTime = 0f;
    private Vector3 doorClosedPosition;

    private void Start()
    {
        doorClosedPosition = door.transform.position;

        // 🎧 إنشاء AudioSource
        audioSource = gameObject.AddComponent<AudioSource>();
        audioSource.playOnAwake = false;
    }

    private void Update()
    {
        if (isDoorOpen && !isFullyOpen)
        {
            float elapsedTime = Time.time - openStartTime;
            if (elapsedTime < doorOpenDuration)
            {
                door.transform.position = Vector3.Lerp(doorClosedPosition, doorClosedPosition + doorOpenVector, elapsedTime / doorOpenDuration);
            }
            else
            {
                door.transform.position = doorClosedPosition + doorOpenVector;
                isFullyOpen = true;

                if (gameTimer != null) gameTimer.StopTimer();
                if (winManager != null) winManager.ShowWinPanelAfterDelay();
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && coinCollectTask.allCoinsCollected && !isDoorOpen)
        {
            isDoorOpen = true;
            openStartTime = Time.time;
            doorClosedPosition = door.transform.position;

            // 🔊 تشغيل صوت الباب
            if (doorOpenSound != null)
            {
                audioSource.PlayOneShot(doorOpenSound);
            }
        }
    }
}
