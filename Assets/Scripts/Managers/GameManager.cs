using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] int CurrentGame;
    [SerializeField] int Speed;
    [SerializeField] int Score;

    // Start is called before the first frame update
    void Start()
    {
        GameObject[] objs = GameObject.FindGameObjectsWithTag("GameManager");

        if (objs.Length > 1)
        {
            Destroy(this.gameObject);
        }

        Speed = 1;

        DontDestroyOnLoad(gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void AddCurrent()
    {
        CurrentGame++;
    }

    public int GetCurrentGame()
    {
        return CurrentGame;
    }

    public void GainScore()
    {

    }
}
