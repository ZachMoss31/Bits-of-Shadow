using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TextTrigger : MonoBehaviour
{
    //This will never clear the text.
    public bool exitTrigger = false;
    public GameObject text;
    
    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
            text.gameObject.SetActive(true);
    }

    private void OnTriggerExit(Collider other)
    {
        if (exitTrigger)
            return;

        if (other.CompareTag("Player"))
            text.gameObject.SetActive(false);
    }
}
