using UnityEngine;
using System;
using UnityEngine.Audio;

public class AudioManager : MonoBehaviour
{
    public AudioSource AmbienceSource;
    public AudioSource SFXSource;
    public AudioSource JumpScareSource;
    public AudioMixer audioMixer;
   

    [Header("Ambience: ")]
    public AudioClip ambience;
    [Header("SFX")]
     
    public AudioClip flashlightSFX;
    public AudioClip dialougeClick;
    public AudioClip memoryPage;

    [Header("Jumpscare Audio")]
    public AudioClip clockSFX;

    public static AudioManager instance;
    private void Awake()
    {
        audioMixer.SetFloat("Ambience", Mathf.Log10(PlayerPrefs.GetFloat("AmbienceVolume")) * 20);
        audioMixer.SetFloat("SFX", Mathf.Log10(PlayerPrefs.GetFloat("SFXVolume")) * 20);
        audioMixer.SetFloat("Jumpscare", Mathf.Log10(PlayerPrefs.GetFloat("JumpscareVolume")) * 20);

        if (instance == null )
        {
            instance = this;
            DontDestroyOnLoad(this);
        }

        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        

        AmbienceSource.clip = ambience;
        AmbienceSource.Play(); 
    }

    public void PlaySFX(AudioClip clip)
    {
        SFXSource.PlayOneShot(clip); 
    }

    public void PlayJumpScare(AudioClip clip)
    {
        JumpScareSource.PlayOneShot(clip);
    }



}  
 