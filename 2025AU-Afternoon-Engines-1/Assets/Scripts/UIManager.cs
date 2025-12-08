using UnityEngine;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    // Class for showing/hiding UI menus
    // Set isMenuOpen to true or false, static so it can be widely used
    public static bool isMenuOpen;

    public static int currentMenu = -1;

    public GameObject[] menus;

    public GameObject memoryCanvas;

    public GameObject deathScreen;

    public string menuScene = "MainMenu";

    AudioManager audioManager;

    private void Awake()
    {
        audioManager = GameObject.FindGameObjectWithTag("AudioManager").GetComponent<AudioManager>();
    }

    public void ShowMenu(int menuIndex)
    {
        for (int i = 0; i < menus.Length; i++)
        {
            menus[i].SetActive(false);
        }

        menus[menuIndex].SetActive(true);
        isMenuOpen = true;
        currentMenu = menuIndex;
        Time.timeScale = 0f;
        Cursor.lockState = CursorLockMode.None;

    }

    public void HideMenu(int menuIndex)
    {
        menus[menuIndex].SetActive(false);
        if (currentMenu == menuIndex) currentMenu = -1;
        isMenuOpen = false;
        Time.timeScale = 1f;
        Cursor.lockState = CursorLockMode.Locked;
    }

    public void ReturnToMainMenu()
    {
        HideMenu(1);
        ShowCursor();
        SceneManager.LoadScene("MainMenu");
        audioManager.AmbienceSource.Pause();
    }

    private void ShowCursor()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    public void ShowDeathScreen()
    {
        deathScreen.SetActive(true);
        Time.timeScale = 0f;
        ShowCursor();
    }

}

