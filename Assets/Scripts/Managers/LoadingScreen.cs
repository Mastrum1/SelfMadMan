using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LoadingScreen : MonoBehaviour
{
    public static LoadingScreen Instance;
    [SerializeField] GameObject _mLoadingScreenObject;
    [SerializeField] Slider _mProgressBar;
    AsyncOperation _mLoadingOperation;
    
    void Awake()
    {
        if (Instance == null) {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        } else
            Destroy(gameObject);
        _mLoadingScreenObject.SetActive(false);
    }

    public void LoadScene(string sceneToLoad)
    {
        _mLoadingOperation = SceneManager.LoadSceneAsync(sceneToLoad);
        _mLoadingOperation.allowSceneActivation = false;
        _mLoadingScreenObject.SetActive(true);
        while (_mLoadingOperation.progress < 0.9f) {
            //await Task.Delay(10);
            _mProgressBar.value = Mathf.Clamp01(_mLoadingOperation.progress / 0.9f);
        } 
        _mLoadingOperation.allowSceneActivation = true;
        _mLoadingScreenObject.SetActive(false);
    }
}
