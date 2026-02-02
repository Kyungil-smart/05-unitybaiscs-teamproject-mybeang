using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class BattleFieldUI : MonoBehaviour
{
    [Header("Player")]
    [SerializeField] private PlayerStatus _playerStatus;
    [SerializeField] private PlayerLevel _playerLevel;
    
    [Header("HP")]
    [SerializeField] private Image _playerHPbar;
    [SerializeField] private TextMeshProUGUI _playerHptext;

    [Header("EXP")]
    [SerializeField] private Image _expBarFill;           // ExpBarFill
    [SerializeField] private TextMeshProUGUI _expText;    // ExpText (선택)
    [SerializeField] private TextMeshProUGUI _levelText;
    [SerializeField] private TextMeshProUGUI _remainTimeText;

    [Header("Weapon")]
    [SerializeField] private PistolStatus _pistolStatus;
    [SerializeField] private RifleStatus _rifleStatus;
    [SerializeField] private GrenadeStatus _grenadeStatus;
    
    [Header("Ammo Text")]
    [SerializeField] private TextMeshProUGUI _pistolText;
    [SerializeField] private TextMeshProUGUI _rifleText;
    [SerializeField] private TextMeshProUGUI _grenadeText;

    private void Start()
    {
        GameManager.Instance.GameStart();
        SetHp(_playerStatus.CurrentHp, _playerStatus.TotalHp);
        SetPistolMegazine(_pistolStatus.CurrentMagazine, _pistolStatus.TotalMagazine);
        SetRifleMegazine(_rifleStatus.CurrentMagazine, _rifleStatus.TotalMagazine);
        SetGrenadeMegazine(_grenadeStatus.CurrentMagazine, _grenadeStatus.TotalMagazine);
    }

    private void OnEnable()
    {
        GameManager.Instance.OnTimerSecondsChanged.AddListener(SetRemainTime);
        _playerStatus.OnCurrentHpChanged.AddListener(SetHp);
        _playerStatus.OnTotalHpChanged.AddListener(SetHp);
        _playerLevel.OnExpbarChange.AddListener(SetExp);
        _playerLevel.OnLevelChange.AddListener(SetLevel);
        _pistolStatus.OnCurrentMagazineChanged.AddListener(SetPistolMegazine);
        _pistolStatus.OnTotalMagazineChanged.AddListener(SetPistolMegazine);
        _rifleStatus.OnCurrentMagazineChanged.AddListener(SetRifleMegazine);
        _rifleStatus.OnTotalMagazineChanged.AddListener(SetRifleMegazine);
        _grenadeStatus.OnCurrentMagazineChanged.AddListener(SetGrenadeMegazine);
        _grenadeStatus.OnTotalMagazineChanged.AddListener(SetGrenadeMegazine);
    }

    private void OnDisable()
    {
        GameManager.Instance.OnTimerSecondsChanged.RemoveListener(SetRemainTime);
        _playerStatus.OnCurrentHpChanged.RemoveListener(SetHp);
        _playerStatus.OnTotalHpChanged.RemoveListener(SetHp);
        _playerLevel.OnExpbarChange.RemoveListener(SetExp);
        _playerLevel.OnLevelChange.RemoveListener(SetLevel);
        _pistolStatus.OnCurrentMagazineChanged.RemoveListener(SetPistolMegazine);
        _pistolStatus.OnTotalMagazineChanged.RemoveListener(SetPistolMegazine);
        _rifleStatus.OnCurrentMagazineChanged.RemoveListener(SetRifleMegazine);
        _rifleStatus.OnTotalMagazineChanged.RemoveListener(SetRifleMegazine);
        _grenadeStatus.OnCurrentMagazineChanged.RemoveListener(SetGrenadeMegazine);
        _grenadeStatus.OnTotalMagazineChanged.RemoveListener(SetGrenadeMegazine);
    }

    public void SetHp(int currentHp, int maxHP)
    {
        maxHP = Mathf.Max(1, maxHP);
        currentHp = Mathf.Clamp(currentHp, 0, maxHP);
        Debug.Log("HP 갱신");
        if (_playerHPbar != null)
            _playerHPbar.fillAmount = currentHp / (float)maxHP;

        if (_playerHptext != null)
            _playerHptext.text = $"{currentHp} / {maxHP}";
    }

    // ⭐ 경험치 바 갱신용
    public void SetExp(int curExp, int maxExp)
    {
        maxExp = Mathf.Max(1, maxExp);
        curExp = Mathf.Clamp(curExp, 0, maxExp);
        
        if (_expBarFill != null)
            _expBarFill.fillAmount = curExp / (float)maxExp;

        if (_expText != null)
            _expText.text = $"EXP : {curExp} / {maxExp}";
    }

    public void SetLevel(int level)
    {
        _levelText.text = $"Lv. {level}";
    }
    
    // 탄약은 나중에
    public void SetPistolMegazine(int current, int max)
    {
        _pistolText.text = $"Pistol: {current} / {max}";
    }
    public void SetRifleMegazine(int current, int max)
    {
        _rifleText.text = $"Rifle: {current} / {max}";
    }
    public void SetGrenadeMegazine(int current, int max)
    {
        _grenadeText.text = $"Grenade: {current} / {max}";
    }

    private void SetRemainTime()
    {
        int t = GameManager.Instance.TimerSeconds;
        string timer = $"{(t / 60):D2}:{(t % 60):D2}"; 
        _remainTimeText.text = timer;
    }
}
