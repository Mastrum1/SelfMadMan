using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    [SerializeField] GameObject _mpauseMenu;
    [SerializeField] Animator _mPauseAnim;

    public void Pause()
    {
        _mPauseAnim.SetBool("OpenPause", true);
        StartCoroutine(CheckOpenPause());
    }

    public void Resume()
    {
        Time.timeScale = 1;
        _mPauseAnim.SetBool("OpenPause", false);
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
        yield return new WaitForSeconds(0.3f);
        if (_mPauseAnim.GetBool("OpenPause") == true)
        {
            if (_mPauseAnim.GetCurrentAnimatorStateInfo(0).IsName("OpenPause"))
            {
                Time.timeScale = 0;
                Debug.Log("Paused");
            }
        }
    }
}
