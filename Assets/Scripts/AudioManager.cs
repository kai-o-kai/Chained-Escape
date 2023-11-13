using System;
using UnityEngine;
using UnityEngine.Audio;

public class AudioManager : MonoBehaviour {
    public static AudioManager Instance { get; private set; }
    
    [SerializeField] private Sound[] _sounds;
    [SerializeField] private AudioMixerGroup _sfxGroup;
    [SerializeField] private AudioMixerGroup _musicGroup;
    
    private void Awake() {
        Instance = this;
        foreach (Sound toInit in _sounds) {
            AudioSource newSource = gameObject.AddComponent<AudioSource>();
            toInit.Initialize(newSource, _sfxGroup, _musicGroup);
        }
    }
    public void PlaySound(string name) {
        GetSoundByName(name)?.Play();
    }
    public void StopSound(string name) {
        GetSoundByName(name)?.Stop();
    }
    public void PlayOneShot(string name) {
        GetSoundByName(name)?.PlayOneShot();
    }
    private Sound GetSoundByName(string name) {
        return Array.Find(_sounds, x => x.name == name);
    }
    public enum SoundType {
        SFX, Music
    }
}