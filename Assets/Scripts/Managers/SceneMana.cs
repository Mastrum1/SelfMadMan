using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class ScaneMana : MonoBehaviour
{

    [SerializeField] int _mCurrentGame;
    
    // Start is called before the first frame update
    void Start()
    {
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
        GameManager.instance.AddCurrent();
        _mCurrentGame = GameManager.instance.GetCurrentGame();
        SceneManager.LoadScene(_mCurrentGame);
    }
}
