using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AdvanceScene : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            StartCoroutine(EndLevel());
        }
    }

    IEnumerator EndLevel()
    {
        yield return new WaitForSecondsRealtime(5f);

        //Fade stuff out...
        Application.Quit();
    }
}
