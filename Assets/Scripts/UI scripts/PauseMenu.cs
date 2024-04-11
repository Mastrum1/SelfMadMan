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
        StartCoroutine(CheckFinished());
    }

    public void Resume()
    {
        Time.timeScale = 1;
        _mPauseAnim.SetBool("OpenPause", false);
    }

    public void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void Quit()
    {
        mySceneManager.instance.SetScene("HomePage", mySceneManager.LoadMode.SINGLE);
    }

    IEnumerator CheckFinished()
    {
        yield return new WaitForSeconds(0.5f);
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
