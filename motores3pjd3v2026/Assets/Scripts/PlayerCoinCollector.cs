using UnityEngine;

public class PlayerCoinCollector : MonoBehaviour
{
    [Header("Identificação do Jogador")]
    public int playerId = 1;

    [Header("Aumento de Velocidade")]
    [SerializeField] private float speedIncrement = 1.5f;

    private int coinCount = 0;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Coin"))
        {
            coinCount++;

            // Notifica o sistema de observador
            PlayerObserverManager.NotifyCoinCollected(playerId, coinCount);

            // Atualiza o GameManager
            if (GameManager.Instance != null)
            {
                GameManager.Instance.OnCoinCollectedByPlayer(playerId, coinCount);
            }

            Destroy(other.gameObject);
        }
    }
}