using TMPro;
using UnityEngine;

public class CoinUI : MonoBehaviour
{
    [Header("Interface dos Jogadores")]
    [SerializeField] private TextMeshProUGUI player1Text;
    [SerializeField] private TextMeshProUGUI player2Text;

    [Header("Fim de Jogo")]
    [SerializeField] private GameObject winPanel;
    [SerializeField] private TextMeshProUGUI winText;

    private void OnEnable()
    {
        PlayerObserverManager.OnCoinCollected += UpdateCoinText;
        PlayerObserverManager.OnGameOver += ShowWinner;
    }

    private void OnDisable()
    {
        PlayerObserverManager.OnCoinCollected -= UpdateCoinText;
        PlayerObserverManager.OnGameOver -= ShowWinner;
    }

    private void Start()
    {
        if (player1Text != null) player1Text.text = "P1 Moedas: 0";
        if (player2Text != null) player2Text.text = "P2 Moedas: 0";
        if (winPanel != null) winPanel.SetActive(false);
    }

    private void UpdateCoinText(int playerId, int totalCoins)
    {
        if (playerId == 1 && player1Text != null)
        {
            player1Text.text = "P1 Moedas: " + totalCoins;
        }
        else if (playerId == 2 && player2Text != null)
        {
            player2Text.text = "P2 Moedas: " + totalCoins;
        }
    }

    private void ShowWinner(int winningPlayerId)
    {
        if (winPanel != null)
        {
            winPanel.SetActive(true);

            if (winText != null)
            {
                if (winningPlayerId == 0)
                {
                    winText.text = "Empate!";
                }
                else
                {
                    winText.text = "Jogador " + winningPlayerId + " Venceu!";
                }
            }
        }
    }
}