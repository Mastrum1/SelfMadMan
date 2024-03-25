using NaughtyAttributes;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class MinigameScene
{
    [SerializeField][Scene] private string _sceneName;
    public string SceneName { get => _sceneName; set => _sceneName = value; }

    [SerializeField] private bool _unlocked;
    public bool Unlocked { get => _unlocked; set => _unlocked = value; }

    public void Unlock()
    {
        Unlocked = true;
    }
}