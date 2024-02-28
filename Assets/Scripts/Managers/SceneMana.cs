using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class ScaneMana : MonoBehaviour
{

    [SerializeField] int _mCurrentGame;

    GameObject _mGameManager;
    
    // Start is called before the first frame update
    void Start()
    {
        _mGameManager = GameObject.Find("GameManager");

        GameObject[] objs = GameObject.FindGameObjectsWithTag("SceneManager");

        if (objs.Length > 1)
        {
            Destroy(gameObject);
        }

        DontDestroyOnLoad(gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void NextGame()
    {
        _mGameManager.GetComponent<GameManager>().AddCurrent();
        _mCurrentGame = _mGameManager.GetComponent<GameManager>().GetCurrentGame();
        SceneManager.LoadScene(_mCurrentGame);
    }
}
