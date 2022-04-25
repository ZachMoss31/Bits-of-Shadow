using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class KeyPickup : MonoBehaviour
{
    public GameObject keyToDisable;
    public RawImage keyToAdd;

    GameplayManager gameManager;
    private void Awake()
    {
        gameManager = FindObjectOfType<GameplayManager>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            keyToAdd.gameObject.SetActive(true);
            gameManager.IncrementKeys();
            keyToDisable.SetActive(false);
            this.gameObject.SetActive(false);
        }
    }
}
