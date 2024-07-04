using UnityEngine;

public class PlayerFallDetector : MonoBehaviour
{
    public float fallThreshold = -10f; // Ajusta este valor según tu escena
    public float checkInterval = 0.3f; // Cada cuánto tiempo verificar la posición

    private float lastYPosition;
    private float timeSinceLastCheck;

    private void Start()
    {
        lastYPosition = transform.position.y;
    }

    private void Update()
    {
        timeSinceLastCheck += Time.deltaTime;

        if (timeSinceLastCheck >= checkInterval)
        {
            CheckForFall();
            timeSinceLastCheck = 0f;
        }
    }

    private void CheckForFall()
    {
        float currentYPosition = transform.position.y;

        if (currentYPosition < fallThreshold)
        {
            OnFallIntoVoid();
        }

        lastYPosition = currentYPosition;
    }

    private void OnFallIntoVoid()
    {
        Debug.Log("El jugador ha caído al vacío");
        GameController.Instance.LoseLife();
    }
}