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

    public virtual void OnLookEnter(InteractableUI ui)
    {
        ui.Show(message);

    }

    public virtual void OnLookExit(InteractableUI ui)
    {
        ui.Hide();
    }

    protected virtual void Interact()
    {

    }
}
