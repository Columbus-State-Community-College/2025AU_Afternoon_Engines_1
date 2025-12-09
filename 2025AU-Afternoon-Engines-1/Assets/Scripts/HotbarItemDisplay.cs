using UnityEngine;

public class HotbarItemDisplay : MonoBehaviour
{
    public static HotbarItemDisplay Instance;

    public Transform handAnchor;  // On the camera
    private GameObject currentItem;

    private void Awake()
    {
        Instance = this;
    }

    public void ShowItem(HotbarSlot selectedSlot)
    {
        if (currentItem != null)
            Destroy(currentItem);

        foreach (HotbarSlot slot in HotbarInventory.Instance.slots)
        {
            if (slot.sceneItem != null && slot != selectedSlot)
                slot.sceneItem.SetActive(false);
        }

        if (!selectedSlot.hasItem)
            return;

        if (selectedSlot.sceneItem != null)
        {
            selectedSlot.sceneItem.SetActive(true);
            return;
        }

        if (selectedSlot.prefabItem != null)
            currentItem = Instantiate(selectedSlot.prefabItem, handAnchor);
    }

    public void SpawnItemInHand(HotbarSlot slot)
    {

        if (slot.prefabItem != null)    
        {
            currentItem = Instantiate(slot.prefabItem, handAnchor);
        }
    }

    public void ClearHeldItem()
    {
        if (currentItem != null)
        {
            Destroy(currentItem);
            currentItem = null;
        }
    }
}
