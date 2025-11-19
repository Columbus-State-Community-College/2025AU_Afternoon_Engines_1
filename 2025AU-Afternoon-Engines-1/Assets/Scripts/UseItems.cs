using UnityEngine;

public class UseItems : MonoBehaviour
{
    public FlashlightSystem flashlight;
    //public PlayerHealth playerHealth;

    public void UseItem(Item item)
    {
        switch (item.useType)
        {
            case ItemUseType.RestoreBattery:
                flashlight.restoreCharge(item.value);
                break;

            //case ItemUseType.HealPlayer:
            //    playerHealth.Heal(item.value);
            //    break;
        }
    }
}
