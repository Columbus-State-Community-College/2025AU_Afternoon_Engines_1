using UnityEngine;

public abstract class Interactable : MonoBehaviour
{
    // Abstract class that any pickups can inherit from
    // message can be used for ui hint text
    public string message;

    public void BaseInteract()
    {
        Interact();
    }

    protected virtual void Interact()
    {

    }
}
