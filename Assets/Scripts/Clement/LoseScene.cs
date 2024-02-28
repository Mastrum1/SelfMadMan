using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoseScene : MonoBehaviour
{
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void HomeButton()
    {
        SceneMana.instance.ReturnToHome();
        GameManager.instance.ResetCurrentGame();
    }

    public void CountinueButton()
    {
        SceneMana.instance.Continue();
    }

    public void RetryButton()
    {
        SceneMana.instance.Retry();
    }
}

