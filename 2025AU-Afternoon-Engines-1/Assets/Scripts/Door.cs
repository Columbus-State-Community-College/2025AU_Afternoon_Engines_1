using UnityEngine;

public class Door : MonoBehaviour
{
    public Animator animator;
    private bool isOpen;

    AudioManager audioManager;


    private void Awake()
    {
        audioManager = GameObject.FindGameObjectWithTag("AudioManager").GetComponent<AudioManager>();
    }
    public void ToggleDoor()
    {
        isOpen = !isOpen;
        animator.SetBool("isOpen", isOpen);
       audioManager.PlaySFX(audioManager.doorOpen);
    }
}
