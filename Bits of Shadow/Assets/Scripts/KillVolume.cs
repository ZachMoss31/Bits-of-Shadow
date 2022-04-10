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
            var curAnim = other.GetComponentInChildren<Animator>();
            var _playerControl = other.GetComponentInChildren<PlayerControls>();
            _playerControl.PlayerDeathAnimate();
            //other.gameObject.SetActive(false);
            StartCoroutine(RespawnPlayer(_playerControl));
        }

        else if (other.CompareTag("Interactable"))
        {
            other.gameObject.SetActive(false);
        }
    }

    IEnumerator RespawnPlayer(PlayerControls _playerAnimator)
    {
        yield return new WaitForSecondsRealtime(3f);
        _playerAnimator.StopPlayer();
        player.transform.position = respawnPoint.transform.position;
        player.transform.rotation = respawnPoint.transform.rotation;
        //player.gameObject.SetActive(true);
        deathParticles.Stop();
        _playerAnimator.PlayerDeathRespawn();
    }
}
