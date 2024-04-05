using NaughtyAttributes;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class MinigameScene
{

    [SerializeField][Scene] private string _sceneName;
    public string SceneName { get => _sceneName; set => _sceneName = value; }

    [SerializeField] private bool _locked;
    public bool Locked { get => _locked; set => _locked = value; }

    public void Unlock()
    {
        Debug.Log("unlocking " + SceneName);
        Locked = false;
    }
}