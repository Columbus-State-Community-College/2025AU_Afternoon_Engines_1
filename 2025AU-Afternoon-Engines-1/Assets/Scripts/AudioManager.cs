using UnityEngine;
using System;

public class AudioManager : MonoBehaviour
{
    public AudioSource AmbienceSource;
    public AudioSource SFXSource;

    [Header("Ambience: ")]
    public AudioClip ambience;
    [Header("SFX")]
     
    public AudioClip flashlightSFX;
    public AudioClip dialougeClick;


    private void Start()
    {
        AmbienceSource.clip = ambience;
        AmbienceSource.Play(); 
    }

    public void PlaySFX(AudioClip clip)
    {
        SFXSource.PlayOneShot(clip); 
    }



}  
 