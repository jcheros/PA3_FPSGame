using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Medkit : MonoBehaviour
{
    public float respawnTime = 10f;
    public AudioClip audioClipPowerUp;
    private bool isActive = true;


    private void OnTriggerEnter(Collider other)
    {
        if (isActive && other.CompareTag("Player"))
        {
            CollectMedkit();
        }
    }

    private void CollectMedkit()
    {
        GameController.Instance.AddLife();
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
        soundSource.clip = audioClipPowerUp;
        soundSource.Play();

        Destroy(destructionSound, audioClipPowerUp.length);
    }
}
