using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.Characters.FirstPerson;

public class PlayerRunController : MonoBehaviour
{
    public float walkSpeed = 5;
    public float runningSpeedMultiplier = 1.5f;

    private FirstPersonController fpsController;
    private CharacterController characterController;

    void Start()
    {
        fpsController = GetComponent<FirstPersonController>();
        characterController = GetComponent<CharacterController>();
    }

    void Update()
    {
        // Obtenemos el input original
        float inputX = Input.GetAxis("Horizontal");
        float inputY = Input.GetAxis("Vertical");

        // Verificamos si el jugador está intentando correr
        bool isRunning = Input.GetKey(KeyCode.R);

        // Calculamos el movimiento
        Vector3 move = transform.right * inputX + transform.forward * inputY;

        // Aplicamos el multiplicador si está corriendo
        if (isRunning)
        {
            move *= runningSpeedMultiplier;

            // Aplicamos el movimiento
            characterController.Move(move * Time.deltaTime * walkSpeed);
        }
    }
}
