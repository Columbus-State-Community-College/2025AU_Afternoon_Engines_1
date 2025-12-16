using UnityEngine;

public class TVInteract : Interactable
{
    public GameObject tvStatic;

    AudioManager audioManager;
    GameObject picFramesUp;
    GameObject picFramesDown;


    void Awake()
    {
        audioManager = GameObject.FindGameObjectWithTag("AudioManager").GetComponent<AudioManager>();
        picFramesUp = GameObject.FindGameObjectWithTag("PicFramesUp");
        picFramesDown = GameObject.FindGameObjectWithTag("PicFramesDown");
        message = "";

        picFramesUp.SetActive(true);
        picFramesDown.SetActive(false);
    }

    protected override void Interact()
    {
        if (MemoryCollectiing.tvTriggerActive)
        {
            message = "";
            tvStatic.SetActive(false);
            audioManager.StopTVSFX();

            picFramesUp.SetActive(false);
            picFramesDown.SetActive(true);
            audioManager.PlaySFX(audioManager.fallingFrames);
        }
    }
}
