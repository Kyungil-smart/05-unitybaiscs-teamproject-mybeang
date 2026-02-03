using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseGameUI : MonoBehaviour
{
    [SerializeField] private GameObject _pausePanel;
    [SerializeField] private AudioClip _uIBt;
    [SerializeField] private AudioClip _openPenalSound;
    private bool _isOpened;

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
        AudioManager.Instance.PlaySound(_uIBt);
        if (_pausePanel != null)
            _pausePanel.SetActive(false);
        
        if (GameManager.Instance.IsOpenedAbilityManagerUI) return;
        GameManager.Instance.IsPaused = false;
        Time.timeScale = 1f;
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
        UnlockMouse();
        AudioManager.Instance.PlaySound(_uIBt);
        Time.timeScale = 1.0f;
        SceneManager.LoadScene(0); // 타이틀 씬으로
    }

    private void StopGame()
    {
        AudioManager.Instance.PlaySound(_openPenalSound);
        _isOpened = !_isOpened;
        if (_pausePanel != null)
            _pausePanel.SetActive(_isOpened);

        if (GameManager.Instance.IsOpenedAbilityManagerUI) return;
        GameManager.Instance.IsPaused = !GameManager.Instance.IsPaused;
        Time.timeScale = GameManager.Instance.IsPaused ? 0f : 1f;
        
        if (GameManager.Instance.IsPaused)
        {
            UnlockMouse();
        }
        else
        {
            LockMouse();
        }
    }

    private void LockMouse()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    private void UnlockMouse()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }
}
