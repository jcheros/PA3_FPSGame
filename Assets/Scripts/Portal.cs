using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Portal : MonoBehaviour
{
    public string sceneToLoad;
    private static GameObject player;
    private static Vector3 newPlayerPosition = new Vector3(0, 1.2f, 2);
    private static bool isTransitioning = false;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && !isTransitioning)
        {
            isTransitioning = true;
            player = other.gameObject;
            StartCoroutine(FPSSceneManager.Instance.TransitionToNewScene(sceneToLoad, player, newPlayerPosition));
        }
    }
}