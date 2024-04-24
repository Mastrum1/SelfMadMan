using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Item", menuName = "Item")]
public class ItemsSO : ScriptableObject
{
    public enum TYPE { COINS, MINIGAME };

    public int ID;
    public string ItemName;
    public TYPE Type;
    public Sprite Icon;
    public int Cost;
}
