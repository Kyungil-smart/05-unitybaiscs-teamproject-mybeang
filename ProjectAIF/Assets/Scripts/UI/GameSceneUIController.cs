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
        StartCoroutine(LoadSceneWithDelay(0, 1f));
    }

    public void GoToStory()
    {
        AudioManager.Instance.PlaySound(_uIBtClip);
        StartCoroutine(LoadSceneWithDelay(1, 1f));
    }
    
    public void GoToGameStart()
    {
        AudioManager.Instance.PlaySound(_uIBtClip);
        StartCoroutine(LoadSceneWithDelay(2, 1f));
    }
    
    public void GoToEnding()
    {
        AudioManager.Instance.PlaySound(_uIBtClip);
        StartCoroutine(LoadSceneWithDelay(3, 1f));
    }
    
    public void GoToCredits()
    {
        AudioManager.Instance.PlaySound(_uIBtClip);
        StartCoroutine(LoadSceneWithDelay(4, 1f));
    }

    public void ExitGame()
    {
        AudioManager.Instance.PlaySound(_uIBtClip);
        Application.Quit();
    }
    
    private IEnumerator LoadSceneWithDelay(int number, float delay)
    {
        yield return YieldContainer.WaitForRealSeconds(delay);
        SceneManager.LoadScene(number);
    }
}
