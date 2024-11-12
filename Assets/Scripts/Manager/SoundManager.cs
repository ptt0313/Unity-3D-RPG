using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : Singleton<SoundManager>
{
    [SerializeField] private AudioSource musicSource;
    [SerializeField] private AudioSource effectsSource;
    [SerializeField] private AudioSource footstepSource;
    private Dictionary<string, AudioClip> audioClips = new Dictionary<string, AudioClip>();
    private void Start()
    {
        LoadAudioClips();
    }
    public void PlayMusic(string clipName)
    {
        if (audioClips.ContainsKey(clipName))
        {
            musicSource.clip = audioClips[clipName];
            musicSource.loop = true;
            musicSource.Play();
        }
    }
    public void PlayEffect(string clipName)
    {
        if (audioClips.ContainsKey(clipName))
        {
            effectsSource.PlayOneShot(audioClips[clipName]);
        }
    }
    // 오디오 클립 추가
    private void LoadAudioClips()
    {
        AudioClip[] clips = Resources.LoadAll<AudioClip>("Sounds");
        foreach (AudioClip clip in clips)
        {
            if (!audioClips.ContainsKey(clip.name))
            {
                audioClips.Add(clip.name, clip);
            }
        }
    }
    public void PlayFootstep(string clipName)
    {
        if (audioClips.ContainsKey(clipName))
        {
            footstepSource.clip = audioClips[clipName];
            footstepSource.loop = true;
            if (!footstepSource.isPlaying) footstepSource.Play();
        }
    }

    public void StopFootstep()
    {
        if (footstepSource.isPlaying)
        {
            footstepSource.Stop();
        }
    }
}
