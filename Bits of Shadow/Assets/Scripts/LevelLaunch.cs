using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class LevelLaunch : MonoBehaviour
{
    public void LaunchLevel(int level)
    {
        SceneManager.LoadScene("Sc_TutLevel");
    }
}
