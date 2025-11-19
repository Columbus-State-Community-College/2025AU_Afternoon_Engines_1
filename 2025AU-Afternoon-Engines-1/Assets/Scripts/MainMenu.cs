using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{

    public GameObject optionsMenu;
    public GameObject mainMenu;
    public GameObject creditsMenu;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        optionsMenu.SetActive(false);
        creditsMenu.SetActive(false);
    }

    

    public void goToOptions()
    {
        optionsMenu.SetActive(true);
        mainMenu.SetActive(false);

    }

    public void StartGame()
    {
        SceneManager.LoadScene(1);
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void goToMainMenu()
    {
        optionsMenu.SetActive(false);
        mainMenu.SetActive(true);
    }

    public void ExitCredits()
    {
        creditsMenu.SetActive(false);
        mainMenu.SetActive(true);
    }

    public void goToCredits()
    {

        mainMenu.SetActive(false);
        creditsMenu.SetActive(true);
    }
}
