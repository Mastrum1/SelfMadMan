using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    [SerializeField] GameObject _mpauseMenu;
    [SerializeField] GameObject _mElementsMenu;
    [SerializeField] InputManager _mInputManager;

    public void Pause()
    {
        if(!TutorialManager.instance.InTutorial)
        {
            Time.timeScale = 0;
            StartCoroutine(CheckOpenPause());
        }
    }

    public void Resume()
    {
        StartCoroutine(ResumePause());
    }

    public void Restart()
    {
        StartCoroutine(RestartLevel());
    }

    public void Quit()
    {
        StartCoroutine(QuitLevel());
    }

    public void DisableInput()
    {
        if (!TutorialManager.instance.InTutorial)
        {
            _mInputManager.enabled = false;
        }
    }

    public void EnableInput() 
    {
        _mInputManager.enabled = true;
    }

    IEnumerator CheckOpenPause()
    {
        _mpauseMenu.SetActive(true);
        _mElementsMenu.SetActive(true);
        float scale = 0f;
        while (scale != 1)
        {
            scale += Time.unscaledDeltaTime * 10;
            scale = Mathf.Clamp01(scale);
            _mElementsMenu.gameObject.transform.localScale = new Vector3(scale, scale, scale);
            yield return null;
        }
    }

    IEnumerator ResumePause()
    {
        float scale = 1f;
        while (scale != 0)
        {
            scale -= Time.unscaledDeltaTime * 10;
            scale = Mathf.Clamp01(scale);
            _mElementsMenu.gameObject.transform.localScale = new Vector3(scale, scale, scale);
            yield return null;
        }
        _mpauseMenu.SetActive(false);
        _mElementsMenu.SetActive(false);
        Time.timeScale = 1;
    }

    IEnumerator RestartLevel()
    {
        Time.timeScale = 1;
        yield return new WaitForSeconds(0.1f);

        // Keep track of active scenes
        List<Scene> activeScenes = new List<Scene>();

        // Iterate through all loaded scenes
        for (int i = 0; i < SceneManager.sceneCount; i++)
        {
            Scene scene = SceneManager.GetSceneAt(i);

            // Check if the scene is active
            if (scene.isLoaded && scene.isLoaded && scene.IsValid())
            {
                activeScenes.Add(scene);
            }
        }

        // Check if there are at least two active scenes
        if (activeScenes.Count >= 1)
        {
            // Get the first and second active scenes
            Scene firstScene = activeScenes[0];

            // Load the first and second active scenes
            SceneManager.LoadScene(firstScene.buildIndex);
            GameManager.instance.OnRestart();
            Debug.Log("I'm in");
        }
        else
        {
            // Handle the case where there are not enough active scenes
            Debug.Log("Not enough active scenes found.");
        }
    }

    IEnumerator QuitLevel()
    {
        Time.timeScale = 1;
        yield return new WaitForSeconds(0.1f);

        // Keep track of active scenes
        List<Scene> activeScenes = new List<Scene>();

        // Iterate through all loaded scenes
        for (int i = 0; i < SceneManager.sceneCount; i++)
        {
            Scene scene = SceneManager.GetSceneAt(i);

            // Check if the scene is active
            if (scene.isLoaded && scene.isLoaded && scene.IsValid())
            {
                activeScenes.Add(scene);
            }
        }

        // Check if there are at least two active scenes
        if (activeScenes.Count >= 1)
        {
            // Get the first and second active scenes
            Scene firstScene = activeScenes[0];

            // Set the first and second active scenes to "HomePage" and load them in single mode
            SceneManager.LoadScene(firstScene.buildIndex);
            mySceneManager.instance.SetScene("HomePage", mySceneManager.LoadMode.ADDITIVE);
        }
        else
        {
            // Handle the case where there are not enough active scenes
            Debug.Log("Not enough active scenes found.");
        }
    }
}
