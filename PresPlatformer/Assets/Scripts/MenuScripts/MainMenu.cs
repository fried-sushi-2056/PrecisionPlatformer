using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public void PlayGame()
    {
        SceneManager.LoadScene("Level Selector");//Brings you to the level select screen
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void GoToMenu()
    {
        SceneManager.LoadScene("Title Screen");
    }

    public void LoadLvl1()
    {
        SceneManager.LoadScene(1);
    }

    public void LoadLvl2()
    {
        SceneManager.LoadScene(2);
    }
}
