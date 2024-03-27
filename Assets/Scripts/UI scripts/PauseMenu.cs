using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    [SerializeField] GameObject _mpauseMenu;

    private void Start()
    {
        _mpauseMenu = gameObject;
    }

    public void Pause()
    {
        _mpauseMenu.SetActive(true);
        Time.timeScale = 0;
    }

    public void Resume()
    {
        _mpauseMenu?.SetActive(false);
        Time.timeScale = 1;
    }

    public void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void Quit()
    {
        mySceneManager.instance.SetScene("HomePage", mySceneManager.LoadMode.SINGLE);
    }
}
