using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New girl", menuName = "SO/Findher/Girl")]
public class Girls : ScriptableObject
{
    public Sprite ProfilePicture;
    public string GirlName;
    public int Age;

    //Test if descriptions are necessary 
    public string Description;
}
