using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndingUIController : MonoBehaviour
{
    [SerializeField] GameObject _gameOverUI;
    [SerializeField] GameObject _gameClearUI;
    private void Start()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        if (GameManager.Instance.IsGameOver)
        {
            _gameOverUI.gameObject.SetActive(true);
        }
        else
        {
            _gameClearUI.gameObject.SetActive(true);
        }
    }
}
