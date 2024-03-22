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
            if (transform.position.y >= 4f) yield break;
            
            transform.Translate(transform.up * (_speed * Time.deltaTime), Space.World);
            yield return null;   
        }
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

    private void OnDisable()
    {
        StopCoroutine(MoveForward());
    }
}
