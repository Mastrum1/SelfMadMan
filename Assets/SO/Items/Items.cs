using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Item", menuName = "Item")]
public class Items : ScriptableObject
{
    public int ID;
    public string ItemName;
    public string Description;
    public string Type;
    public Sprite Icon;
    public Sprite Look;
}
