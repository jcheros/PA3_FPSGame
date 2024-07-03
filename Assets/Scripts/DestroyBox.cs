using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyBox : MonoBehaviour
{
    public int health = 0;
    public AudioClip audioClipExplotion;

    public void TakeDamage(int damage)
    {
        health -= damage;

        if (health <= 0)
        {
            Desintegrate();
        }
    }

    void Desintegrate()
    {
        if (gameObject.CompareTag("Caja"))
        {
            GameController.Instance.DestroyBox();
        } else if (gameObject.CompareTag("Barril"))
        {
            GameController.Instance.DestroyBarrel();
        }

        Destroy(gameObject);

        GameObject destructionSound = new GameObject("destructionSound");
        AudioSource soundSource = destructionSound.AddComponent<AudioSource>();
        soundSource.clip = audioClipExplotion;
        soundSource.Play();

        Destroy(destructionSound, audioClipExplotion.length);
    }
}
