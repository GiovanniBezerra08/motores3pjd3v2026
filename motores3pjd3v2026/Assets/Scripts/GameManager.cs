using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;

public class GameManager : MonoBehaviour
{
    // singleton
    public static GameManager Instance;

    // estadosdogame
    public enum GameState
    {
        Iniciando,
        MenuPrincipal,
        Gameplay,
        FimDeJogo
    }

    public GameState CurrentState;

    [Header("Input")]
    public PlayerInput playerInput;

    [Header("Multiplayer & Vencedor")]
    private int p1Coins = 0;
    private int p2Coins = 0;
    private int totalCoinsInScene = 0;

    private void Awake()
    {
        // Singleton
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
            return;
        }
    }

    private void Start()
    {
        ChangeState(GameState.Iniciando);
        
        LoadScene("Splash");
    }

    // trocaestado
    public void ChangeState(GameState newState)
    {
        CurrentState = newState;

        Debug.Log("Estado atual: " + CurrentState);
    }

    // trocadecenas
    public void LoadScene(string sceneName)
    {
        Debug.Log("Carregando cena: " + sceneName);

        SceneManager.LoadScene(sceneName);

        // atualizaroestadoconformeacena
        switch (sceneName)
        {
            case "Splash":
                ChangeState(GameState.Iniciando);
                break;

            case "MenuPrincipal":
                ChangeState(GameState.MenuPrincipal);

                UnloadGUI();

                break;

            case "GetStarted_Scene":
                ChangeState(GameState.Gameplay);

                ResetCoins();

                AllocateInput();

                if (!SceneManager.GetSceneByName("GUI").isLoaded)
                {
                    SceneManager.LoadScene("GUI", LoadSceneMode.Additive);
                }

                break;
        }
    }

    private void ResetCoins()
    {
        p1Coins = 0;
        p2Coins = 0;
        totalCoinsInScene = GameObject.FindGameObjectsWithTag("Coin").Length;
    }

    public void OnCoinCollectedByPlayer(int playerId, int currentCoins)
    {
        if (playerId == 1)
        {
            p1Coins = currentCoins;
        }
        else if (playerId == 2)
        {
            p2Coins = currentCoins;
        }

        totalCoinsInScene--;

        if (totalCoinsInScene <= 0)
        {
            EndGame();
        }
    }

    private void EndGame()
    {
        ChangeState(GameState.FimDeJogo);

        int winner = 0; // 0 = Empate
        if (p1Coins > p2Coins)
        {
            winner = 1;
        }
        else if (p2Coins > p1Coins)
        {
            winner = 2;
        }

        PlayerObserverManager.NotifyGameOver(winner);
    }

    // alocacãodoInput
    void AllocateInput()
    {
        if (playerInput != null)
        {
            Debug.Log("Input alocado ao jogador.");
        }
        else
        {
            Debug.LogWarning("PlayerInput não encontrado!");
        }
    }
    
    void UnloadGUI()
    {
        if (SceneManager.GetSceneByName("GUI").isLoaded)
        {
            SceneManager.UnloadSceneAsync("GUI");
        }
    }

    // comecandojogobotao
    public void StartGame()
    {
        LoadScene("GetStarted_Scene");
    }

    // saindodojogobotao
    public void QuitGame()
    {
        Debug.Log("Saindo do jogo...");
        Application.Quit();
    }
}