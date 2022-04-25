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
        yield return new WaitForSecondsRealtime(3f);

        //Fade stuff out...

        Cursor.lockState = CursorLockMode.None;
        UnityEngine.SceneManagement.SceneManager.LoadScene("Sc_MainMenu");
    }
}
