using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameClear : MonoBehaviour
{
    public void ClearGame()
    {
        Debug.Log("ClearGame 버튼 눌림");
        SceneManager.LoadScene(3);
    }
}