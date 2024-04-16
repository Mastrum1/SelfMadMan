using NaughtyAttributes;
using UnityEngine;

[CreateAssetMenu(fileName = "Trophy", menuName = "SO/NewTrophy", order = 1)]
public class Trophies : ScriptableObject
{
    public int ID;
    public string trophyDescription;
    public int goal;
    public int reward;
    public MinigameScene scene;
}
