using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public Text indicatorsText;
    public GameObject gameWinContainer;
    public GameObject gameOverContainer;

    void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }

    private void Start()
    {
        gameWinContainer.SetActive(false);
        gameOverContainer.SetActive(false);

        // Suscribirse al evento onStatsChanged del GameController
        GameController.Instance.onStatsChanged.AddListener(UpdateGameText);

        // Suscribirse al evento onGameOver del GameController
        GameController.Instance.onGameOver.AddListener(ShowGameOverText);

        // Suscribirse al evento onGameWin del GameController
        GameController.Instance.onGameWin.AddListener(ShowGameWinText);

        // Suscribirse al evento onGameRestart del GameController
        GameController.Instance.onGameRestart.AddListener(GameRestarStart);

        // Actualizar el texto inicial
        UpdateGameText();
    }

    public void UpdateGameText()
    {
        int lives = GameController.Instance.GetCurrentLives();
        int points = GameController.Instance.GetPlayerScore();
        int boxes = GameController.Instance.GetBoxesDestroyed();
        int barrels = GameController.Instance.GetBarrelsDestroyed();
        int coins = GameController.Instance.GetCoinsCollected();

        indicatorsText.text = string.Format(
            "VIDAS: {0}\n" +
            "PUNTAJE: {1}\n" +
            "CAJAS DESTRUIDAS: {2}\n" +
            "BARRILES DESTRUIDOS: {3}\n" +
            "MONEDAS RECOGIDAS: {4}",
            lives, points, boxes, barrels, coins
        );
    }

    public void ShowGameOverText()
    {
        Debug.Log("In ShowGameOverText");
        gameOverContainer.SetActive(true);
    }

    public void ShowGameWinText()
    {
        gameWinContainer.SetActive(true);
    }

    public void GameRestarStart()
    {
        gameWinContainer.SetActive(false);
        gameOverContainer.SetActive(false);
    }
}