using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Loader : MonoBehaviour
{
    public float LoadingTime = 2.0f;
    public GameObject menuManager;
    void Awake()
    {
        DontDestroyOnLoad(menuManager);
    }
    private void Start()
    {
        StartCoroutine(Load());
    }
    public IEnumerator Load()
    {
        yield return new WaitForSeconds(LoadingTime);
        menuManager.SetActive(true);
        SceneManager.LoadScene(1);
    }
}
