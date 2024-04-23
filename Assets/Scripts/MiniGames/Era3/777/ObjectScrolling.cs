using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectScrolling : MonoBehaviour
{
    [SerializeField] int _mSpeed;
    private bool Started = false;

    public void StartGame()
    {
        Started = true;
    }

    void Update()
    {
        if (Started)
        {
            transform.Translate(Vector3.down * _mSpeed * Time.deltaTime);
        }
    }
}
