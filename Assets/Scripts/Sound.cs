using UnityEngine;
using UnityEngine.Audio;
using static AudioManager.SoundType;

[CreateAssetMenu(menuName = "Scriptable Objects/Sound", fileName = "New Sound")]
public class Sound : ScriptableObject {
    [SerializeField] private AudioClip _clip;
    [SerializeField] private AudioManager.SoundType _soundType;
    [SerializeField] private float _volume = 1f;
    [SerializeField] private float _pitch = 1f;
    [SerializeField] private bool _loop;
    [SerializeField] private bool _playOnAwake;

    private AudioSource _source;

    public void Initialize(AudioSource source, AudioMixerGroup sfxGroup, AudioMixerGroup musicGroup) {
        _source = source;
        _source.clip = _clip;
        _source.volume = _volume;
        _source.pitch = _pitch;
        _source.loop = _loop;
        _source.outputAudioMixerGroup = _soundType switch {
            SFX => sfxGroup,
            Music => musicGroup,
            _ => null
        };
        if (_playOnAwake) {
            _source.Play();
        }
    }
    public void Play() => _source.Play();
    public void Stop() => _source.Stop();
    public void PlayOneShot() => _source.PlayOneShot(_clip);
}