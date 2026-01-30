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

    public void GoToEnding()
    {
        SceneManager.LoadScene(1);
    }
    
    public void GoToCredits()
    {
        SceneManager.LoadScene(3);
    }

    public void ExitGame()
    {
        
    }
}
