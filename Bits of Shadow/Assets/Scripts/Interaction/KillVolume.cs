using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KillVolume : MonoBehaviour
{
    GameplayManager _gameplayManager;

    private void Awake()
    {
        _gameplayManager = GameObject.Find("SceneManagement").GetComponent<GameplayManager>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            _gameplayManager.KillPlayer(other.gameObject);
        }

        //If a box was blown off map by push attack
        else if (other.GetComponent<IInteractable>() != null)
        {
            other.gameObject.SetActive(false);
        }
    }
}
