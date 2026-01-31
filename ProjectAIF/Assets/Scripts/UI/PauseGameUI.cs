using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseGameUI : MonoBehaviour
{
    [SerializeField] private GameObject _pausePanel;
    [SerializeField] private AudioClip _uIBt;

    private void Awake()
    {
        GameManager.Instance.IsPaused = false;
        if (_pausePanel != null)
            _pausePanel.SetActive(false);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            StopGame(); // 토글 처리
        }
    }

    // 진행중인 게임을 돌아가기
    public void ReturnGame()
    {
        GameManager.Instance.IsPaused = false;
        Time.timeScale = 1f;

        if (_pausePanel != null)
            _pausePanel.SetActive(false);
        
        AudioManager.Instance.PlaySound(_uIBt);
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    public void RetstartGame()
    {
        GameManager.Instance.IsPaused = false;
        AudioManager.Instance.PlaySound(_uIBt);
        Time.timeScale = 1.0f;
        SceneManager.LoadScene(2); // 게임 재시작
    }

    public void QuitGame()
    {
        GameManager.Instance.IsPaused = false;
        AudioManager.Instance.PlaySound(_uIBt);
        Time.timeScale = 1.0f;
        SceneManager.LoadScene(0); // 타이틀 씬으로
    }

    private void StopGame()
    {
        //토글
        GameManager.Instance.IsPaused = !GameManager.Instance.IsPaused;

        Time.timeScale = GameManager.Instance.IsPaused ? 0f : 1f;

        if (_pausePanel != null)
            _pausePanel.SetActive(GameManager.Instance.IsPaused);

        Cursor.lockState = GameManager.Instance.IsPaused ? CursorLockMode.None : CursorLockMode.Locked;
        Cursor.visible = GameManager.Instance.IsPaused;
    }
}
