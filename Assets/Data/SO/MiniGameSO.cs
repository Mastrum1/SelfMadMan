using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;
using NaughtyAttributes;

[CreateAssetMenu(fileName = "New MiniGame", menuName = "MiniGame", order = 1)]
public class MiniGameSO : ScriptableObject
{
    Sprite MiniGameIcon;
    [SerializeField] public string MinigameName;
    [SerializeField] public int MinigameEra;
}
