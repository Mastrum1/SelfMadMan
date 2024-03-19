using NaughtyAttributes;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneManaProxy : MonoBehaviour
{
    [Scene] public string SceneName;

    public void ChangeScene()
    {
        mySceneManager.instance.SetScene(SceneName, mySceneManager.LoadMode.SINGLE);
    }

    public void StartGame()
    {
        GameManager.instance.OnGameStart();
    }
}