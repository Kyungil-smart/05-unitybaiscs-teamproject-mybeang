using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class BattleFieldUI : MonoBehaviour
{
    [Header("HP")]
    [SerializeField] private Image _playerHPbar;
    [SerializeField] private TextMeshProUGUI _playerHptext;

    [Header("EXP")]
    [SerializeField] private Image _expBarFill;           // ExpBarFill
    [SerializeField] private TextMeshProUGUI _expText;    // ExpText (선택)

    [Header("Ammo Text")]
    [SerializeField] private TextMeshProUGUI _pistolText;
    [SerializeField] private TextMeshProUGUI _rifleText;
    [SerializeField] private TextMeshProUGUI _grenadeText;

    public void SetHP(int currentHp, int maxHP)
    {
        maxHP = Mathf.Max(1, maxHP);
        currentHp = Mathf.Clamp(currentHp, 0, maxHP);

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

    // 탄약은 나중에
    public void SetMegazine(int current, int max, WeaponType Type) { }

    public enum WeaponType
    {
        pistol = 0, rifle = 1, grenade = 2
    }
}
