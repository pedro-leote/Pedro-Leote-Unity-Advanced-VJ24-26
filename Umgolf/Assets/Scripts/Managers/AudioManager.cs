using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Um manager de 1ª source genérico para áudio. Tentei fazer com que fosse abstrato o suficiente para mover entre projetos.
public class AudioManager : MonoBehaviour
{
    private AudioSource _audioSource;
    private static AudioManager _instance;
    public static AudioManager Instance
    {
        get
        {
            if (Instance == null)
            {
                _instance = new AudioManager();
            }
            
            return _instance;
        }
    }
    // Start is called before the first frame update
    private void Awake()
    {

        _audioSource.GetComponent<AudioSource>();
        DontDestroyOnLoad(AudioManager.Instance);
    }
    

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
