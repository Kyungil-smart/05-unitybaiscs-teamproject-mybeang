using System.Collections;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    private AudioSource _audioSource;
    private static AudioManager _instance;
    public static AudioManager Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = FindObjectOfType<AudioManager>();
            }
            return _instance;
        }
    }
    private void Awake()
    {
        Init();
    }

    private void Init()
    {
        _audioSource = GetComponent<AudioSource>();
        if (_audioSource == null)
        {
            _audioSource = gameObject.AddComponent<AudioSource>();
        }
    }

    public void PlaySound(AudioClip _audioClip)
    {
        if (_audioClip == null) return;
        _audioSource.PlayOneShot(_audioClip);
    }

    public Coroutine StartPlaySoundContinuous(AudioClip _audioClip, float interval = 3f)
    {
        interval += _audioClip.length;
        return StartCoroutine(PlaySoundCoroutine(_audioClip, interval));
    }

    public void StopPlaySoundContinuous(Coroutine _coroutine)
    {
        if (_coroutine == null) return;
        StopCoroutine(_coroutine);
    }

    private IEnumerator PlaySoundCoroutine(AudioClip _audioClip, float interval)
    {
        while (true)
        {
            PlaySound(_audioClip);
            yield return YieldContainer.WaitForSeconds(interval);
        }
    }
}