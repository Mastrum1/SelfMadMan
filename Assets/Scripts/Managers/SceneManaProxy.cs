using NaughtyAttributes;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneManaProxy : MonoBehaviour
{
    [SerializeField] string SceneName;

    public void ChangeScene()
    {
        mySceneManager.instance.SetScene(SceneName);
    }

    public void RandomScene(int era)
    {
        mySceneManager.instance.RandomGameChoice(era);
    }
}