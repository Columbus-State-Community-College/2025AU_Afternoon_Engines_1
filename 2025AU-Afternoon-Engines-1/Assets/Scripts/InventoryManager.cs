using UnityEngine;
using System.Collections.Generic;
using UnityEngine.UI;

public class InventoryManager : MonoBehaviour
{
    public static InventoryManager Instance;
    public List<Item> Items = new List<Item>();
    public UIManager uiManager;

    public UseItems useItems;

    // Content from UI and item prefab to set name and image
    public Transform ItemContent;
    public GameObject InventoryItem;

    public GameObject useItemPanel;
    public Button useButton;
    public Button cancelButton;

    private Item selectedItem;

    private void Awake()
    {
        Instance = this;
    }

    public void Add(Item item)
    {
        Items.Add(item);
    }

    public void ListItems()
    {
        // Cleans up so there are no item duplicates
        foreach (Transform item in ItemContent)
        {
            Destroy(item.gameObject);
        }
        // Instantiates item in inventory (different than add item, this displays item
        // instead of adding it to a list)
        foreach (var item in Items)
        {
            GameObject obj = Instantiate(InventoryItem, ItemContent);

            var itemName = obj.transform.Find("ItemName").GetComponent<TMPro.TextMeshProUGUI>();
            var itemIcon = obj.transform.Find("ItemIcon").GetComponent<Image>();

            itemName.text = item.itemName;
            itemIcon.sprite = item.icon;

            obj.GetComponent<InventoryItemUI>().SetItem(item);
        }
    }

    public void OpenUseMenu(Item item)
    {
        selectedItem = item;
        useItemPanel.SetActive(true);

    }

    public void UseSelectedItem()
    {
        Debug.Log("Using item: " + selectedItem.itemName);

        useItems.UseItem(selectedItem);

        Remove(selectedItem);

        selectedItem = null;

        useItemPanel.SetActive(false);
    }

    public void Remove(Item item)
    {
        if (Items.Contains(item))
            Items.Remove(item);

        ListItems();
    }
}
