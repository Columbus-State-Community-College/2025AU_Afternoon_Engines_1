using UnityEngine;

public class InventoryItemUI : MonoBehaviour
{
    private Item item;

    public void SetItem(Item newItem)
    {
        item = newItem;
    }

    public void OnClickItem()
    {
        InventoryManager.Instance.OpenUseMenu(item);
    }
}
