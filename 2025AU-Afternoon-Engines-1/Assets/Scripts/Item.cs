using UnityEngine;


public enum ItemUseType
{
    None,
    RestoreBattery,
    HealPlayer
}

[CreateAssetMenu(fileName ="New Item", menuName ="Item/Create New Item")]
public class Item : ScriptableObject
{
    //Info for Items, can create new with the asset menu

    public int id;
    public string itemName;
    public string itemDescription;
    public int value;
    public ItemUseType useType;
    public Sprite icon;
}
