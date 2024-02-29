using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class mySceneManager : MonoBehaviour
{
    public static mySceneManager instance;

    private string _mScene;
    
    void Awake()
    {
        if (instance == null)
            instance = this;
        else if (instance != this)
            Destroy(gameObject);

        DontDestroyOnLoad(gameObject);
    }

    public void SetScene(string scene)
    {
        _mScene = scene;
        SceneManager.LoadScene(_mScene, LoadSceneMode.Single);
    }
}
