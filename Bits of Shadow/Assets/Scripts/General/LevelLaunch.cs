using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class LevelLaunch : MonoBehaviour
{
    public void LaunchLevel(int level)
    {
        if (level == -1)
            Time.timeScale = 1;
            SceneManager.LoadScene("Sc_MainMenu");
        if(level == 0)
            SceneManager.LoadScene("Sc_TutLevel");
        if (level == 1)
            SceneManager.LoadScene("ShadowTest");
    }

    public void CloseGame()
    {
        Application.Quit();
    }
}
