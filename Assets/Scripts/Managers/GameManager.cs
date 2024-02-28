using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [SerializeField] int _mCurrentGame;
    [SerializeField] int _mSpeed;
    [SerializeField] int _mScore;
    [SerializeField] int _mHearts;

    // Start is called before the first frame update
    void Start()
    {
        _mHearts = 3;
        _mSpeed = 1;

        GameObject[] objs = GameObject.FindGameObjectsWithTag("GameManager");

        if (objs.Length > 1)
        {
            Destroy(gameObject);
        }

        DontDestroyOnLoad(gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        if (_mHearts <= 0)
        {
            SceneManager.LoadScene("LoseScene");
        }
    }

    public void AddCurrent()
    {
        _mCurrentGame++;
    }

    public int GetCurrentGame()
    {
        return _mCurrentGame;
    }

    public void GainScore()
    {

    }

    public int GetScore() 
    {
        return _mScore;
    }

    public string DisplayScore()
    {
        return _mScore.ToString();
    }

}
