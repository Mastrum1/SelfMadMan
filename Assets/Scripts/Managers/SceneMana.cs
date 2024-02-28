using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class ScaneMana : MonoBehaviour
{

    [SerializeField] int CurrentGame;

    public GameObject gameManager;
    
    // Start is called before the first frame update
    void Start()
    {
        gameManager = GameObject.Find("GameManager");

        GameObject[] objs = GameObject.FindGameObjectsWithTag("SceneManager");

        if (objs.Length > 1)
        {
            Destroy(this.gameObject);
        }

        DontDestroyOnLoad(gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void NextGame()
    {
        gameManager.GetComponent<GameManager>().AddCurrent();
        CurrentGame = gameManager.GetComponent<GameManager>().GetCurrentGame();
        SceneManager.LoadScene(CurrentGame);
    }
}
