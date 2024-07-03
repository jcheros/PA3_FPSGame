using UnityEngine;
using UnityEngine.Events;

public class GameController : MonoBehaviour
{
    // Singleton instance
    public static GameController Instance { get; private set; }

    [SerializeField] private int startingLives = 5;
    private const int MaxLives = 100;
    private int currentLives;

    // Indicadores del juego
    private int boxesDestroyed;
    private int barrelsDestroyed;
    private int coinsCollected;
    private int playerScore;
    private bool enablePortal = false;

    // Eventos
    public UnityEvent onLivesChanged;
    public UnityEvent onGameOver;
    public UnityEvent<int> onScoreChanged;
    public UnityEvent onStatsChanged; // Evento para cualquier cambio de estadísticas
    public UnityEvent onPortalEnabled;

    private void Awake()
    {
        // Singleton pattern
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        ResetGame();
    }

    public void ResetGame()
    {
        currentLives = Mathf.Min(startingLives, MaxLives);
        boxesDestroyed = 0;
        barrelsDestroyed = 0;
        coinsCollected = 0;
        playerScore = 0;
        onStatsChanged.Invoke();
    }

    public void LoseLife()
    {
        if (currentLives > 0)
        {
            currentLives--;
            onLivesChanged.Invoke();
            onStatsChanged.Invoke();

            if (currentLives <= 0)
            {
                GameOver();
            }
        }
    }

    public void AddLife()
    {
        if (currentLives < MaxLives)
        {
            currentLives++;
            onLivesChanged.Invoke();
            onStatsChanged.Invoke();
        }

        if (!enablePortal)
        {
            enablePortal = true;
            onPortalEnabled.Invoke();
        }
    }

    public void DestroyBox()
    {
        boxesDestroyed++;
        AddScore(10);
        onStatsChanged.Invoke();
    }

    public void DestroyBarrel()
    {
        barrelsDestroyed++;
        AddScore(15);
        onStatsChanged.Invoke();
    }

    public void CollectCoin()
    {
        coinsCollected++;
        AddScore(5);
        onStatsChanged.Invoke();
    }

    private void AddScore(int points)
    {
        playerScore += points;
        onScoreChanged.Invoke(playerScore);
        onStatsChanged.Invoke();
    }

    private void GameOver()
    {
        onGameOver.Invoke();
        onStatsChanged.Invoke();
        // TODO: Agregar lógica adicional para el fin del juego
    }

    // Getters para acceder a los valores desde otros scripts
    public int GetCurrentLives() => currentLives;
    public int GetMaxLives() => MaxLives;
    public int GetBoxesDestroyed() => boxesDestroyed;
    public int GetBarrelsDestroyed() => barrelsDestroyed;
    public int GetCoinsCollected() => coinsCollected;
    public int GetPlayerScore() => playerScore;
    public bool isPortalEnable() => enablePortal;
}