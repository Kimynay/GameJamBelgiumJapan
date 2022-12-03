using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    public bool returnToMenu = false;
    public GameObject Canvas;
    private void OnEnable()
    {
        DontDestroyOnLoad(gameObject);
    }
    void Update()
    {
        if (returnToMenu)
        {
            returnToMenu = false;
            SceneManager.LoadScene(1);
            Canvas.SetActive(true);
        }
    }
    public void PlayGame ()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        Canvas.SetActive(false);
    }
    public void QuitGame()
    {
        Debug.Log("QUIT !");
        Application.Quit();
    }
    public void WinGame()
    {
        Debug.Log("You Win !");
        SceneManager.LoadScene(1);
    }
    public void LooseGame()
    {
        Debug.Log("You Loosed !");
        SceneManager.LoadScene(1);
    }
}
