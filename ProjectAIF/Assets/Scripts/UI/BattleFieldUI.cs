using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class BattleFieldUI : MonoBehaviour
{
    [SerializeField] private Image _playerHPbar;
    [SerializeField] private TextMeshProUGUI _playerHptext;

    [SerializeField] private TextMeshProUGUI _pistolText;
    [SerializeField] private TextMeshProUGUI _rifleText;
    [SerializeField] private TextMeshProUGUI _grenadeText;

    public void SetHP(int currentHp, int maxHP)
    {
        maxHP = Mathf.Max(0, maxHP);
        currentHp = Mathf.Clamp(currentHp, 0, maxHP);

        if (_playerHPbar != null)
        {
            _playerHPbar.fillAmount = currentHp / maxHP;
        }

        if (_playerHptext != null)
        {
            _playerHptext.text = $"{currentHp} / {maxHP}";
        }

    }

    public void SetMegazine(int current, int max, WeaponType Type)
    {
        string Megazine = $"{current} /{max}";
    }
    public enum WeaponType 
    {
        pistol= 0, rifle= 1, grenade= 2
    }
}
