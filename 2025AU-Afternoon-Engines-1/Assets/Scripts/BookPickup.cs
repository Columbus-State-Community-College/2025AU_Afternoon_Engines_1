using UnityEngine;

public class BookPickup : Interactable
{
    public string itemID = "Book";
    public Sprite icon;
    public GameObject holdingPrefab;

    void Awake()
    {
        message = "Pick Up Book";
    }
    protected override void Interact()
    {
        bool added = HotbarInventory.Instance.AddItem(icon, itemID, holdingPrefab);

        if (added)
            gameObject.SetActive(false);
        else
            Debug.Log("Hotbar full!");
    }
}
