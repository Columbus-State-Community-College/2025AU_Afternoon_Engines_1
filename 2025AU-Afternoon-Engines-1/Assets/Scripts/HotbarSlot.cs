using UnityEngine;
using UnityEngine.UI;

public class HotbarSlot : MonoBehaviour
{
    public Image iconImage;
    public Image selectionHighlight;

    public bool hasItem;
    public string itemID;
    public GameObject prefabItem;
    public GameObject sceneItem;

    public void SetPrefabItem(Sprite icon, string id, GameObject prefab)
    {
        hasItem = true;
        itemID = id;
        prefabItem = prefab;
        sceneItem = null;

        iconImage.sprite = icon;
        iconImage.gameObject.SetActive(true);
    }

    public void SetSceneItem(Sprite icon, string id, GameObject sceneObj)
    {
        hasItem = true;
        itemID = id;
        sceneItem = sceneObj;
        prefabItem = null;

        iconImage.sprite = icon;
        iconImage.gameObject.SetActive(true);
    }

    public void ClearItem()
    {
        hasItem = false;
        itemID = "";
        prefabItem = null;
        sceneItem = null;

        iconImage.gameObject.SetActive(false);
    }

    public void SetSelected(bool selected)
    {
        if (selectionHighlight != null)
            selectionHighlight.gameObject.SetActive(selected);
    }
}
