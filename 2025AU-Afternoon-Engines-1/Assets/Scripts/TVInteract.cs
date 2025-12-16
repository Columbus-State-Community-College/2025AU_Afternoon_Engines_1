using UnityEngine;

public class TVInteract : Interactable
{
    public GameObject tvStatic;

    AudioManager audioManager;


    void Awake()
    {
        audioManager = GameObject.FindGameObjectWithTag("AudioManager").GetComponent<AudioManager>();
        message = "Turn off TV";
    }

    protected override void Interact()
    {
        tvStatic.SetActive(false);
        audioManager.StopTVSFX();
    }
}
