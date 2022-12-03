using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Loader : MonoBehaviour
{
    public GameObject menuManager;
    void Start()
    {
        menuManager.SetActive(true);
        DontDestroyOnLoad(menuManager);
        SceneManager.LoadScene(1);
    }
    void Update()
    {
        
    }
}
