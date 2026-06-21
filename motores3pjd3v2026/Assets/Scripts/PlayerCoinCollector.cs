using UnityEngine;

public class PlayerCoinCollector : MonoBehaviour
{
    private int coinCount = 0;

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Coin"))
        {
            coinCount++;

            PlayerObserverManager.NotifyCoinCollected(coinCount);

            Destroy(other.gameObject);
        }
    }
}