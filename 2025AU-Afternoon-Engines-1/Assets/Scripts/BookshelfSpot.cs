using System.Linq;
using UnityEngine;

public class BookshelfSpot : Interactable
{
    public GameObject shelfBookObject;
    public Sprite keyIcon;
    public GameObject keyPrefab;

    protected override void Interact()
    {
        HotbarInventory inv = HotbarInventory.Instance;
        HotbarSlot slot = inv.slots[inv.selectedSlot];

        if (!slot.hasItem || slot.itemID != "Book")
        {
            Debug.Log("You need the book selected.");
            return;
        }

        slot.ClearItem();

        HotbarItemDisplay.Instance.ShowItem(slot);


        if (shelfBookObject != null)
            shelfBookObject.SetActive(true);

        foreach (HotbarSlot s in inv.slots)
        {
            if (!s.hasItem)
            {
                s.SetPrefabItem(keyIcon, "Key", keyPrefab);

                HotbarItemDisplay.Instance.ShowItem(s);

                break;
            }
        }
    }
}

