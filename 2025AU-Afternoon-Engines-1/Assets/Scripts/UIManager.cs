using UnityEngine;

public class UIManager : MonoBehaviour
{
    // Class for showing/hiding UI menus
    // Set isMenuOpen to true or false, static so it can be widely used
    public static bool isMenuOpen;

    public GameObject inventoryMenu;
    
    public void ShowInventoryMenu()
    {
        inventoryMenu.SetActive(true);
        isMenuOpen = true;
        Time.timeScale = 0f;
        Cursor.lockState = CursorLockMode.None;

    }

    public void HideInventoryMenu()
    {
        inventoryMenu.SetActive(false);
        isMenuOpen = false;
        Time.timeScale = 1f;
        Cursor.lockState = CursorLockMode.Locked;
    }

   
}
