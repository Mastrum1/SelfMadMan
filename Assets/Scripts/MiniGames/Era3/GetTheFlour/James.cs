using System;
using UnityEngine;

public class James : MonoBehaviour
{
    [SerializeField] private GameObject _maxLeft;
    [SerializeField] private GameObject _maxRight;
    public void Move(Vector3 position)
    {
        if (position.x <= _maxLeft.transform.position.x) return;
        if (position.x >= _maxRight.transform.position.x) return;
        
        transform.position = new Vector3(position.x, transform.position.y);
    }
}
