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

    public void ShowItem(HotbarSlot slot)
    {
        if (currentItem != null)
            Destroy(currentItem);

        if (slot.sceneItem != null)
            slot.sceneItem.SetActive(false);

        // nothing selected
        if (!slot.hasItem)
            return;

        if (slot.sceneItem != null)
        {
            slot.sceneItem.SetActive(true);
            return;
        }

        if (slot.prefabItem != null)
            currentItem = Instantiate(slot.prefabItem, handAnchor);
    }
}
