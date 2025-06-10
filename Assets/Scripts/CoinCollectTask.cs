using UnityEngine;

public class CoinCollectTask : MonoBehaviour
{
    [Header("Game Settings")]
    public int requiredCoins = 5;
    public bool allCoinsCollected = false;

    private int currentCoins = 0;

    public void CoinsCollected()
    {
        currentCoins++;
        Debug.Log("✅ Coin collected! Current: " + currentCoins);

        if (currentCoins >= requiredCoins)
        {
            allCoinsCollected = true;
            Debug.Log("🎉 All coins collected!");
             
        }
    }
}
