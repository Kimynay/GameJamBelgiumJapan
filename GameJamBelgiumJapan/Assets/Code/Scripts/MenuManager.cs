using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuManager : MonoBehaviour
{
    public static MenuManager menuManager;
    public GameObject mainMenu;
    public GameObject gameMenu;
    public GameObject winMenu;
    public GameObject looseMenu;
    public GameObject menuBackGRound;
    public PokedexManager pokedexManager;
    public Slider VolumeSlider;
    public Camera mainCamera;

    private void Awake()
    {
        menuManager = this;
        DontDestroyOnLoad(gameObject);
    }
    void Update()
    {
        if(mainCamera == null)
        {
            mainCamera = Camera.main;
        }
        mainCamera.gameObject.GetComponent<AudioSource>().volume = VolumeSlider.value;

    }
    public void PlayGame ()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
    public void QuitGame()
    {
        Debug.Log("QUIT !");
        Application.Quit();
    }
    public void WinGame()
    {
        Debug.Log("You Win !");
        menuBackGRound.SetActive(false);
        mainMenu.SetActive(false);
        gameMenu.SetActive(false);
        winMenu.SetActive(true);
        SceneManager.LoadScene(1);
    }
    public void LooseGame()
    {
        Debug.Log("You Loosed !");
        menuBackGRound.SetActive(false);
        mainMenu.SetActive(false);
        gameMenu.SetActive(false);
        looseMenu.SetActive(true);
        SceneManager.LoadScene(1);
    }
    public void ReturnToMenu()
    {
        SceneManager.LoadScene(1);
    }
}
