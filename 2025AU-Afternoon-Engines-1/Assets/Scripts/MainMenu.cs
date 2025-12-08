using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{

    public GameObject optionsMenu;
    public GameObject mainMenu;
    public GameObject creditsMenu;
    public AudioMixer audioMixer;
    public AudioSource SFX;

    private void Awake()
    {
        audioMixer.SetFloat("Ambience", Mathf.Log10(PlayerPrefs.GetFloat("AmbienceVolume")) * 20);
        audioMixer.SetFloat("SFX", Mathf.Log10(PlayerPrefs.GetFloat("SFXVolume")) * 20);
    }
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
    public void playSFX()
    {
        SFX.Play();
    }
}
