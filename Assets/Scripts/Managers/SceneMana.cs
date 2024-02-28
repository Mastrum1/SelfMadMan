using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class SceneMana : MonoBehaviour
{
    public static SceneMana instance;

    [SerializeField] int _mCurrentGame;
    
    // Start is called before the first frame update
    void Start()
    {
        if (instance == null)
            instance = this;
        else if (instance != this)
            Destroy(gameObject);

        DontDestroyOnLoad(gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void NextGame()
    {
        GameManager.instance.AddCurrent();
        _mCurrentGame = GameManager.instance.GetCurrentGame();
        SceneManager.LoadScene(_mCurrentGame);
    }

    public void ReturnToHome()
    {
        GameManager.instance.ResetCurrentGame();
        SceneManager.LoadScene("Clement");
    }

    public void Continue()
    {
        
    }

    public void Retry()
    {

    }

}
