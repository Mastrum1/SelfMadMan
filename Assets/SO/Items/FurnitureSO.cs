using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Furniture", menuName = "Furniture")]
public class FurnitureSO : ItemsSO
{
    public FurnitureManager.FurnitureType Type;
    public GameObject FurniturePrefab;
}
