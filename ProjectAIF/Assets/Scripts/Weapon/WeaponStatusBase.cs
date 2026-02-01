using UnityEngine;
using UnityEngine.Events;

public abstract class WeaponStatusBase : MonoBehaviour
{
    [Header("기본 성능")]
    public int Damage;
    [SerializeField] private int _currentMagazine;
    public UnityEvent<int, int> OnCurrentMagazineChanged = new UnityEvent<int, int>();
    public int CurrentMagazine
    {
        get { return _currentMagazine; }
        set
        {
            _currentMagazine = value;
            OnCurrentMagazineChanged?.Invoke(_currentMagazine, _totalMagazine);
        }
    }
    [SerializeField] private int _totalMagazine;
    public UnityEvent<int, int> OnTotalMagazineChanged = new UnityEvent<int, int>();
    public int TotalMagazine
    {
        get { return _totalMagazine; }
        set
        {
            _totalMagazine = value;
            OnCurrentMagazineChanged?.Invoke(_currentMagazine, _totalMagazine);
        }
    }
}
