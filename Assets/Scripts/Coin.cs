using UnityEngine;

public class Coin : MonoBehaviour
{
    private CoinCollectTask coinCollectTask;
    public AudioClip collectSound;  // ✅ نضيف ملف صوت
    private AudioSource audioSource;

    private void Start()
    {
        coinCollectTask = FindObjectOfType<CoinCollectTask>();

        // نضيف AudioSource تلقائيًا
        audioSource = gameObject.AddComponent<AudioSource>();
        audioSource.playOnAwake = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (coinCollectTask != null)
            {
                coinCollectTask.CoinsCollected();
            }
            else
            {
                Debug.LogError("❌ CoinCollectTask not found in scene!");
            }

            // ✅ تشغيل صوت العملة
            if (collectSound != null)
            {
                audioSource.PlayOneShot(collectSound);
            }

            // ✅ نحذف العملة بعد شوي عشان الصوت يخلص
            Destroy(gameObject, 0.2f);
        }
    }
}
