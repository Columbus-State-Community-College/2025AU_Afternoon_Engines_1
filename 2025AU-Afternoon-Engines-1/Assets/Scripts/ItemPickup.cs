using UnityEngine;

public class ItemPickup : Interactable
{
    public Item Item;

    // Adds item to inventory
    public void AddItem()
    {
        InventoryManager.Instance.Add(Item);
        Destroy(gameObject);
    }

    // Overrides base interact method in abstract class
    protected override void Interact()
    {
            AddItem();
    }

}
