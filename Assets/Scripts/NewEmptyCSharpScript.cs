using UnityEngine;
using TMPro; 

public class CoinCounterUI : MonoBehaviour
{
    [Header("References")]
    public TMP_Text coinText;      
    public int totalCoins = 30;

    private int collectedCoins = 0;

    void Start()
    {
        UpdateCoinText();
    }

    public void OnCoinCollected()
    {
        collectedCoins++;
        UpdateCoinText();
    }

    void UpdateCoinText()
    {
        coinText.text = $"{collectedCoins}/{totalCoins}";
    }
}