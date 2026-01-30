using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseGameUI : MonoBehaviour
{
    [SerializeField] private GameObject _pausePanel;

    private bool isPaused = false;

    private void Awake()
    {
        if (_pausePanel != null)
            _pausePanel.SetActive(false);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            StopGame(); // 토글 처리
        }
    }

    // 진행중인 게임을 돌아가기
    public void ReturnGame()
    {
        isPaused = false;
        Time.timeScale = 1f;

        if (_pausePanel != null)
            _pausePanel.SetActive(false);

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    public void RetstartGame()
    {
        isPaused = false;
        Time.timeScale = 1.0f;
        SceneManager.LoadScene(1); // 게임 재시작
    }

    public void QuitGame()
    {
        isPaused = false;
        Time.timeScale = 1.0f;
        SceneManager.LoadScene(0); // 타이틀 씬으로
    }

    private void StopGame()
    {
        //토글
        isPaused = !isPaused;

        Time.timeScale = isPaused ? 0f : 1f;

        if (_pausePanel != null)
            _pausePanel.SetActive(isPaused);

        Cursor.lockState = isPaused ? CursorLockMode.None : CursorLockMode.Locked;
        Cursor.visible = isPaused;
    }
}
