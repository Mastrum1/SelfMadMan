using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NaughtyAttributes;


[CreateAssetMenu(fileName = "New Minigame", menuName = "MinigameItem")]
public class MinigameSO : ItemsSO
{
    public int Era;
    [Scene] public string MinigameScene;
}
