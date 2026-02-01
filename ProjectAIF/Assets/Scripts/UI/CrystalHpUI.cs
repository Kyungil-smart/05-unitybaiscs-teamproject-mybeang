using UnityEngine;
using UnityEngine.UI;

public class CrystalHpUI : MonoBehaviour
{
    [SerializeField] private Image _hpBar;
    [SerializeField] private Canvas _uiCanvas;
    private Crystal _crystal;

    private Camera _camera;

    private void Awake()
    {
        Init();
        transform.position = _crystal.transform.position;
    }

    private void Update()
    {
        _uiCanvas.transform.forward = _camera.transform.forward;
    }

    private void Init()
    {
        _crystal = GetComponent<Crystal>();
        _camera = Camera.main;
        _crystal.OnHpChanged.AddListener(RefreshUI);
        RefreshUI();
    }
    
    private void RefreshUI()
    {
        _hpBar.fillAmount = _crystal.CurrentHp / (float)_crystal.MaxHp;
    }
}
