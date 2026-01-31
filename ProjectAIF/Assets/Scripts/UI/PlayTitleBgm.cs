using UnityEngine;

public class PlayTitleBgm : MonoBehaviour
{
    public AudioClip Clip;
    
    private AudioSource _audioSource;

    private void Awake()
    {
        _audioSource = GetComponent<AudioSource>();
        if (_audioSource == null)
        {
            _audioSource = gameObject.AddComponent<AudioSource>();
        }
    }

    private void Start()
    {
        if (Clip == null) return;
        _audioSource.PlayOneShot(Clip);
    }
}
