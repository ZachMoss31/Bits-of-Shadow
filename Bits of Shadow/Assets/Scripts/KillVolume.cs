using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KillVolume : MonoBehaviour
{
    public Transform respawnPoint;
    public GameObject player;
    public ParticleSystem deathParticles;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            deathParticles.transform.position = other.transform.position;
            deathParticles.Play();
            other.gameObject.SetActive(false);
            StartCoroutine(RespawnPlayer());
        }

        else if (other.CompareTag("Interactable"))
        {
            other.gameObject.SetActive(false);
        }
    }

    IEnumerator RespawnPlayer()
    {
        yield return new WaitForSecondsRealtime(3f);
        player.transform.position = respawnPoint.transform.position;
        player.transform.rotation = respawnPoint.transform.rotation;
        player.gameObject.SetActive(true);
        deathParticles.Stop();
    }
}
