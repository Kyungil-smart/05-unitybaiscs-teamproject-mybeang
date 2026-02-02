using UnityEngine;

public class PlayCreditsSound : MonoBehaviour
{
    [SerializeField] private AudioClip _creditsSound;
    private void Start()
    {
        AudioManager.Instance.PlaySound(_creditsSound);
    }
}
