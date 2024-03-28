using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GarbageDrag : MonoBehaviour
{
    //public void OnSeleted
    public void OnDrag(Vector3 pos)
    {
        transform.position = new Vector3(pos.x, pos.y, 0);
    }
}
