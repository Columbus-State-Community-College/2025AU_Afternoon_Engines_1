using NUnit;
using UnityEngine;

public class DoorInteract : Interactable
{
    public Door parentDoor;

    protected override void Interact()
    {
        if (!parentDoor.isLocked)
        {
            parentDoor.ToggleDoor();
            return;
        }

        if (PlayerHasKey())
        {
            Debug.Log("Unlocked with key.");
            parentDoor.Unlock();
            parentDoor.ToggleDoor();
        }
        else
        {
            Debug.Log("Door is locked. You need a key.");
        }
    }

    private bool PlayerHasKey()
    {
        HotbarInventory inv = HotbarInventory.Instance;
        HotbarSlot slot = inv.slots[inv.selectedSlot];

        if (slot.hasItem && slot.itemID == "Key")
        {
            slot.ClearItem();
            HotbarItemDisplay.Instance.ClearHeldItem();
            return true;
        }

        
        return false;
    }

}
