using UnityEngine;

public class AudioController : SingletonMonoBehaviour<AudioController>
{
    [SerializeField] private AudioSource _audioSource;
    [SerializeField] private AudioClip _zombieDeadSound;
    [SerializeField] private AudioClip _playerGetHitSound;

    public void PlayZombieDeadSound()
    {
        _audioSource.PlayOneShot(_zombieDeadSound);
    }

    public void PlayPlayerGetHitSound()
    {
        _audioSource.PlayOneShot(_playerGetHitSound);
    }

    public void StopAllSounds()
    {
        _audioSource.Stop();
    }
}