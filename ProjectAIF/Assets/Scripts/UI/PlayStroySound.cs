using UnityEngine;

public class PlayStroySound : MonoBehaviour
{
    [SerializeField] private AudioClip _storySound;
    private void Start()
    {
        AudioManager.Instance.PlaySound(_storySound);
    }
}
