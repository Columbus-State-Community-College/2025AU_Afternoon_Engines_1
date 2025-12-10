using NUnit;
using UnityEngine;

public class DoorInteract : Interactable
{
    public Door parentDoor;
    public string lockedMessage = "Door is locked. You need a key.";
    public string unlockMessage = "Unlock Door";


    public override void OnLookEnter(InteractableUI ui)
    {
        HotbarSlot selectedSlot = HotbarInventory.Instance.slots[HotbarInventory.Instance.selectedSlot];
        bool holdingKey = selectedSlot.hasItem && selectedSlot.itemID == "Key";

        if (parentDoor.isLocked)
        {
            message = holdingKey ? unlockMessage : lockedMessage;
        }
        else
        {
            message = "";
        }

        base.OnLookEnter(ui);
    }

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
