using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameSceneUIController : MonoBehaviour
{
    public void GoToTitle()
    {
        SceneManager.LoadScene(0);
    }

    public void GoToStory()
    {
        SceneManager.LoadScene(1);
    }
    
    public void GoToGameStart()
    {
        Debug.Log("Start Game");
        SceneManager.LoadScene(2);
    }
    
    public void GoToEnding()
    {
        SceneManager.LoadScene(3);
    }
    
    public void GoToCredits()
    {
        SceneManager.LoadScene(4);
    }

    public void ExitGame()
    {
        Application.Quit();
    }
}
