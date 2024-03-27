using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CommentMovement : MonoBehaviour
{
    [SerializeField] private float _mSpeed = 1.5f;
    void Start()
    {
    }

    void Update()
    {
        transform.Translate(Vector3.up * _mSpeed * Time.deltaTime);
    }

    public void Stop()
    {
        _mSpeed = 0;
    }
}
