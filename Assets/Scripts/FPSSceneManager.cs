using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FPSSceneManager : MonoBehaviour
{
    public static FPSSceneManager Instance { get; private set; }

    private static GameObject player;
    private static Vector3 playerPosition = new Vector3(0, 1.2f, 2);
    private static bool isTransitioning = false;

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

    public IEnumerator TransitionToNewScene(string sceneToLoad, GameObject newPlayer, Vector3 newPlayerPosition)
    {
        if (isTransitioning)
        {
            yield return null;
        }

        Debug.Log("Iniciando transición a nueva escena: " + sceneToLoad);
        isTransitioning = true;

        player = newPlayer;
        playerPosition = newPlayerPosition;
        DisablePlayerControl();
        DontDestroyOnLoad(player);

        // Cargar la nueva escena de manera aditiva
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(sceneToLoad, LoadSceneMode.Additive);

        while (!asyncLoad.isDone)
        {
            yield return null;
        }

        Debug.Log("Nueva escena cargada");

        // Obtener la escena recién cargada
        Scene newScene = SceneManager.GetSceneByName(sceneToLoad);

        // Mover al jugador a la nueva escena
        SceneManager.MoveGameObjectToScene(player, newScene);

        // Posicionar al jugador
        PositionPlayer();

        // Descargar la escena anterior
        Scene oldScene = SceneManager.GetActiveScene();
        yield return SceneManager.UnloadSceneAsync(oldScene);

        // Establecer la nueva escena como activa
        SceneManager.SetActiveScene(newScene);

        EnablePlayerControl();
        isTransitioning = false;
        Debug.Log("Transición completada");
    }

    private static void PositionPlayer()
    {
        Debug.Log("Posicionando al jugador...");
        if (player != null)
        {
            CharacterController cc = player.GetComponent<CharacterController>();
            if (cc != null)
            {
                cc.enabled = false;
            }

            player.transform.position = playerPosition;
            player.transform.rotation = Quaternion.Euler(0, -90, 0);
            //player.transform.Rotate(0, -90, 0);

            if (cc != null)
            {
                cc.enabled = true;
            }

            Debug.Log($"Jugador posicionado en: {player.transform.position}");
        }
        else
        {
            Debug.LogError("El jugador es null al intentar posicionarlo.");
        }
    }

    private void DisablePlayerControl()
    {
        // player.GetComponent<PlayerController>().enabled = false;
    }

    private void EnablePlayerControl()
    {
        // player.GetComponent<PlayerController>().enabled = true;
    }
}
