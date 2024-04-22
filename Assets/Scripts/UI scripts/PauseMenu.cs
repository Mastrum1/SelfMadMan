using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    [SerializeField] GameObject _mpauseMenu;
    [SerializeField] GameObject _mElementsMenu;

    public void Pause()
    {
        Time.timeScale = 0;
        StartCoroutine(CheckOpenPause());
    }

    public void Resume()
    {
        StartCoroutine(ResumePause());
    }

    public void Restart()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void Quit()
    {
        Time.timeScale = 1;
        mySceneManager.instance.SetScene("HomePage", mySceneManager.LoadMode.SINGLE);
    }

    IEnumerator CheckOpenPause()
    {
        _mpauseMenu.SetActive(true);
        _mElementsMenu.SetActive(true);
        float scale = 0f;
        while (scale != 1)
        {
            scale += Time.unscaledDeltaTime;
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
            scale -= Time.unscaledDeltaTime;
            scale = Mathf.Clamp01(scale);
            _mElementsMenu.gameObject.transform.localScale = new Vector3(scale, scale, scale);
            yield return null;
        }

        if (scale == 0)
        {
            _mpauseMenu.SetActive(false);
            _mElementsMenu.SetActive(false);
            Time.timeScale = 1;
        }
    }
}
