using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class LevelLaunch : MonoBehaviour
{
    public AudioSource mainAudio;
    public AudioClip selectionAudio;
    public AudioClip quitAudio;

    public void LaunchLevel(int level)
    {
        if (level == -1)
        {
            mainAudio.clip = selectionAudio;
            mainAudio.Play();
            Time.timeScale = 1;
            StartCoroutine(MainMenu());
            Cursor.lockState = CursorLockMode.None;
        }
        if(level == 0)
        {
            mainAudio.clip = selectionAudio;
            mainAudio.Play();
            var fadeControl = FindObjectOfType<FadeControl>();
            fadeControl.StartFade(1);
            Time.timeScale = 1;
            SceneManager.LoadScene("Sc_TutLevel");
        }  
    }

    public void CloseGame()
    {
        mainAudio.clip = quitAudio;
        mainAudio.Play();
        Application.Quit();
    }

    IEnumerator MainMenu()
    {
        var fadeControl = FindObjectOfType<FadeControl>();
        fadeControl.StartFade(1);
        yield return new WaitForSeconds(1.5f);
        SceneManager.LoadScene("Sc_MainMenu");
    }
}
