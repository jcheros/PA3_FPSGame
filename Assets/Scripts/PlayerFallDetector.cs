using UnityEngine;

public class PlayerFallDetector : MonoBehaviour
{
    public float fallThreshold = -10f; // Ajusta este valor seg�n tu escena
    public float checkInterval = 0.3f; // Cada cu�nto tiempo verificar la posici�n

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
        Debug.Log("El jugador ha ca�do al vac�o");
        GameController.Instance.LoseLife();
    }
}