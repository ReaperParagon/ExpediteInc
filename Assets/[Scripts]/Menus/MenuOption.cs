using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuOption : MonoBehaviour
{
    /// Functions ///

    public void PlayButtonSoundEffect()
    {
        AudioManager.instance.PlayMenuClip();
    }

    public void GoToScene(string scene)
    {
        SceneManager.LoadScene(scene);
    }

    public void ExitGame()
    {
        Application.Quit();
    }
}
