using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Um manager de 1ª source genérico para áudio. Tentei fazer com que fosse abstrato o suficiente para mover entre projetos.
public class AudioManager : MonoSingleton<AudioManager>
{
    private AudioSource _audioSource;
    public override void Awake()
    {
        base.Awake();
        _audioSource.GetComponent<AudioSource>();
    }
    
    
    
    //These methods demonstram overloading do mesmo method com multiple parameters.
    public void PlayAudio(AudioClip audioClip)
    {
        _audioSource.clip = audioClip;
        _audioSource.Play();
    }
    
    public void PlayAudio(AudioClip audioClip, float volume)
    {
        _audioSource.volume = volume;
        _audioSource.clip = audioClip;
        _audioSource.Play();
    }
    public void PlayAsOneShot(AudioClip audioClip)
    {
        _audioSource.PlayOneShot(audioClip);
    }
    
    public void PlayAsOneShot(AudioClip audioClip, float volume)
    {
        _audioSource.volume = volume;
        _audioSource.PlayOneShot(audioClip);
    }
}
