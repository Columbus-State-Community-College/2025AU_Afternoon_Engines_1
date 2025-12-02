using UnityEngine;

public class Door : MonoBehaviour
{
    public Animator animator;
    private bool isOpen;

    public void ToggleDoor()
    {
        isOpen = !isOpen;
        animator.SetBool("isOpen", isOpen);
    }
}
