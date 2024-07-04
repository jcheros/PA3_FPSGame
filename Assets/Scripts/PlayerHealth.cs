using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    public float damageInterval = 2f; // Intervalo en segundos entre cada daño
    public AudioClip audioClipCelebration;
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

        if (other.CompareTag("Trophy"))
        {
            GameController.Instance.GameWin();
            Destroy(other.gameObject);

            GameObject destructionSound = new GameObject("celebrationSound");
            AudioSource soundSource = destructionSound.AddComponent<AudioSource>();
            soundSource.clip = audioClipCelebration;
            soundSource.Play();

            Destroy(destructionSound, audioClipCelebration.length);
        }
    }

    private void TakeDamage()
    {
        GameController.Instance.LoseLife();
    }
}