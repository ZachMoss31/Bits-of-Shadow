using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpdateRespawn : MonoBehaviour
{
    public int spawnIndex;

    private void OnTriggerEnter(Collider other)
    {
        var gameManager = FindObjectOfType<GameplayManager>();
        if (other.CompareTag("Player"))
        {
            gameManager.UpdateSpawn(spawnIndex);
        }
        Debug.Log("Spawn point set to " + spawnIndex);
    }
}
