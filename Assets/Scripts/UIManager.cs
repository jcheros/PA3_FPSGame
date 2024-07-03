using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public Text indicatorsText;

    void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }

    private void Start()
    {
        // Suscribirse al evento onStatsChanged del GameController
        GameController.Instance.onStatsChanged.AddListener(UpdateGameText);

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
}