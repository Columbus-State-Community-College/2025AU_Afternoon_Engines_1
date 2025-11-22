using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;


public class VolumeSlider : MonoBehaviour
{
    public AudioMixer ambienceMixer;
    public Slider ambienceSlider;
    public AudioMixer SFXMixer;
    public Slider SFXSlider;
    public AudioMixer JumpMixer;
    public Slider JumpSlider;

    private void Start()
    {
        if (PlayerPrefs.HasKey("AmbienceVolume"))
        {
            LoadVolume();
           
        }

        if (PlayerPrefs.HasKey("SFXVolume"))
        {
            LoadSFX();

        }

        if (PlayerPrefs.HasKey("JumpscareVolume"))
        {
            LoadJumpScareVolume();

        }

        else
        {
            SetVolume();
            SetSFXVolume();
            SetJumpscareVolume();
        }
    }
    public void SetVolume()
    {
        float volume = ambienceSlider.value;
        ambienceMixer.SetFloat("Ambience", Mathf.Log10(volume)*20);
        PlayerPrefs.SetFloat("AmbienceVolume", volume);
        PlayerPrefs.Save();
    }

    public void SetSFXVolume()
    {
        float volume = SFXSlider.value;
        SFXMixer.SetFloat("SFX", Mathf.Log10(volume) * 20);
        PlayerPrefs.SetFloat("SFXVolume", volume);
        PlayerPrefs.Save();
    }

    public void SetJumpscareVolume()
    {
        float volume = JumpSlider.value;
        JumpMixer.SetFloat("Jumpscare", Mathf.Log10(volume) * 20);
        PlayerPrefs.SetFloat("JumpscareVolume", volume);
        PlayerPrefs.Save();
    }

    private void LoadVolume()
    {
        ambienceSlider.value = PlayerPrefs.GetFloat("AmbienceVolume");
        SetVolume();
        
    }

    private void LoadSFX()
    {
        SFXSlider.value = PlayerPrefs.GetFloat("SFXVolume");
        SetSFXVolume();
    }

    private void LoadJumpScareVolume()
    {
        JumpSlider.value = PlayerPrefs.GetFloat("JumpscareVolume");
     

        SetJumpscareVolume();
    }
     
}
