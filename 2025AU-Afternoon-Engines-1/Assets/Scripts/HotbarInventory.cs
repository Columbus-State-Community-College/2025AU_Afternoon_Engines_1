using UnityEngine;

public class HotbarInventory : MonoBehaviour
{
    public static HotbarInventory Instance;

    public HotbarSlot[] slots;
    public int selectedSlot = 0;

    public Sprite flashlightIcon;
    public GameObject flashlightSceneObject;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        slots[0].SetSceneItem(flashlightIcon, "Flashlight", flashlightSceneObject);

        UpdateSelection();
    }

    private void Update()
    {
        float scroll = Input.GetAxis("Mouse ScrollWheel");
        if (scroll > 0f)
            SelectNext();
        else if (scroll < 0f)
            SelectPrev();
    }

    void SelectNext()
    {
        selectedSlot = (selectedSlot + 1) % slots.Length;
        UpdateSelection();
    }

    void SelectPrev()
    {
        selectedSlot--;
        if (selectedSlot < 0) selectedSlot = slots.Length - 1;
        UpdateSelection();
    }

    public bool AddItem(Sprite icon, string itemID, GameObject prefabItem)
    {
        foreach (HotbarSlot slot in slots)
        {
            if (!slot.hasItem)
            {
                slot.SetPrefabItem(icon, itemID, prefabItem);
                return true;
            }
        }
        return false;
    }

    void UpdateSelection()
    {
        UpdateSlotHighlight();
        HotbarItemDisplay.Instance.ShowItem(slots[selectedSlot]);
    }

    void UpdateSlotHighlight()
    {
        for (int i = 0; i < slots.Length; i++)
            slots[i].SetSelected(i == selectedSlot);
    }
}
