using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using TestGame.Scripts.Tools;
using UnityEngine;

public class GameAudioController : MonoBehaviour
{

    [Header("Audio")]
    [SerializeField]
    private AudioSource _gameAudioSource;
    [SerializeField]
    private List<AudioClipContainer> _audioClips = new List<AudioClipContainer>();

    public void Initialize()
    {
        ChangeClipHard("menu_music", true);
        _gameAudioSource.Play();
    }

    /// <summary>
    /// Replaces the audio clip instantly. If you need a default sound
    /// </summary>
    /// <param name="clipName">Clip name on AudioClipContainer</param>
    /// <param name="isLoop"></param>
    public void ChangeClipHard(string clipName, bool isLoop = false)
    {
        var clip = _audioClips.Find(container => container.Name == clipName).Clip;

        if (clip != null)
        {
            ChangeClip(clip);
            _gameAudioSource.loop = isLoop;
            _gameAudioSource.Play();
        }
        else
            Debug.LogError($"No clip with the name {clipName} was found!");
    }
    /// <summary>
    /// Replaces the audio clip smoothly. If you need a default sound
    /// </summary>
    /// <param name="clipName">Clip name on AudioClipContainer</param>
    /// <param name="fadeDuration">Smooth transition time</param>
    /// <param name="isLoop"></param>
    public void ChangeClipSmoothly(string clipName,float fadeDuration =  1f, bool isLoop = false)
    {
        var clip = _audioClips.Find(container => container.Name == "loose_song").Clip;

        if (clip != null)
        {
            _gameAudioSource.DOFade(0f, fadeDuration).OnComplete(() =>
            {
                ChangeClip(clip);
                _gameAudioSource.loop = isLoop;
                _gameAudioSource.Play();
                _gameAudioSource.DOFade(1f, fadeDuration);
            });
        }
        else
            Debug.LogError($"No clip with the name {clipName} was found!");
    }   
    /// <summary>
    /// Replaces the audio clip instantly
    /// </summary>
    /// <param name="clip">Audio clip</param>
    /// <param name="isLoop"></param>
    public void ChangeClipHard(AudioClip clip, bool isLoop = false)
    {
        if (clip != null)
        {
            ChangeClip(clip);
            _gameAudioSource.loop = isLoop;
            _gameAudioSource.Play();
        }
        else
            Debug.LogError($"No clip with the name {clip.name} was found!");
    }   
    /// <summary>
    /// Replaces the audio clip smoothly
    /// </summary>
    /// <param name="clip">Audio clip</param>
    /// <param name="isLoop"></param>
    public void ChangeClipSmoothly(AudioClip clip, float fadeDuration = 1f, bool isLoop = false)
    {

        if (clip != null)
        {
            _gameAudioSource.DOFade(0f, fadeDuration).OnComplete(() =>
            {
                ChangeClip(clip);
                _gameAudioSource.loop = isLoop;
                _gameAudioSource.Play();
                _gameAudioSource.DOFade(1f, fadeDuration);
            });
        }
        else
            Debug.LogError($"No clip with the name {clip} was found!");
    }
    
    private void ChangeClip(AudioClip clip)
    {
        _gameAudioSource.Stop();
        _gameAudioSource.clip = clip;
    }

}
