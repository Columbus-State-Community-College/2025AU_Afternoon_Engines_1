using UnityEngine;
using System.Collections.Generic;
using UnityEngine.UI;

public class InventoryManager : MonoBehaviour
{
    public static InventoryManager Instance;
    public List<Item> Items = new List<Item>();

    // Content from UI and item prefab to set name and image
    public Transform ItemContent;
    public GameObject InventoryItem;

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
        }
    }
}
