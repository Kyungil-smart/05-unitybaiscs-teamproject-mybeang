using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameSceneUIController : MonoBehaviour
{
    [SerializeField] private AudioClip _uIBtClip;
    
    public void GoToTitle()
    {
        AudioManager.Instance.PlaySound(_uIBtClip);
        SceneManager.LoadScene(0);
    }

    public void GoToStory()
    {
        AudioManager.Instance.PlaySound(_uIBtClip);
        SceneManager.LoadScene(1);
    }
    
    public void GoToGameStart()
    {
        Debug.Log("Start Game");
        AudioManager.Instance.PlaySound(_uIBtClip);
        SceneManager.LoadScene(2);
    }
    
    public void GoToEnding()
    {
        AudioManager.Instance.PlaySound(_uIBtClip);
        SceneManager.LoadScene(3);
    }
    
    public void GoToCredits()
    {
        AudioManager.Instance.PlaySound(_uIBtClip);
        SceneManager.LoadScene(4);
    }

    public void ExitGame()
    {
        AudioManager.Instance.PlaySound(_uIBtClip);
        Application.Quit();
    }
}
