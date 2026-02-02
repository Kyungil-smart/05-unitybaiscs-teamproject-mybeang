using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AbilityUIController : MonoBehaviour
{
    [SerializeField] private PlayerLevel _playerLevel;
    [SerializeField] private Canvas _abilityPanel;

    [SerializeField] private bool _pauseGameWhileSelecting = true;

    [SerializeField] private AudioClip _popUpSound;
    
    private void Awake()
    {
        if(_playerLevel == null)
        {
            return;
        }

        if(_abilityPanel == null)
        {
            return;
        }
    }

    private void OnEnable()
    {
        if( _playerLevel != null)
        {
            _playerLevel.OnLevelUp.AddListener(OpenAbilityUI);
        }
    }

    private void OnDisable()
    {
        _playerLevel.OnLevelUp.RemoveListener(OpenAbilityUI);
    }

    public void OpenAbilityUI(int level)
    {
        Debug.Log("[AbilityUIController] OpenAbilityUI called");
        Debug.Log($"[AbilityUIController] OpenAbilityUI param level = {level}");
        
        AbilityManager.Instance.ReadyToThreeAbilities();
        if(_abilityPanel != null)
        {
            _abilityPanel.enabled = true;
            AudioManager.Instance.PlaySound(_popUpSound);
        }
        if (_pauseGameWhileSelecting)
        {
            Time.timeScale = 0f;
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }
    }

    public void Confirm()
    {
        AbilityManager.Instance.ApplyAbility();


        if (_abilityPanel != null)
        {
            _abilityPanel.enabled = false;
        }

        if (_pauseGameWhileSelecting)
        {
            Time.timeScale = 1f;
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }

        if (_playerLevel != null)
        {
            _playerLevel.ConfirmAbility();
        }

    }
    // 이 밑에 있는 코드는 테스트용 코드입니다. 추후에 삭제 가능합니다.
#if UNITY_EDITOR
    [ContextMenu("DEBUG/Open Ability UI")]
    private void DebugOpenAbilityUI()
    {
        // 레벨업 없이도 선택지 세팅 + UI 오픈을 강제로 실행
        AbilityManager.Instance.ReadyToThreeAbilities();

        if (_abilityPanel != null)
        {
            _abilityPanel.enabled = true;
        }

        if (_pauseGameWhileSelecting)
        {
            Time.timeScale = 0f;
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }

        Debug.Log("[AbilityUIController] DEBUG/Open Ability UI");
    }

    [ContextMenu("DEBUG/Close Ability UI")]
    private void DebugCloseAbilityUI()
    {
        if (_abilityPanel != null)
        {
            _abilityPanel.enabled = false;
        }

        if (_pauseGameWhileSelecting)
        {
            Time.timeScale = 1f;
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }

        Debug.Log("[AbilityUIController] DEBUG/Close Ability UI");
    }
#endif

}
