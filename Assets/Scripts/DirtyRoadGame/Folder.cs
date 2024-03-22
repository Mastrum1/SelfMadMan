using System;
using System.Collections;
using UnityEngine;

public class Folder : MonoBehaviour
{
    [SerializeField] private float _speed;
    [SerializeField] private float _turn;

    public void Move(string function)
    {
        StartCoroutine(function);
    }
    
    IEnumerator MoveForward()
    {
        while (true)
        {
            transform.Translate(transform.up * (_speed * Time.deltaTime), Space.World);
            yield return null;   
        }
    }
    
    IEnumerator MoveBackward()
    {
        while (true)
        {
            transform.Translate(-transform.up * (_speed * Time.deltaTime), Space.World);
            yield return null;   
        }
    }

    public void MoveRight()
    {
        transform.Translate(transform.right * (_turn * Time.deltaTime), Space.World);
    }

    public void MoveLeft()
    {
        transform.Translate(-transform.right * (_turn * Time.deltaTime), Space.World);
    }

    private void OnDisable()
    {
        StopCoroutine(MoveForward());
        StopCoroutine(MoveBackward());
    }
}
