using System;
using System.Collections;
using UnityEngine;

public class Folder : MonoBehaviour
{
    [SerializeField] private float _speed;
    [SerializeField] private float _turn;
    
    public void MoveForward()
    {
        transform.Translate(transform.up * (_speed * Time.deltaTime), Space.World);
    }

    public void MoveRight()
    {
        if (transform.position.x >= 2.35f) return;
        transform.Translate(transform.right * (_turn * Time.deltaTime), Space.World);
    }

    public void MoveLeft()
    {
        if (transform.position.x <= -1.6f) return;
        transform.Translate(-transform.right * (_turn * Time.deltaTime), Space.World);
    }
}
