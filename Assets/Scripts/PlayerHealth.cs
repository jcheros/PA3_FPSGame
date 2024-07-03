using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    public float damageInterval = 2f; // Intervalo en segundos entre cada daño
    private float lastDamageTime;

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Lava"))
        {
            if (Time.time - lastDamageTime >= damageInterval)
            {
                Debug.Log("Player took damage from lava!");
                TakeDamage();
                lastDamageTime = Time.time;
            }
        }
    }

    private void TakeDamage()
    {
        GameController.Instance.LoseLife();
    }
}