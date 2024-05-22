using System;
using UnityEngine;

public class James : MonoBehaviour
{
    public void Move(Vector3 position)
    {
        transform.position = new Vector3(position.x, transform.position.y);
    }
}
