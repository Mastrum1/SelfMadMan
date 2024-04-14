using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Trophy", menuName = "SO/NewTrophy", order = 1)]
public class Trophies : ScriptableObject
{
    public int ID;
    public string trophyDescription;
    public int reward;
    public Sprite trophyIcon;
}
