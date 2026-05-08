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
        Gameplay
    }

    public GameState CurrentState;

    [Header("Input")]
    public PlayerInput playerInput;

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
                break;

            case "GetStarted_Scene":
                ChangeState(GameState.Gameplay);

                AllocateInput();
                break;
        }
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