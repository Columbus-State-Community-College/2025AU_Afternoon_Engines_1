using UnityEngine;

public class DoorInteract : Interactable
{
    public Door parentDoor;

    protected override void Interact()
    {
        if (parentDoor != null)
            parentDoor.ToggleDoor();
    }
}
