using UnityEngine;

public class SoundEffectController : MonoBehaviour
{
    private AudioSource _audioSource;
    [SerializeField] private AudioClip[] _soundEffects;

    private void Start()
    {
        _audioSource = GetComponent<AudioSource>();
    }

    public void PlaySoundEffect(int index)
    {
        _audioSource.clip = _soundEffects[index];
        _audioSource.Play();

    }

}
