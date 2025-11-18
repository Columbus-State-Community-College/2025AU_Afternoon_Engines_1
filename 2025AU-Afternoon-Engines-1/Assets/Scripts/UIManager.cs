using UnityEngine;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    // Class for showing/hiding UI menus
    // Set isMenuOpen to true or false, static so it can be widely used
    public static bool isMenuOpen;

    public GameObject[] menus;

    public string menuScene;
    
    public void ShowMenu(int menuIndex)
    {
        menus[menuIndex].SetActive(true);
        isMenuOpen = true;
        Time.timeScale = 0f;
        Cursor.lockState = CursorLockMode.None;

    }

    public void HideMenu(int menuIndex)
    {
        menus[menuIndex].SetActive(false);
        isMenuOpen = false;
        Time.timeScale = 1f;
        Cursor.lockState = CursorLockMode.Locked;
    }

    public void ReturnToMainMenu()
    {
        ShowCursor();
        SceneManager.LoadScene(menuScene);
    }

    private void ShowCursor()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

}

