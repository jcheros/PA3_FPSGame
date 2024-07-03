using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour
{
    public float respawnTime = 10f;
    public AudioClip audioClipCoinPickup;
    private bool isActive = true;

    private void OnTriggerEnter(Collider other)
    {
        if (isActive && other.CompareTag("Player"))
        {
            CollectCoin();
        }
    }

    private void CollectCoin()
    {
        GameController.Instance.CollectCoin();
        StartCoroutine(DisappearAndRespawn());
    }

    private IEnumerator DisappearAndRespawn()
    {
        isActive = false;
        soundEfect();
        GetComponent<Renderer>().enabled = false;
        GetComponent<Collider>().enabled = false;

        yield return new WaitForSeconds(respawnTime);

        GetComponent<Renderer>().enabled = true;
        GetComponent<Collider>().enabled = true;
        isActive = true;
    }

    private void soundEfect()
    {
        GameObject destructionSound = new GameObject("destructionSound");
        AudioSource soundSource = destructionSound.AddComponent<AudioSource>();
        soundSource.clip = audioClipCoinPickup;
        soundSource.Play();

        Destroy(destructionSound, audioClipCoinPickup.length);
    }
}
